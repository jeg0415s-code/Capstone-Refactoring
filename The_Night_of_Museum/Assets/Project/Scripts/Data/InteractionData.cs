using System;
using UnityEngine;

/// <summary>
/// 상호작용 오브젝트 타입별 변수값
/// </summary>

[Serializable]
public class BatteryData
{
    [Tooltip("배터리 충전량")]
    public float chargeAmount = 20f;
}

[Serializable]
public class CollectionData
{
    public enum CollectionType
    {
        Real,
        Fake
    }

    [Tooltip("콜렉션 타입")]
    public CollectionType collectionType = CollectionType.Real;
}

[Serializable]
public class CabinetData
{
    [Tooltip("플레이어가 숨겨지는 캐비넷 위치")]
    public Transform hidePoint = null;
}

[Serializable]
public class SafeData
{
}

[Serializable]
public class StatueData
{
}

[Serializable]
public class ExitDoorData
{
}
