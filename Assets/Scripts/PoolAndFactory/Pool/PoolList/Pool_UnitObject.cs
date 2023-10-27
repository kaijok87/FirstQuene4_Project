using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_UnitObject : PoolBase<UnitBaseNode>
{
    public override void SetPrefab(UnitBaseNode prefab)
    {
        poolPrefab = prefab;
        capasity = prefab.Capacity;
    }
}
