using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class TranslateTest : TestBase
{
    [SerializeField]
    int mapIndex = 0;

    [SerializeField]
    UnitMoveController testCharter;
    [SerializeField]
    Transform target_1;
    CapsuleCollider target_1_Collider;
    [SerializeField]
    Transform target_2;
    CapsuleCollider target_2_Collider;

    [SerializeField]
    TeamBaseNode flockingManager;

    [SerializeField]
    float cameraZPos = 0.0f;

    [SerializeField]
    GameSpeedType tempSType;

    [SerializeField]
    Vector3[] flockingPos =
        {
            new Vector3(0,0,0),new Vector3(3,0,0),new Vector3(6,0,0),new Vector3(9,0,0),
            new Vector3(12,0,0),new Vector3(-3,0,0),new Vector3(-6,0,0),new Vector3(-9,0,0),
            new Vector3(-12,0,0),new Vector3(2,0,1),new Vector3(2,0,4),new Vector3(2,0,7),
            new Vector3(2,0,10),new Vector3(-2,0,1),new Vector3(-2,0,4),new Vector3(-2,0,7),
            new Vector3(-2,0,10),new Vector3(5,0,0),new Vector3(5,0,2), new Vector3(5,0,5)

        };
    UnitData[] testCreateUnitArray;
    [SerializeField]
    UnitBaseNode[] unitTests;
    UnitSearchController testSearch;
    
    protected override void Awake()
    {
        base.Awake();
        target_1_Collider = target_1.GetComponent<CapsuleCollider>();
        target_2_Collider = target_2.GetComponent<CapsuleCollider>();
        for (int i = 0; i < unitTests.Length; i++)
        {
            if (unitTests[i] != null)
            {
                //testCreateUnitArray[i] = unitTests[i].UnitData;
            }
        }
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        BattleUnitGenerateManager.Instance.BattleDataSetting(mapIndex);



    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        flockingManager.ResetData();
    }
    protected override void Test3(InputAction.CallbackContext context)
    {
        //flockingManager.OnAssemble();
    }
    protected override void Test4(InputAction.CallbackContext context)
    {
        flockingManager.SettingFlockingPos();
    }
    protected override void Test5(InputAction.CallbackContext context)
    {
        //flockingManager.SetFlockingPosArray(flockingPos);
        //foreach (UnitAction_BaseProcess bt in flockingManager.MemberArray)
        //{
        //    if (bt != null)
        //    {
        //        //bt.ResetList();
        //    }
        //}

    }
    protected override void TestRightClick(InputAction.CallbackContext context)
    {
        GameManager.Instance.GameSpeedType = tempSType;
        Vector3 screenPos = Mouse.current.position.value;
        screenPos.z = cameraZPos;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        //flockingManager.OnFlockingMove(worldPos);
    }
    protected override void TestLeftClick(InputAction.CallbackContext context)
    {
        //foreach (UnitAction_BaseProcess bt in flockingManager.MemberArray) 
        //{
        //    if (bt != null) 
        //    {
        //        //bt.SeTList();
        //    }
        //}

        //if (testSearch != null) 
        //{
        //    Debug.Log( testSearch.GetSearchTarget());
        //}

    }
    /// <summary>
    /// 캐릭터 이동을위한 작업을 진행한다.
    /// </summary>
    /// <param name="endPos">도착지점</param>
    /// <param name="radius">타겟이있는경우 타겟의 반지름값</param>
    public virtual void CharcterMove(Vector3 endPos, float radius = 0.0f)
    {
        Vector3 dir = endPos - transform.position;
        testCharter.SetMoveDistanceSubtractiveOperation(radius);
        testCharter.OnMove(dir.normalized, dir.magnitude);
    }
}
