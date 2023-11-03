using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 캐릭터 이동 로직 
/// 좌표로 하고있는데 
/// 인제 타일좌표로 이동로직변경을해야된다.
/// </summary>
public class UnitMoveController : MonoBehaviour, IMoveBase
{
    /// <summary>
    /// float 의 0 값인지 체크하기위한 변수
    /// </summary>
    float minFloatValue = 0.000001f;

    /// <summary>
    /// 캐릭터의 콜라이더 반지름 크기
    /// </summary>
    float colliderSize = 0.0f;

    /// <summary>
    /// 충돌한 장애물의 콜라이더 반지름 크기
    /// </summary>
    //float propCollisionSize = 0.0f;

    /// <summary>
    /// 최종 이동위치에서 감산할 거리값 
    /// 타겟이있을경우 타겟 반지름 + 캐릭터반지름 값 없는경우 0으로셋팅
    /// </summary>
    float moveDistanceSubtractiveOperation;

    /// <summary>
    /// 도착했는지 체크할 변수값
    /// </summary>
    float checkingInterval = 0.2f;

    /// <summary>
    /// 해당 캐릭터의 이동용 코루틴 로직담아둘 반복자 
    /// </summary>
    IEnumerator moveCoroutine;

    IUnitDefaultBase unitData;

    protected virtual void Awake()
    {
        moveCoroutine = RigidBodyCharcterMove(transform.position, 0.0f);
        charcterRigidbody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// 초기값 셋팅하는 함수
    /// <param name="colliderRadius">내자신의 콜라이더 반지름크기</param>
    /// </summary>
    public void InitDataSetting(float colliderRadius) 
    {
        
        colliderSize = colliderRadius;
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
        transform.position = endPos;
    }

    /// <summary>
    /// 이동용 코루틴 실행 
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    /// <param name="distance">이동할 거리</param>
    public void OnMove(Vector3 direction, float distance) 
    {
        OnRigidMove(direction,distance);
        //return;
        //StopCoroutine(moveCoroutine);
        //moveCoroutine = CharcterMoveCoroutine(direction, distance);
        //StartCoroutine(moveCoroutine);
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
        Vector3 endPos = transform.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif

        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;                     // 이동할 거리 값
        float timeDeltaValue = 0.0f;
        float dirMag = (endPos - transform.position).sqrMagnitude;
        float tempValue = 0.0f;
        while (dirMag > checkValue)
        {
            timeDeltaValue = Time.deltaTime;
            transform.Translate(timeDeltaValue * unitData.GetUnitMoveRange() * direction, Space.World);     // 특정방향으로 이동속도만큼 이동시킨다.
            
            tempValue = (endPos - transform.position).sqrMagnitude;    //진행상황임시로저장하고 

            if (dirMag < tempValue) //이동간격은 좁아져야하는데  거리가 넓어졌다는것은 도착점을 지나쳤다는것이니 
            {
                direction = (endPos - transform.position).normalized; //방향을 다시잡는다
            }
            dirMag = tempValue;  //체크할 변수에 셋팅한다.
            transform.localPosition = Vector3.zero;
            yield return null;                                                                      // 프레임마다.
        }

        if (moveDistanceSubtractiveOperation < minFloatValue)                                       // 타겟이 없는경우 
        {
            transform.position = endPos;                                                            // 정확한 위치로 이동을 시킨다
        }
    }



    ///====================================== 리지드 바디 테스트 코드======================================
     
    /// <summary>
    /// 캐릭터의 물리연산처리용 리지드바디
    /// </summary>
    Rigidbody charcterRigidbody;

    WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();

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
    public virtual void OnRigidMove(Vector3 direction, float distance)
    {
        //isRigid = true;
        //direction_Value = direction ;
        //endPos = transform.position + (direction * distance);
        //tempValue = (endPos - transform.position).sqrMagnitude;
        //a = tempValue;
        StopCoroutine(moveCoroutine);
        moveCoroutine = RigidBodyCharcterMove(direction, distance);
        StartCoroutine(moveCoroutine);
    }

    //bool isRigid = false;
    //Vector3 direction_Value;
    //Vector3 endPos;
    //float tempValue;
    //float a;
    //private void FixedUpdate()
    //{
    //    if (isRigid) 
    //    {
    //        charcterRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * charcterMoveSpeed * direction_Value);
    //        tempValue = (endPos - transform.position).sqrMagnitude;
    //        if (tempValue < a) 
    //        {
    //            direction_Value = (endPos - transform.position).normalized;
    //        }
    //        a = tempValue;
    //    }
    //}

    /// <summary>
    /// 캐릭터 이동속도 에 비례하여 
    /// 이동할 방향과 거리만큼 이동시키는 코루틴
    /// 리지드 바디 를 이용한다.
    /// 벽뚧 버그 어떻게 안되나?? 뒤에서 밀면 뚧어버리네..
    /// </summary>
    /// <param name="direction">이동할 방향</param>
    /// <param name="distance">이동할 거리</param>
    protected virtual IEnumerator RigidBodyCharcterMove(Vector3 direction, float distance)
    {
        Vector3 endPos = transform.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif
        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;
        //Debug.Log($"start :{(endPos-transform.position).sqrMagnitude} > {checkValue}");
        float sqlMagnitudeValue = (endPos - transform.position).sqrMagnitude;  // 도착했는지에대한 현재 진행상황 저장할변수
        float tempValue = 0.0f;     //방향을 다시잡아야할때 사용할 변수
        while (sqlMagnitudeValue  > checkValue)
        {
            //charcterRigidbody.MovePosition(transform.position + tempValue * propCollisionSize * direction);      // 충돌한 장애물 반지름 만큼 밀리도록 만든다.
            charcterRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * unitData.GetUnitMoveSpeed() * direction);
            yield return fixedWait;
            Debug.Log(charcterRigidbody.velocity);
            tempValue = (endPos - transform.position).sqrMagnitude;    //진행상황임시로저장하고 
            if (sqlMagnitudeValue  < tempValue) //이동간격은 좁아져야하는데  거리가 넓어졌다는것은 도착점을 지나쳤다는것이니 
            {
                direction = (endPos - transform.position).normalized; //방향을 다시잡는다
            }
            sqlMagnitudeValue = tempValue;  //체크할 변수에 셋팅한다.
            transform.localPosition = Vector3.zero;
        }
        //Debug.Log($"end :{(endPos - transform.position).sqrMagnitude} > {checkValue}");
        charcterRigidbody.MovePosition(endPos);
    }




  

#if UNITY_EDITOR

    [SerializeField]
    Color gizmosColor = Color.black;

    [SerializeField]
    float moveRange = 5.0f;
    [SerializeField]
    float attackRange = 8.0f;
    Vector3 gizmosEndPos = Vector3.zero;


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;



        if (transform != null)
        {
            Gizmos.DrawLine(transform.position, gizmosEndPos);
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, transform.up, moveRange);

            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, attackRange);
        }
        else 
        {
            Gizmos.color= Color.clear;
        }

    }




#endif



}
