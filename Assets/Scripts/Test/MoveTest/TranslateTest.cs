using System.Collections;
using System.Data.Common;
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
    FlockingManager flockingManager;

    [SerializeField]
    float cameraZPos = 0.0f;
    protected override void Awake()
    {
        base.Awake();
        target_1_Collider = target_1.GetComponent<CapsuleCollider>();
        target_2_Collider = target_1.GetComponent<CapsuleCollider>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        flockingManager.InitData();
  
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
