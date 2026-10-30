using System;
using UnityEngine;

namespace Runtime.Player
{
    public class PlayerState
    {
        public float Battery;
        public float MaxBattery;
        public float Stamina;
        public float MaxStamina;

        public float UseBatteryPerSec;
        public float UseStaminaPerSec;
        public float ChargeStaminaPerSec;

        private bool IsExhausted;
        public bool IsNoBattery;

        public float BatteryRatio => MaxBattery > 0f ? Battery / MaxBattery : 0f;
        public float StaminaRatio => MaxStamina > 0f ? Stamina / MaxStamina : 0f;

        public event Action BatteryDepleted;

        /// <summary>
        /// 배터리/스테미나 등의 변수값 초기화
        /// <param name="battery"> 플레이어 배터리량
        /// <param name="stamina"> 플레이어 스테미나량
        /// <param name="useBatteryPerSec"> 초당 배터리 사용량
        /// <param name="useStaminaPerSec"> 초당 스테미나 사용량
        /// <param name="chargeStaminaPerSec"> 초당 스테미나 충전량
        /// </summary>
        public void Initialize(
            float battery,
            float stamina,
            float useBatteryPerSec,
            float useStaminaPerSec,
            float chargeStaminaPerSec)
        {
            Battery = MaxBattery = battery;
            Stamina = MaxStamina = stamina;
            UseBatteryPerSec = useBatteryPerSec;
            UseStaminaPerSec = useStaminaPerSec;
            ChargeStaminaPerSec = chargeStaminaPerSec;

            IsExhausted = false;
            IsNoBattery = false;
        }

        /// <summary>
        /// 배터리 사용
        /// <param name="deltaTime"> Time.deltaTime
        /// </summary>
        public void UseBattery(float deltaTime)
        {
            if (Battery > 0)
            {
                Battery -= UseBatteryPerSec * deltaTime;
                return;
            }

            BatteryDepletion();
        }

        /// <summary>
        /// 배터리 충전
        /// <param name="amount"> 초당 충전량
        /// </summary>
        public void ChargeBattery(float amount)
        {
            if (IsNoBattery) return;

            Battery = Mathf.Min(Battery + amount, MaxBattery);

            float currentRatio = BatteryRatio;
        }

        /// <summary>
        /// 스테미나 사용
        /// <param name="deltaTime"> Time.deltaTime
        /// </summary>
        public void UseStamina(float deltaTime)
        {
            if (Stamina <= 0f && IsExhausted) return;

            Stamina -= UseStaminaPerSec * deltaTime;
            if (Stamina > 0f) return;

            Stamina = 0f;
            IsExhausted = true;
            PlayerEvent.SetExhausted(true);
        }

        /// <summary>
        /// 스테미나 충전
        /// <param name="deltaTime"> Time.deltaTime
        /// </summary>
        public void ChargeStamina(float deltaTime)
        {
            Stamina = Mathf.Min(Stamina + (ChargeStaminaPerSec * deltaTime), MaxStamina);
            if (IsExhausted && Stamina >= MaxStamina * 0.3f)
            {
                IsExhausted = false;
                PlayerEvent.SetExhausted(false);
            }
        }

        private void BatteryDepletion()
        {
            Battery = 0f;
            if (IsNoBattery) return;

            IsNoBattery = true;
            BatteryDepleted?.Invoke();
        }
    }
}
