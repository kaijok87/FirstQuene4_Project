using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleMapPoolNames 
{
    FlockingMain,
    FlockingMember,
    FlockingUnit,
}

public class BattleMapFactory : DontDistroySingleton<BattleMapFactory>
{
    Pool_FlockingMember flockingMember;
    Pool_Unit flockingUnit;
    private void Awake()
    {
        flockingMember = GetComponentInChildren<Pool_FlockingMember>();
        flockingUnit = GetComponentInChildren<Pool_Unit>();
    }

    public PoolObjectBase GetObject(BattleMapPoolNames type) 
    {
        PoolObjectBase poolObject = null;
        switch (type)
        {
            case BattleMapPoolNames.FlockingMain:
                poolObject = null;
                break;
            case BattleMapPoolNames.FlockingMember:
                poolObject = flockingMember.GetPoolObject();
                break;
            case BattleMapPoolNames.FlockingUnit:
                poolObject = flockingUnit.GetPoolObject();
                break;
            default:
                break;
        }
        return poolObject;
    }

    public PoolObjectBase GetObject(BattleMapPoolNames type, Transform setParent) 
    {
        PoolObjectBase poolObject = GetObject(type);
        poolObject.transform.SetParent(setParent);
        return poolObject;
    }
}
