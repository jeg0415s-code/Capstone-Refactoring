using UnityEngine;

namespace Runtime.Player
{
    public class PlayerInteraction
    {
        /// <summary>
        /// 상호작용 가능 범위 안에 상호작용 가능 오브젝트가 들어왔는지
        /// <param name="interactionRange"> 상호작용 가능 범위
        /// </summary>
        private bool CanInteraction(float interactionRange)
        {
            var interactionSq = interactionRange * interactionRange;
        }
    }
}
