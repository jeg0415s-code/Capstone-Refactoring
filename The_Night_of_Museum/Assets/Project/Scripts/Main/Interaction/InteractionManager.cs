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

    [Header("Interaction Data")]
    [SerializeField] private BatteryData batteryData = new BatteryData();
    [SerializeField] private CollectionData collectionData = new CollectionData();

    public BatteryData Battery => batteryData;
    public CollectionData Collection => collectionData;
    #endregion

    private void Start()
    {
        // 상호작용 UI 비활성화
        interactionUI.SetActive(false);
    }

    #region Functions
    public void HandleInteraction()
    {
        switch(type)
        {
        case InteractionType.Battery:
            Debug.Log("배터리 상호작용");
            break;
        case InteractionType.Collection:
            Debug.Log("콜렉션 상호작용");
            break;
        case InteractionType.Cabinet:
            Debug.Log("캐비넷 상호작용");
            break;
        case InteractionType.Safe:
            Debug.Log("금고 상호작용");
            break;
        case InteractionType.Statue:
            Debug.Log("석상 상호작용");
            break;
        case InteractionType.ExitDoor:
            Debug.Log("출구 상호작용");
            break; 
        }
    }

    // 상호작용 UI 활성/비활성화
    public void SetActiveInteractUI()
    {
        interactionUI.SetActive(!interactionUI.activeSelf);
    }
    #endregion
}
