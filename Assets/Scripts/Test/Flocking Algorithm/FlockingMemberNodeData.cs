using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class FlockingMemberNodeData : PoolObjectBase , IControllObject
{
    [SerializeField]
    /// <summary>
    /// 현재 맴버가 리더인지 체크할 변수
    /// </summary>
    bool isLeader = false;

    /// <summary>
    /// 군체를 관리할 메니저
    /// </summary>
    FlockingManager parentNode;
    
    /// <summary>
    /// 유닛을 풀로 돌리기위한 오브젝트 
    /// </summary>
    PoolObj_Unit unitObject;

    /// <summary>
    /// 캐릭터의 이동을 관리할 컴포넌트
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;

    /// <summary>
    /// 군체의 기준점에서 얼마나떨어졌는지에대한 좌표값
    /// 기준점에서 해당 맴버의 위치를 잡기위한 연산값
    /// </summary>
    Vector3 flockingDirectionPos;

    /// <summary>
    /// 군체의 인덱스 번호
    /// </summary>
    int flockingIndex = -1;

    /// <summary>
    /// 군체의 인덱스를 셋팅 
    /// </summary>
    public int ArrayIndex => flockingIndex;

    /// <summary>
    /// 유닛의 데이터 를 관리할 인터페이스
    /// </summary>
    IUnitDataBase unitData;
    public IUnitDataBase UnitData => unitData;

    /// <summary>
    /// 리더 인지 설정하는 프로퍼티
    /// </summary>
    public bool IsLeader 
    {
        get => isLeader; 
        set => isLeader = value; 
    }


    /// <summary>
    /// 유닛이 죽었을때 신호보낼 델리게이트
    /// </summary>
    public Action<IControllObject> onDie { get ; set ; }


    /// <summary>
    /// 해당 군체 맴버데이터및 델리게이터를 초기화할 함수
    /// </summary>
    /// <param name="parnetNode">군체의 중심부</param>
    /// <param name="unitObject">맴버에담긴 유닛오브젝트</param>
    /// <param name="index">군체에서의 인덱스값</param>
    public void InitDataSetting(FlockingManager parnetNode, PoolObj_Unit unitObject, int index) 
    {
        this.parentNode = parnetNode;
        this.flockingIndex = index;
        this.unitObject = unitObject;

        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting();
        
        unitData = GetComponentInChildren<IUnitDataBase>();
        unitData.InitDataSetting();
        unitData.onDie = OnDie;
    }

    /// <summary>
    /// 캐릭터 이동을위한 작업을 진행한다.
    /// </summary>
    /// <param name="endPos">도착지점</param>
    /// <param name="radius">타겟이있는경우 타겟의 반지름값</param>
    private void CharcterMove(Vector3 direction, float distance , float radius = 0.0f)
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(radius);
        charcterMoveProcess.OnMove(direction, distance);
    }

   

    /// <summary>
    /// 군체의 집합신호를 받아서 집합위치로 이동시키는 로직
    /// </summary>
    private void OnAssemble() 
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(0.0f);
        Vector3 dir = flockingDirectionPos - transform.position;
        charcterMoveProcess.OnMove(dir.normalized, dir.sqrMagnitude);
    }

    /// <summary>
    /// 군체이동중 멈춰야할때 실행될 함수
    /// </summary>
    private void OnFlockingMovingStop() 
    {
        charcterMoveProcess.OnMovingStop();
    }

    /// <summary>
    /// 유닛이 죽었을때 실행할 함수
    /// </summary>
    private void OnDie() 
    {
        onDie?.Invoke(this);    //죽었다고 신호보내고 
        DataReset();
    }

    /// <summary>
    /// 초기화할 공통 데이터
    /// </summary>
    private void DataReset() 
    {
        unitObject.ResetData();     // 유닛 풀로 돌리고 
        unitObject = null;          // 값 초기화 

        charcterMoveProcess = null; //
        unitData.onDie = null;
        unitData = null;

        isLeader = false;
        parentNode = null;
        flockingDirectionPos = Vector3.zero;
        flockingIndex = -1;
    }

    /// <summary>
    /// 초기 데이터값으로 돌리기위한 함수
    /// </summary>
    public override void ResetData()
    {
        if (unitObject != null) //유닛이 있는 것들은 
        {
            DataReset();
        }
        base.ResetData();            //맴버도 풀로돌린다.
    }
}
