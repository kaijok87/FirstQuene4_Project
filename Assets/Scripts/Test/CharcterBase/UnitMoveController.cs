using System.Collections;
using UnityEngine;

/// <summary>
/// 캐릭터 이동 로직 
/// </summary>
public class UnitMoveController : MonoBehaviour, IMoveBase
{
    /// <summary>
    /// 이동로직 적용할 트랜스폼
    /// </summary>
    Transform moveTarget;

    /// <summary>
    /// float 의 0 값인지 체크하기위한 변수
    /// </summary>
    float minFloatValue = 0.000001f;

    /// <summary>
    /// 캐릭터의 콜라이더 반지름 크기
    /// </summary>
    float colliderSize = 0.0f;

    /// <summary>
    /// 최종 이동위치에서 감산할 거리값 
    /// 타겟이있을경우 타겟 반지름 + 캐릭터반지름 값 없는경우 0으로셋팅
    /// </summary>
    float moveDistanceSubtractiveOperation;

    /// <summary>
    /// 캐릭터의 이동속도 
    /// </summary>
    [SerializeField]
    [Range(0.01f,20.0f)]
    float charcterMoveSpeed = 1.0f;

    /// <summary>
    /// 도착했는지 체크할 변수값
    /// </summary>
    float checkingInterval = 0.2f;

    /// <summary>
    /// 해당 캐릭터의 이동용 코루틴 로직담아둘 반복자 
    /// </summary>
    IEnumerator moveCoroutine;

    protected virtual void Awake()
    {
        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        colliderSize = collider.radius;
        moveCoroutine = CharcterMoveCoroutine(transform.position, 0.0f); //StopCoroutine Null레퍼런스 Expection 방지용
    }

    /// <summary>
    /// 초기값 셋팅하는 함수
    /// </summary>
    public void InitDataSetting() 
    {
        moveTarget = transform.parent;
    }


    /// <summary>
    /// 겹치지 않게 값을 셋팅하기
    /// 타겟의 반지름값을 가져와서 이동거리체크용으로 셋팅
    /// 빈값이면 타겟이없음으로 해당위치로 이동할수있게 값셋팅
    /// </summary>
    /// <param name="targetRadiusValue">타겟의 콜라이더 반지름크기</param>
    public void SetMoveDistanceSubtractiveOperation(float targetRadiusValue = 0.0f) 
    {
        if (targetRadiusValue > minFloatValue)  //값이 있는지 체크
        {
            moveDistanceSubtractiveOperation = colliderSize + targetRadiusValue;    //값셋팅
        }
        else   
        {
            moveDistanceSubtractiveOperation = targetRadiusValue; //빈값셋팅  
        }
        
    }

    /// <summary>
    /// 해당위치로 순간이동시키는 함수
    /// </summary>
    /// <param name="endPos">이동할 위치</param>
    public void Teleportation(Vector3 endPos) 
    {
        moveTarget.position = endPos;
    }

    /// <summary>
    /// 이동용 코루틴 실행 
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    /// <param name="distance">이동할 거리</param>
    public void OnMove(Vector3 direction, float distance) 
    {
        StopCoroutine(moveCoroutine);
        moveCoroutine = CharcterMoveCoroutine(direction,distance);
        StartCoroutine(moveCoroutine);
    }

    /// <summary>
    /// 캐릭터 이동을 멈추는 함수
    /// </summary>
    public void OnMovingStop()
    {
        StopCoroutine(moveCoroutine);
    }

    /// <summary>
    /// 캐릭터 이동속도 에 비례하여 
    /// 이동할 방향과 거리만큼 이동시키는 코루틴
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    /// <param name="distance">이동할 거리</param>
    /// <returns></returns>
    protected IEnumerator CharcterMoveCoroutine(Vector3 direction, float distance)
    {
        Vector3 endPos = moveTarget.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif

        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;                     // 이동할 거리 값

        while ((endPos - moveTarget.position).sqrMagnitude > checkValue)
        {
            moveTarget.Translate(Time.deltaTime * charcterMoveSpeed * direction, Space.World);       // 특정방향으로 이동속도만큼 이동시킨다.
            yield return null;                                                                      // 프레임마다.
        }

        if (moveDistanceSubtractiveOperation < minFloatValue)                                       // 타겟이 없는경우 
        {
            moveTarget.position = endPos;                                                            // 정확한 위치로 이동을 시킨다
        }
    }



    ///====================================== 리지드 바디 테스트 코드======================================
     
    /// <summary>
    /// 캐릭터의 물리연산처리용 리지드바디
    /// </summary>
    Rigidbody charcterRigidbody;

    private void Start()
    {
        charcterRigidbody = GetComponentInChildren<Rigidbody>();
    }

    /*
        Root Object 를 이동시키고 
        Child Object 에 RigidBody 를 넣어놓은 상태이면 
        리지드바디의 MovePosition 을 사용할수가없다.
        부모로 이동처리하는데 자식만 따로 노는 경우가 생긴다.
    */

    /// <summary>
    /// 이동할 방향과 거리만큼 이동시키는 함수
    /// 리지드 바디를 이용한다.
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    /// <param name="distance">이동할 거리</param>
    //public virtual void OnRigidMove(Vector3 direction, float distance)
    //{
    //    StopCoroutine(moveCoroutine);
    //    moveCoroutine = RigidBodyCharcterMove(direction, distance);
    //    StartCoroutine(moveCoroutine);
    //}


    /// <summary>
    /// 캐릭터 이동속도 에 비례하여 
    /// 이동할 방향과 거리만큼 이동시키는 코루틴
    /// 리지드 바디 를 이용한다.
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    /// <param name="distance">이동할 거리</param>
    //    protected virtual IEnumerator RigidBodyCharcterMove(Vector3 direction , float distance) 
    //    {
    //        Vector3 endPos = transform.position + (direction * distance);
    //#if UNITY_EDITOR
    //        gizmosEndPos = endPos;
    //#endif
    //        while ((endPos- transform.position).sqrMagnitude > checkingInterval) 
    //        {
    //            charcterRigidbody.MovePosition(charcterRigidbody.position + Time.deltaTime * charcterMoveSpeed * direction);
    //            yield return null;
    //        }
    //        transform.position = endPos;
    //    }


#if UNITY_EDITOR

    [SerializeField]
    Color gizmosColor = Color.black;

    Vector3 gizmosEndPos = Vector3.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        if(moveTarget != null)
        Gizmos.DrawLine(moveTarget.position , gizmosEndPos);
    }


#endif



}
