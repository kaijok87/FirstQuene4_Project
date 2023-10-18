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

    protected override void Awake()
    {
        base.Awake();
        target_1_Collider = target_1.GetComponent<CapsuleCollider>();
        target_2_Collider = target_1.GetComponent<CapsuleCollider>();
    }

    protected override void Test1(InputAction.CallbackContext context)
    {
        CharcterMove(target_1.position, target_1_Collider.radius);
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        CharcterMove(target_2.position, target_2_Collider.radius);
    }
    protected override void Test3(InputAction.CallbackContext context)
    {
        CharcterMove(Vector3.zero);
    }
    protected override void Test4(InputAction.CallbackContext context)
    {
        flockingManager.InitData();
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
        testCharter.OnMove(dir.normalized, dir.sqrMagnitude);
    }
}
