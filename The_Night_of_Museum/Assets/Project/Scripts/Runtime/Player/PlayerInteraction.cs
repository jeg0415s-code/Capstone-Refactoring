using UnityEngine;

namespace Runtime.Player
{
    public class PlayerInteraction
    {
        /// <summary>
        /// 플레이어 상호작용
        /// <param name="player"> 플레이어 트랜스폼
        /// <param name="target"> 상호작용 오브젝트 트랜스폼
        /// <param name="interactionRange"> 상호작용 가능 범위
        /// </summary>
        private void HandleInteraction(Transform player, Transform target, float interactionRange)
        {
            var range = (target.position - player.position).sqrMagnitude <= interactionRange * interactionRange;
        }
    }
}
