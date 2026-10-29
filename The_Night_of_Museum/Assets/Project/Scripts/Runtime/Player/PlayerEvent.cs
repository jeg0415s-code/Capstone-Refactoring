using System;

namespace Runtime.Player
{
    /// <summary>
    /// 플레이어 관련 이벤트 클래스
    /// </summary>
    public static class PlayerEvent
    {
        public static event Action<bool> MovementEnabledChanged; // 플레이어 이동 가능 여부 관련 이벤트

        public static void SetMovementEnabled(bool enabled) => MovementEnabledChanged?.Invoke(enabled);
    }
}
