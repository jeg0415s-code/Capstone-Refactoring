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
