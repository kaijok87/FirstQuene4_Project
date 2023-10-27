using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleMapObjectPools 
{
    FlockingTeam,
    FlockingMember,
}


public class BattleMapFactory : DontDestroySingleton<BattleMapFactory>
{
    Pool_TeamObject teamObject_Pool;
    Pool_MemberObject memberObject_Pool;
    protected override void Awake()
    {
        base.Awake();
        teamObject_Pool = GetComponentInChildren<Pool_TeamObject>();
        memberObject_Pool = GetComponentInChildren<Pool_MemberObject>();
    }

    public PoolObjectBase GetObject(BattleMapObjectPools type) 
    {
        PoolObjectBase poolObject = null;
        switch (type)
        {
            case BattleMapObjectPools.FlockingTeam:
                poolObject = teamObject_Pool.GetPoolObject();
                break;
            case BattleMapObjectPools.FlockingMember:
                poolObject = memberObject_Pool.GetPoolObject();
                break;
            default:
                break;
        }
        return poolObject;
    }


    public PoolObjectBase GetObject(BattleMapObjectPools type, Transform setParent) 
    {
        PoolObjectBase poolObject = GetObject(type);
        poolObject.transform.SetParent(setParent);
        return poolObject;
    }

}
