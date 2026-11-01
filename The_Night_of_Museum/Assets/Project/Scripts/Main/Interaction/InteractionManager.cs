using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    // 오브젝트의 상호작용 타입 Enum
    public enum InteractionType
    {
        Battery,
        Collection,
        Cabinet,
        Safe,
        Statue,
        ExitDoor
    }

    #region Variables
    [Header("Object Type")]
    [Tooltip("오브젝트의 상호작용 타입")]
    [SerializeField] private InteractionType type = InteractionType.Battery;
    
    [Header("UI Settings")]
    [Tooltip("상호작용 가능 표시 UI")]
    [SerializeField] GameObject interactionUI = null;
    #endregion

    private void Start()
    {
        // 상호작용 UI 비활성화
        interactionUI.SetActive(false);
    }

    #region Functions
    // 상호작용 UI 활성/비활성화
    public void SetActiveInteractUI()
    {
        interactionUI.SetActive(!interactionUI.activeSelf);
    }
    #endregion
}
