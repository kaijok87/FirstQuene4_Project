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
/// 군체 맴버 데이터
/// </summary>
public class TeamMember : PoolObjectBase, IControllObject
{
    /// <summary>
    /// 군체의 인덱스 번호
    /// </summary>
    [SerializeField]
    int flockingIndex = -1;

    /// <summary>
    /// 군체의 인덱스를 셋팅 
    /// </summary>
    public int ArrayIndex => flockingIndex;
    
    [SerializeField]
    /// <summary>
    /// 현재 맴버가 리더인지 체크할 변수
    /// </summary>
    bool isLeader = false;

    /// <summary>
    /// 리더 인지 설정하는 프로퍼티
    /// </summary>
    public bool IsLeader
    {
        get => isLeader;
        set
        {
            if (value)
            {
                parentNode.onAssemble -= OnAssemble; //리더인경우 호출 로직이 실행안되야함으로 액션에서 제외
                isLeader = value;
            }
        }
    }
    /// <summary>
    /// 군체를 관리할 메니저
    /// </summary>
    TeamObject parentNode;

    /// <summary>
    /// 캐릭터의 이동을 관리할 컴포넌트
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;
    public IMoveBase CharcterMoveProcess => charcterMoveProcess;


    /// <summary>
    /// 캐릭터 회전을 관리할 컴포넌트
    /// </summary>
    [SerializeField]
    IRotateBase charcterRotateProcess;
    IRotateBase CharcterRotateProcess => charcterRotateProcess;



    /// <summary>
    /// 군체의 기준점에서 얼마나떨어졌는지에대한 좌표값
    /// 기준점에서 해당 맴버의 위치를 잡기위한 연산값
    /// </summary>
    Vector3 flockingDirectionPos;
    public Vector3 FlockingDirectionPos
    {
        get => flockingDirectionPos;
        set => flockingDirectionPos = value;
    }


    /// <summary>
    /// 유닛의 데이터 를 관리할 인터페이스
    /// </summary>
    IUnitDefaultBase unitData;
    public IUnitDefaultBase UnitData => unitData;

    /// <summary>
    /// 유닛이 죽었을때 신호보낼 델리게이트
    /// </summary>
    public Action<IControllObject> onDie { get; set; }


    /// <summary>
    /// 맴버유닛의 기본 콜라이더 
    /// </summary>
    CapsuleCollider capCollider;
    float colliderRadius = 0.0f;

    CharacterController cc;

    Collider[] thisColliders;

    private void Awake()
    {
        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting(colliderRadius);

        charcterRotateProcess = GetComponentInChildren<IRotateBase>();
        charcterRotateProcess.InitDataSetting();
        //capCollider = GetComponent<CapsuleCollider>();
        //thisColliders = GetComponentsInChildren<Collider>();
        //stateList = new(10000);
        //cc = GetComponent<CharacterController>();
        //if (cc != null)
        //{
        //    colliderRadius = cc.radius;
        //}
        //else if () {
        //{
        //    colliderRadius = capCollider.radius;
        //}
    }

    /// <summary>
    /// 해당 군체 맴버데이터및 델리게이터를 초기화할 함수
    /// </summary>
    /// <param name="parentNode">군체의 중심부</param>
    /// <param name="unitObject">유닛의 데이터</param>
    /// <param name="index">군체에서의 인덱스값</param>
    public void InitDataSetting(TeamObject parentNode, IUnitDefaultBase unit, int index)
    {
        
        flockingIndex = index;
        this.parentNode = parentNode;

        /// 유닛 데이터 연결 
        unitData = unit;
        unitData.onDie = OnDie;

        /// 맴버에서 들어오는 액션 연결
        parentNode.onAssemble += OnAssemble;
        parentNode.onMove += CharcterMove;
        parentNode.onRotate += CharcterRotate;
        parentNode.onStop += OnFlockingMovingStop;

        /// 부모 트랜스폼 위치에 맞추기위해 자식 오브잭트 위치 초기화 
        //unit.transform.position = Vector3.zero;
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
    /// 캐릭터 회전 시키기
    /// </summary>
    /// <param name="endPos">도착지점 </param>
    private void CharcterRotate(Vector3 endPos) 
    {
        charcterRotateProcess.OnRotateRealTime(endPos-transform.position);
    }

    /// <summary>
    /// 군체의 집합신호를 받아서 집합위치로 이동시키는 로직
    /// <param name="originPos">이동시킬 기준점</param>
    /// </summary>
    public void OnAssemble(Vector3 originPos)
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(0.0f);
        Vector3 dir = originPos + flockingDirectionPos - transform.position;
        charcterMoveProcess.OnMove(dir.normalized, dir.magnitude);
    }

    /// <summary>
    /// 군체이동중 멈춰야할때 실행될 함수
    /// </summary>
    private void OnFlockingMovingStop()
    {
        charcterMoveProcess.OnMovingStop();
    }

    /// <summary>
    /// 군체 정렬기준점을 자신의위치로 갱신하는 함수
    /// </summary>
    public Vector3 SetFlockingDirectionPos()
    {
        return flockingDirectionPos = transform.position;
    }

    /// <summary>
    /// 유닛이 죽었을때 실행할 함수
    /// </summary>
    private void OnDie()
    {
        onDie?.Invoke(this);    //죽었다고 신호보내고 
        ResetData();
    }


    /// <summary>
    /// 초기 데이터값으로 돌리기위한 함수
    /// 항상들고다닐것이기때문에 풀로 돌리는 작업은 제외시켰다.
    /// 사용처 유닛 죽었을때와 게임 로드및 타이틀 이동과같이 전체데이터 초기화시 사용 
    /// </summary>
    public override void ResetData()
    {
        if (unitData != null)
        {
            unitData.ResetData();     // 유닛 풀로 돌리고 
            UnitDataReset();            // 데이터 리셋 
        }
        MemberDataReset();
        //OffCollider();
    }

    /// <summary>
    /// 맴버 전용 데이터 리셋함수
    /// </summary>
    private void MemberDataReset()
    {
        isLeader = false;
        flockingDirectionPos = Vector3.zero;
        flockingIndex = -1;
        transform.position = Vector3.zero;
    }

    /// <summary>
    /// 맴버에 연결된 유닛에대한 데이터 리셋하는 함수
    /// </summary>
    private void UnitDataReset() 
    {
        unitData.onDie = null;
        unitData = null;
        parentNode.onAssemble -= OnAssemble;
        parentNode.onMove -= CharcterMove;
        parentNode.onStop -= OnFlockingMovingStop;
        parentNode.onRotate -= CharcterRotate;
        parentNode = null;
        charcterMoveProcess.OnMovingStop();
        charcterMoveProcess = null;
    } 
    

    /// <summary>
    /// 항복시 실행할 데이터 리셋함수
    /// </summary>
    public void SurrenderDataReset()
    {
        UnitDataReset();
        MemberDataReset();
    }

    /// <summary>
    /// 충돌시작위치
    /// </summary>
    [SerializeField]
    Vector3 collisionStartPos;
  
    enum CollisionState 
    {
        None = 0,
        Te,
        Ts,
        Tx,
        Ce,
        Cs,
        Cx
    }
    List<CollisionState> stateList;
    [SerializeField]
    CollisionState[] collisionStateArray;
    public void SeTList() 
    {
        collisionStateArray = stateList.ToArray();
    }
    public void ResetList()
    {
        stateList.Clear();
        collisionStateArray = null;
    }
  
    /*
     
     */

    /// <summary>
    /// 밑의 이벤트함수를 이용해서 물리연산 터널링 현상을 어느정도는 막을수는 있지만 
    /// 1. 처음시작점이 충돌위치면 안되고
    /// 2. 충돌 도중에 뒤쪽에서  다른오브젝트가 충돌되서 밀려도 안된다.
    /// </summary>
    [SerializeField]
    float testValue = 0.05f;

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
