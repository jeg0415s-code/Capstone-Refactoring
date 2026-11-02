using UnityEngine;

namespace Runtime.Player
{
    public class PlayerHeadBob
    {
        float bobbingTimer = 0f;

        /// <summary>
        /// 플레이어가 이동 할 때 카메라 흔들림
        /// <param name="camera"> 플레이어 카메라 트랜스폼
        /// <param name="isMoving"> 플레이어가 움직이고 있는지
        /// <param name="defaultEyeHeight"> 기본 카메라 눈높이
        /// <param name="speed"> 플레이어 이동속도
        /// <param name="deltaTime"> Time.deltaTime
        /// </summary>
        public void HandleHeadBobbing(Transform camera, bool isMoving, float defaultEyeHeight, float speed, float deltaTime)
        {
            if (camera == null) return;

            if (isMoving)
            {
                var amount = speed * 0.01f;
                bobbingTimer += speed * deltaTime;

                Vector3 local = camera.localPosition;
                camera.localPosition = new Vector3(local.x, defaultEyeHeight + Mathf.Sin(bobbingTimer) * amount, local.z);

                return;
            }

            bobbingTimer = 0f;

            Vector3 current = camera.localPosition;
            Vector3 target = new Vector3(current.x, defaultEyeHeight, current.z);
            camera.localPosition = Vector3.Lerp(current, target, deltaTime * 5f);
        }
    }
}
