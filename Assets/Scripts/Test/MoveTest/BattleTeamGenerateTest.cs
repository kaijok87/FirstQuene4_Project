using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

/// <summary>
/// 팀 생성 로직 테스트 
/// </summary>
public class BattleTeamGenerateTest : TestBase
{
    [SerializeField]
    int testInt;

    [SerializeField]
    int mapIndex;
    [SerializeField]
    int playerTeamLength;

    [SerializeField]
    int enemyTeamLength;

    [SerializeField]
    int npcTeamLength;

    [SerializeField]
    LayerMask layLayer;
    [SerializeField]
    BattleMapDataTable testMapData;


    [SerializeField]
    Vector3[] flockingPos =
       {
            new Vector3(0,0,0),new Vector3(3,0,0),new Vector3(6,0,0),new Vector3(9,0,0),
            new Vector3(12,0,0),new Vector3(-3,0,0),new Vector3(-6,0,0),new Vector3(-9,0,0),
            new Vector3(-12,0,0),new Vector3(2,0,1),new Vector3(2,0,4),new Vector3(2,0,7),
            new Vector3(2,0,10),new Vector3(-2,0,1),new Vector3(-2,0,4),new Vector3(-2,0,7),
            new Vector3(-2,0,10),new Vector3(5,0,0),new Vector3(5,0,2), new Vector3(5,0,5)

        };


    protected override void Awake()
    {
        base.Awake();
        layLayer = LayerMask.GetMask("BattleMapGround");
    }
    private void OnEnable()
    {
        inputSystem.BattleMap.LeftArrowKey.performed += leftKey;
        inputSystem.BattleMap.RightArrowKey.performed += rightKey;
    }

    private void InitData() 
    {
        testMapData = new();
        testMapData.TestInitDataSetting(testInt, flockingPos);
    }

    private void leftKey(InputAction.CallbackContext context)
    {

    }

    private void rightKey(InputAction.CallbackContext context)
    {

    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        BattleUnitGenerateManager.Instance.BattleDataSetting(mapIndex);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        BattleUnitGenerateManager.Instance.ResetData();
    }

    protected override void TestLeftClick(InputAction.CallbackContext context)
    {
        Vector2 clickPos = Mouse.current.position.value;
        Ray ray = Camera.main.ScreenPointToRay(clickPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f,layLayer)) 
        {
            Debug.Log(hit.point);
        }


    }

    protected override void TestRightClick(InputAction.CallbackContext context)
    {

    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        InitData();
    }
    protected override void Test4(InputAction.CallbackContext context)
    {
        MapDataGenerateManager.Instance.SaveData(testMapData);
    }
    protected override void Test5(InputAction.CallbackContext context)
    {
        MapDataGenerateManager.Instance.LoadDataAsync();
    }
   

}

