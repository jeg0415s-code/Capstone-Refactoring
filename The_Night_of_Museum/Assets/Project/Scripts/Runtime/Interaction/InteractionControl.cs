using UnityEngine;

using Runtime.Player;

/// <summary>
/// 상호작용 오브젝트 타입별 기능들 모음 namespace
/// </summary>
namespace Runtime.Interaction
{
    /// <summary>
    /// 배터리 오브젝트
    /// </summary>
    public class InteractionControl
    {
        /// <summary>
        /// 플레이어 배터리 충전
        /// <param name="amount"> 배터리 충전량
        /// <param name="playerState"> PlayerState 스크립트
        /// </summary>
        public void HandleBattery(float amount, PlayerState playerState)
        {
            if (playerState == null) return;

            playerState.ChargeBattery(amount);
            Debug.Log("플레이어 배터리 충전");
        }

        /// <summary>
        /// 콜렉션 상호작용
        /// <param name="type"> 콜렉션 타입 변수
        /// </summary>
        public void HandleCollection(CollectionData.CollectionType type)
        {
            if (type == null) return;

            if (type == CollectionData.CollectionType.Real) Debug.Log("콜렉션 상호작용");
            else Debug.Log("가짜 콜렉션 상호작용");
        }
    }
}
