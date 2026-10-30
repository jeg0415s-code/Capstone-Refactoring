using UnityEngine;

public class InteractionManager : MonoBehaviour
{
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
    private InteractionType type = InteractionType.Battery;
    
    [Header("UI Settings")]
    [Tooltip("상호작용 가능 표시 UI")]
    [SerializeField] GameObject interactionUI = null;
    #endregion
}
