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
        public float ChargeBatteryPerSec;
        public float UseStaminaPerSec;
        public float ChargeStaminaPerSec;

        public bool IsExhausted;
        public bool IsNoBattery;

        public float BatteryRatio => MaxBattery > 0f ? Battery / MaxBattery : 0f;
        public float StaminaRatio => MaxStamina > 0f ? Stamina / MaxStamina : 0f;

        /// <summary>
        /// 배터리/스테미나 등의 변수값 초기화
        /// </summary>
        public void Initialize(
            float battery,
            float stamina,
            float useBatteryPerSec,
            float chargeBatteryPerSec,
            float useStaminaPerSec,
            float chargeStaminaPerSec)
        {
            Battery = MaxBattery = battery;
            Stamina = MaxStamina = stamina;
            UseBatteryPerSec = useBatteryPerSec;
            ChargeBatteryPerSec = chargeBatteryPerSec;
            UseStaminaPerSec = useStaminaPerSec;
            ChargeStaminaPerSec = chargeStaminaPerSec;

            IsExhausted = false;
            IsNoBattery = false;
        }
    }
}
