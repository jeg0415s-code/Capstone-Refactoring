using UnityEngine;

namespace Runtime.Player
{
    public class PlayerInteraction
    {
        private readonly Collider[] hits = new Collider[16];
        private InteractionManager current = null;

        /// <summary>
        /// 플레이어 상호작용
        /// <param name="player"> 플레이어 트랜스폼
        /// <param name="view"> 카메라 Transform
        /// <param name="viewAngle"> 시야각
        /// <param name="interactionRange"> 상호작용 가능 범위
        /// <param name="interactableLayer"> 상호작용 가능 오브젝트 레이어
        /// <param name="obstacleLayer"> 상호작용을 막을 벽 오브젝트 레이어
        /// <param name="isInteract"> 상호작용 키를 눌렀는지
        /// </summary>
        public void HandleInteraction(
            Transform player,
            Transform view,
            float viewAngle,
            float interactionRange,
            LayerMask interactableLayer,
            LayerMask obstacleLayer,
            bool isInteract)
        {
            // InteractionManager 스크립트를 가진 오브젝트들 찾기
            var count = Physics.OverlapSphereNonAlloc(
                player.position,
                interactionRange,
                hits, interactableLayer,
                QueryTriggerInteraction.Collide);

            InteractionManager closest = null;
            var closestObjSqr = float.MaxValue;

            // 플레이어 시아 안에 들어오고, 플레이어와 가장 가까이 있는 상호작용 오브젝트 고르기
            for (var i = 0; i < count; i++)
            {
                if (!hits[i].TryGetComponent(out InteractionManager interactable)) continue;
                if (!IsInView(view, hits[i], viewAngle, obstacleLayer)) continue;

                var sqr = (interactable.transform.position - player.position).sqrMagnitude;
                if (sqr < closestObjSqr)
                {
                    closestObjSqr = sqr;
                    closest = interactable;
                }
            }
            
            // 상호작용 가능 오브젝트가 변할 때마다 상호작용 UI 활성/비활성화
            if (closest != current)
            {
                if (current != null) current.SetActiveInteractUI();
                if (closest != null) closest.SetActiveInteractUI();

                current = closest;
            }
            
            // 상호작용 키를 눌렀을 때 실행
            if (closest != null && isInteract)
            {
                closest.HandleInteraction();
            }
        }

        /// <summary>
        /// 플레이어 시야 안에 있는지 확인하는 메서드
        /// <param name="view"> 카메라 Transform
        /// <param name="target"> InteractionManager 스크립트를 가진 오브젝트
        /// <param name="viewAngle"> 시야각
        /// <param name="obstacleLayer"> 상호작용을 막을 벽 오브젝트 레이어
        /// </summary>
        private bool IsInView(Transform view, Collider target, float viewAngle, LayerMask obstacleLayer)
        {
            var targetPos = target.bounds.center;
            var direction = (targetPos - view.position).normalized;

            // 시야각 밖이면 제외
            var minDot = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
            if (Vector3.Dot(view.forward, direction) < minDot) return false;

            // 사이에 벽이 있으면 제외
            return !Physics.Linecast(view.position, targetPos, obstacleLayer, QueryTriggerInteraction.Ignore);
        }
    }
}
