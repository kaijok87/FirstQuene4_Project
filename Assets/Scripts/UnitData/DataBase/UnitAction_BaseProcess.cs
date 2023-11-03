using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

/*
 1. 맴버 팀 매니저는 각 팀별(적군, 아군) 이 맴버를 관리하기위한 컴퍼넌트 
    1 - 1. 맴버 는 팀 매니저에서 추가가되어야 하고 추가될때 초기화 로직이 실행되야한다.
    1 - 2. 아군 맴버 는 죽었을때 팀매니저 관리용 변수에서 제거가되어야하고 Dead 관련 데이터에 저장되어야한다.
    1 - 3. 적군 맴버 는 항복했을때 아군 팀매니저 관리용 변수에 추가되어야하고 아군맴버로 변환되어야한다.
    1 - 4. 맴버는 이동로직을 따로 관리되고  이동명령(개별이동, 군체이동)을 실행시킬수있도록 되야한다. 해당 기능으로 모든 이동을 처리한다.
    1 - 5. 맴버는 회전로직을 따로 관리되고  회전명령을 실행시킬수 있도록 해야한다 .
    1 - 6. 팀매니저 오브젝트 하위에 맴버 오브젝트 들이 들어가있어야한다.
    1 - 7. 맴버 오브젝트에는 모델 오브젝트, 데이터 오브젝트 ,  기타 오브젝트 가 들어가야한다.
      1 - 7 - 1. 맴버 오브젝트의 모델 오브젝트와 데이터 오브젝트는 맴버 내용이 바뀔때마다 교체 되어야 한다. 
      1 - 7 - 2. 맴버 오브젝트의 위 두개의 오브젝트는 제외하고 다른 오브젝트들은 공통으로 사용될수있도록 만들어야한다.
     
 */
/*
 1- 7 
 
 */

/// <summary>
/// 유닛 동작관련 기본 컴퍼넌트 
/// 
/// 안쓴다 지울거 내용만 옮기자
/// </summary>
public class UnitAction_BaseProcess : MonoBehaviour
{
    /// <summary>
    /// 캐릭터의 이동을 관리할 컴포넌트
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;
    public IMoveBase CharcterMoveProcess => charcterMoveProcess;

    Collider charcterCollider;


    private void Awake()
    {
        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting(0.0f);
    }

    /// <summary>
    /// 캐릭터 이동을위한 작업을 진행한다.
    /// </summary>
    /// <param name="endPos">도착지점</param>
    /// <param name="radius">타겟이있는경우 타겟의 반지름값</param>
    private void CharcterMove(Vector3 direction, float distance, float radius = 0.0f)
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(radius);
        charcterMoveProcess.OnMove(direction, distance);
    }



    /// <summary>
    /// 군체의 집합신호를 받아서 집합위치로 이동시키는 로직
    /// <param name="originPos">이동시킬 기준점</param>
    /// </summary>
    public void OnAssemble(Vector3 originPos)
    {
        //charcterMoveProcess.SetMoveDistanceSubtractiveOperation(0.0f);
        //Vector3 dir = originPos + flockingDirectionPos - transform.position;
        //charcterMoveProcess.OnMove(dir.normalized, dir.magnitude);
    }

    /// <summary>
    /// 군체이동중 멈춰야할때 실행될 함수
    /// </summary>
    private void OnFlockingMovingStop()
    {
        charcterMoveProcess.OnMovingStop();
    }
  
    /// <summary>
    /// 밑의 이벤트함수를 이용해서 물리연산 터널링 현상을 어느정도는 막을수는 있지만 
    /// 1. 처음시작점이 충돌위치면 안되고
    /// 2. 충돌 도중에 뒤쪽에서  다른오브젝트가 충돌되서 밀려도 안된다.
    /// </summary>
    [SerializeField]
    float testValue = 0.05f;

    /// <summary>
    /// 충돌시작위치
    /// </summary>
    [SerializeField]
    Vector3 collisionStartPos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            collisionStartPos = transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            if (collisionStartPos != transform.position)
            {
                Vector3 tempPos = (collision.transform.position - transform.position).normalized;   // 방향백터의 노말값 구하고
                tempPos *= -testValue;                                                  // 노말값에 반지름크기값을 곱해서 콜라이더의 충돌시작위치지점을구하고
                                                                                        //tempPos =  other.transform.position - transform.position + tempPos;             // 해당물체와 충돌된 지점을 저장해둔다 
                tempPos.y = 0.0f;
                transform.position = collisionStartPos + tempPos;
            }
            collisionStartPos = transform.position;
        }
    }

}
