using UnityEngine;

using Runtime.Player;
using Runtime.Gameplay;

public class Player : MonoBehaviour, PlayerPawn
{
    // namespaces
    public PlayerState State { get; } = new PlayerState();

    #region Variables
    [Header("Battery Settings")]
    [Tooltip("플레이어 최대 배터리 양")]
    [SerializeField] private float battery = 100f;
    [Tooltip("초당 배터리 사용량")]
    [SerializeField] private float useBatteryPerSec = 0.5f;

    [Header("Stamina Settings")]
    [Tooltip("플레이어 최대 스테미나 양")]
    [SerializeField] private float stamina = 50f;
    [Tooltip("초당 스테미나 사용량")]
    [SerializeField] private float useStaminaPerSec = 4f;
    [Tooltip("초당 스테미나 충전량")]
    [SerializeField] private float chargeStaminaPerSec = 9f;

    // bool 변수
    private bool isGamePause = false;
    private bool isMoving = false;
    private bool isHiding = false;
    private bool isNoBattery = false;
    private bool isExhausted = false;
    private bool isDead = false;

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
        // PlayerState 값 초기화
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
    // 이벤트 값 할당
    private void OnGamePause(bool enabled) => isGamePause = enabled;
    private void OnIsMovingNow(bool enabled) => isMoving = enabled;
    private void OnExhaustedChanged(bool enabled) => isExhausted = enabled;

    // State 값 받아오기
    private void SyncStats()
    {
        battery = State.Battery;
        stamina = State.Stamina;
        isNoBattery = State.IsNoBattery;
    }

    private void HandleState()
    {
        // 배터리 자동 사용
        State.UseBattery(Time.deltaTime);
        
        // 달리기
        if (GameplayInput.SprintHeld && isMoving && !isExhausted) { State.UseStamina(Time.deltaTime); }
        else { State.ChargeStamina(Time.deltaTime); }

        SyncStats();
    }
    #endregion
}
