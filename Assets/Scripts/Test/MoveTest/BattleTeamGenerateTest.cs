using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �� ���� ���� �׽�Ʈ 
/// </summary>
public class BattleTeamGenerateTest : TestBase
{
    [SerializeField]
    int playerTeamLength;

    [SerializeField]
    int enemyTeamLength;

    [SerializeField]
    int npcTeamLength;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Test1(InputAction.CallbackContext context)
    {
        //BattleMapFactory.Instance.
    }
}
