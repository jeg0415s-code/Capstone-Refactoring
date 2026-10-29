using UnityEngine;

using Runtime.Gameplay;
using Runtime.Player;

public class PlayerController : MonoBehaviour
{
    // namespaces
    readonly PlayerControl Control = new PlayerControl();

    #region Variables
    [Header("Movement Settings")]
    [Tooltip("걷기 이동 속도")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("달리기 이동 속도")]
    [SerializeField] private float sprintSpeed = 12f;

    [Header("Camera Settings")]
    [Tooltip("카메라 민감도")]
    [SerializeField] private float cameraSensitivity = 1.5f;
    [Tooltip("카메라 X축 최소 각도")]
    [SerializeField] private float minXLook = -45f;
    [Tooltip("카메라 X축 최대 각도")]
    [SerializeField] private float maxXLook = 45f;
    [Tooltip("플레이어 시아 높이")]
    [SerializeField] private float eyeHeight = 2f;

    // bool 변수
    private bool canMove = true;
    private bool isMoving = false;
    private bool isSprinting = false;

    // 컴포넌트
    private Rigidbody rb = null;
    private Transform camera = null;

    // 카메라 private 변수
    private float xRotation = 0f;
    float lookYawVelocity = 0f;
    float lookPitchVelocity = 0f;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PlayerEvent.MovementEnabledChanged += OnMovementEnabledChanged;
    }

    private void OnDisable()
    {
        PlayerEvent.MovementEnabledChanged -= OnMovementEnabledChanged;
    }

    private void Start()
    {
        if (Camera.main != null) { camera = Camera.main.transform; }
        else { Debug.LogError("Main camera is null!"); return; }

        Control.Bind(rb, camera);

        camera.position = new Vector3(0f, eyeHeight, 0f);
    }

    private void Update()
    {
        if (!canMove) return;
        HandleMovement();
    }

    private void LateUpdate()
    {
        if (!canMove) return;
        HandleCamera();
    }

    #region Functions
    private void OnMovementEnabledChanged(bool enabled) => canMove = enabled;

    private void HandleMovement()
    {
        isSprinting = GameplayInput.SprintHeld;
        var vertical = GameplayInput.VerticalInput;
        var horizontal = GameplayInput.HorizontalInput;
        isMoving = vertical != 0f || horizontal != 0f;

        var curSpeed = isSprinting ? sprintSpeed : moveSpeed;
        Control.Movement(canMove, isMoving, GameplayInput.VerticalInput, GameplayInput.HorizontalInput, curSpeed);
    }

    private void HandleCamera()
    {
        var mouseX = GameplayInput.MouseX * cameraSensitivity;
        var mouseY = GameplayInput.MouseY * cameraSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxXLook, minXLook);
        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    #endregion
}
