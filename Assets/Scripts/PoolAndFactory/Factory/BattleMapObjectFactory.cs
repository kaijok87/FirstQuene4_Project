using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleMapObjectFactory : DontDestroySingleton<BattleMapObjectFactory>
{
    /// <summary>
    /// �� ���� ���н�ų ������Ʈ Ǯ
    /// </summary>
    Pool_TeamObject teamObject_Pool;


    protected override void Awake()
    {
        base.Awake();
        teamObject_Pool = GetComponentInChildren<Pool_TeamObject>();
    }

    public ITeamUnit GetTeamUnit()
    {
        return teamObject_Pool.GetPoolObject();
    }
}
