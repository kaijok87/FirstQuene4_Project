using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TranslateTest : TestBase
{
    [SerializeField]
    UnitMoveController testCharter;
    [SerializeField]
    Transform target_1;
    CapsuleCollider target_1_Collider;
    [SerializeField]
    Transform target_2;
    CapsuleCollider target_2_Collider;

    [SerializeField]
    BattleMapTeamManager flockingManager;

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

    protected override void Awake()
    {
        base.Awake();
        target_1_Collider = target_1.GetComponent<CapsuleCollider>();
        target_2_Collider = target_1.GetComponent<CapsuleCollider>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        if (flockingManager.MemberArray[0].UnitData == null) 
        {
            flockingManager.SetFlockingPosArray(flockingPos);
            flockingManager.InitData();
        }
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        flockingManager.ResetData();
    }
    protected override void Test3(InputAction.CallbackContext context)
    {
        flockingManager.OnAssemble();
    }
    protected override void Test4(InputAction.CallbackContext context)
    {
        flockingManager.SettingFlockingPos();
    }
    protected override void Test5(InputAction.CallbackContext context)
    {
        flockingManager.SettingFlockingPos();
    }
    protected override void TestRightClick(InputAction.CallbackContext context)
    {
        Vector3 screenPos = Mouse.current.position.value;
        screenPos.z = cameraZPos;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos); 
        flockingManager.OnFlockingMove(worldPos);
    }
    protected override void TestLeftClick(InputAction.CallbackContext context)
    {
        GameManager.Instance.GameSpeedType = tempSType;

    }
    /// <summary>
    /// ĳ���� �̵������� �۾��� �����Ѵ�.
    /// </summary>
    /// <param name="endPos">��������</param>
    /// <param name="radius">Ÿ�����ִ°�� Ÿ���� ��������</param>
    public virtual void CharcterMove(Vector3 endPos, float radius = 0.0f)
    {
        Vector3 dir = endPos - transform.position;
        testCharter.SetMoveDistanceSubtractiveOperation(radius);
        testCharter.OnMove(dir.normalized, dir.magnitude);
    }
}
