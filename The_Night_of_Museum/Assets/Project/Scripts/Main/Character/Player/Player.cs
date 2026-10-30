using UnityEngine;

using Runtime.Player;
using Runtime.Gameplay;

public class Player : MonoBehaviour, PlayerPawn
{
    // namespaces
    public PlayerState State { get; } = new PlayerState();

    #region Variables
    [Header("Battery Settings")]
    [Tooltip("플레이어 최대 배터리량")]
    [SerializeField] private float battery = 100f;
    [Tooltip("초당 배터리 사용량")]
    [SerializeField] private float useBatteryPerSec = 0.5f;

    [Header("Stamina Settings")]
    [Tooltip("플레이어 최대 스테미나량")]
    [SerializeField] private float stamina = 50f;
    [Tooltip("초당 스테미나 사용량")]
    [SerializeField] private float useStaminaPerSec = 4f;
    [Tooltip("초당 스테미나 충전량")]
    [SerializeField] private float chargeStaminaPerSec = 9f;

    // bool 변수
    private bool isMoving = false;
    private bool isHiding = false;
    private bool isNoBattery = false;
    private bool isExhausted = false;
    private bool isDead = false;
    private bool isGamePause = false;

    // Player Pawn 할당
    Transform PlayerPawn.Transform => transform;
    #endregion

    private void OnEnable()
    {
        GameplayEvent.GamePause += OnGamePause;
        PlayerEvent.IsMovingNow += OnIsMovingNow;
        PlayerEvent.ExhaustedChanged += OnExhaustedChanged;
    }

    private void OnDisable()
    {
        GameplayEvent.GamePause -= OnGamePause;
        PlayerEvent.IsMovingNow -= OnIsMovingNow;
        PlayerEvent.ExhaustedChanged -= OnExhaustedChanged;

    }

    private void Start()
    {
        State.Initialize(battery, stamina, useBatteryPerSec, useStaminaPerSec, chargeStaminaPerSec);

        isNoBattery = false;
        isHiding = false;
        isDead = false;
        isGamePause = false;
    }

    private void Update()
    {
        if (isGamePause) return;

        HandleState();
    }

    #region Functions
    private void OnGamePause(bool enabled) => isGamePause = enabled;
    private void OnIsMovingNow(bool enabled) => isMoving = enabled;
    private void OnExhaustedChanged(bool enabled) => isExhausted = enabled;

    private void SyncStats()
    {
        battery = State.Battery;
        stamina = State.Stamina;
        isNoBattery = State.IsNoBattery;
    }

    private void HandleState()
    {
        State.Battery = battery;
        State.Stamina = stamina;

        State.UseBattery(Time.deltaTime);
        
        if (GameplayInput.SprintHeld && isMoving && !isExhausted) { State.UseStamina(Time.deltaTime); }
        else { State.ChargeStamina(Time.deltaTime); }

        SyncStats();
    }
    #endregion
}
