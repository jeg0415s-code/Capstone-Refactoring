using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Runtime.Player;

public class PlayerUIManager : MonoBehaviour
{
    // namespaces
    private PlayerState State;

    #region Variables
    [Header("UI")]
    [Tooltip("배터리 슬라이더")]
    [SerializeField] private Slider batteryBarSlider = null;
    [Tooltip("스테미나 슬라이더(Canvas Group 컴포넌트가 포함돼있어야함)")]
    [SerializeField] private Slider staminaBarSlider = null;
    [Tooltip("스테미나 Fill 트랜스폼")]
    [SerializeField] RectTransform staminaFill = null;
    [Tooltip("스테미나 바 Fadeinout 속도")]
    [SerializeField] float staminaBarFadeSpeed = 2f;
    [Tooltip("획득한 콜렉션 수 텍스트")]
    [SerializeField] private TextMeshProUGUI collectionCountText = null;

    private float staminaBarGroupAlpha = 0f;

    // 컴포넌트
    private CanvasGroup staminaBarGroup = null;
    #endregion

    private void Start()
    {
        // 플레이어 오브젝트 찾기
        State = FindFirstObjectByType<Player>().State;
        if (State == null) State = new PlayerState();

        // 스테미나 바의 CanvasGroup 컴포넌트 받아오기
        if (staminaBarSlider != null) staminaBarGroup = staminaBarSlider.gameObject.GetComponent<CanvasGroup>();
        else return;

        // 스테미나 바 안보이게 설정
        staminaBarGroupAlpha = 0f;
        staminaBarGroup.alpha = staminaBarGroupAlpha;
    }

    private void Update()
    {
        HandleSliders();
    }

    #region Functions
    private void HandleSliders()
    {
        // hp/stamina 바 값 변경
        batteryBarSlider.value = State.BatteryRatio;
        HandleStaminaBar(State.StaminaRatio);
    }

    private void HandleStaminaBar(float value)
    {
        if (staminaBarSlider == null || staminaFill == null) return;

        // 스테미나 바의 값이 변경되거나, 1이 되면 자연스럽게 FadeInOut되기
        var target = State.StaminaRatio < 1f ? 1f : 0f;
        if (staminaBarGroup != null)
            staminaBarGroup.alpha = Mathf.MoveTowards(staminaBarGroup.alpha, target, staminaBarFadeSpeed * Time.deltaTime);

        float clampValue = Mathf.InverseLerp(staminaBarSlider.minValue, staminaBarSlider.maxValue, value); // 0~1로 정규화
        float halfValue = clampValue * 0.5f;

        // 스테미나 바의 Fill 이미지가 중앙으로 줄어들게 만들기
        staminaFill.anchorMin = new Vector2(0.5f - halfValue, staminaFill.anchorMin.y);
        staminaFill.anchorMax = new Vector2(0.5f + halfValue, staminaFill.anchorMax.y);
        staminaFill.offsetMin = new Vector2(0, staminaFill.offsetMin.y);
        staminaFill.offsetMax = new Vector2(0, staminaFill.offsetMax.y);
    }
    #endregion
}
