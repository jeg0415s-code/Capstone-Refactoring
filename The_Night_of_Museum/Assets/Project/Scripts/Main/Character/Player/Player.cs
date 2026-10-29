using UnityEngine;
using Runtime.Player;

public class Player : MonoBehaviour, PlayerPawn
{
    readonly PlayerState State = new PlayerState();

    #region Variables
    // Player Pawn 할당
    Transform PlayerPawn.Transform => transform;
    #endregion
}
