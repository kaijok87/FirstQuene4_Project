using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메인캐릭터(리더) 의 숨겨진능력치 
/// 1. 리더 캐릭터 에 의한 속도 가산 
/// 2. 리더 캐릭터 에 의한 피로도 수치감소 
/// 3. 
/// </summary>
public struct HiddenLeaderAbility 
{
    /// <summary>
    /// 이동속도 가산치
    /// </summary>
    float moveSpeed;

    /// <summary>
    /// 피로도 감산치
    /// </summary>
    float fatigueValue;

    public HiddenLeaderAbility(float moveSpeed, float fatigueValue) 
    {
        this.moveSpeed = moveSpeed;
        this.fatigueValue = fatigueValue;
    }
}

/// <summary>
/// 기능 정리
/// 1. 군체 기준점이 제거되면 해당인덱스 기준으로 가까운 값 +- 를 비교해서 가까운값을 기준점으로 돌리는 기능
///  
/// 2. 군체 맴버에 유닛이 등록됬는지 체크하는 로직을위해 체크할 값을 정해야한다 현재 transform 으로 기본 셋팅해놨다. 비트로 비교할수있으면 해보도록하자 
/// </summary>
public class FlockingManager : MonoBehaviour
{
    /// <summary>
    /// 히든 속성 정보
    /// </summary>
    HiddenLeaderAbility mainUnitHiddenAbility;
    public HiddenLeaderAbility MainUnitHiddenAbility => mainUnitHiddenAbility;

    /// <summary>
    /// 리더 유닛 기본적으로 0번의 유닛이다.
    /// </summary>
    IControllObject leaderUnit;

    /// <summary>
    /// 군체의 중심에 있는 기준점이될 유닛의 위치 
    /// </summary>
    public IControllObject FlockingCenterPos => leaderUnit;

    [SerializeField]
    int memberCapacity = 18;

    /// <summary>
    /// 해당군체가 가지고있는 맴버 배열
    /// 여기에 들어가는 배열 순서위치의 값들은 셋팅하는 법이 3종류 있다
    /// 1. 전투맵에서 해당 맴버의 유닛이 죽었을경우 제거 
    /// 2. 전투맵에서 적군의 유닛이 항복해서 아군유닛으로 편입할때 배열이 비어있으면 추가 
    /// 3. 편성 화면에서 편성한내용을 적용 (해당내용은 수정이완료됬을때 한번만한다)
    /// </summary>
    IControllObject[] memberArray;

    /// <summary>
    /// 군체이동시킬 유닛이 실질적으로 처리될 리스트
    /// 배틀맵에서 군체 이동 연산처리용으로 사용할 리스트 
    /// 1. 배틀맵에서 memberArray 값이 수정될경우 해당리스트 내용도 수정이되야한다.
    /// 2. 편성 화면에서 편성한내용을 해당리스트에도 적용하도록한다. (해당내용은 수정이완료됬을때 한번만한다)
    ///  2-1.이시점에서 군체의 포지션 종류내용을 읽어서 icontrollObject 들의 간격값을 연결해서 넣어둔다.
    ///  
    /// </summary>
    List<IControllObject> livingMemberList;






    /// <summary>
    /// 정해진 군체 모양으로 집합하라고 신호를 보내는 델리게이트
    /// </summary>
    public Action onAssemble;

    /// <summary>
    /// 군체 배열의 이동신호를 보낸다.
    /// 이동할 방향과 이동거리 및 타겟이 존재하면 타겟의 반지름을 보낸다.
    /// </summary>
    public Action<Vector3, float, float> onMove;

    /// <summary>
    /// 군체 맴버들의 이동을 멈추라고 신호를 보낸다.
    /// </summary>
    public Action onStop;

    /// <summary>
    /// 군체 맴버 셋팅하는 함수 
    /// 매번 새롭게 넣기때문에 자주실행하면 안좋다.
    /// </summary>
    public void InitData()
    {

        memberArray = new IControllObject[memberCapacity];
        int forSize = memberArray.Length;
        PoolObj_Unit unitObject;
        for (int i = 0; i < forSize; i++)
        {
            memberArray[i] = (IControllObject)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingMember,transform);
            unitObject = (PoolObj_Unit)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingMember, memberArray[i].transform);
            memberArray[i].InitDataSetting(this, unitObject , i);
            AddAction(memberArray[i]);
        }
        livingMemberList = new List<IControllObject>(memberArray);
        leaderUnit = memberArray[0]; //군체를 컨트롤할 기준점이될 유닛 셋팅
        memberArray[0].IsLeader = true; //리더셋팅
    }
    /// <summary>
    /// 셋팅된 데이터 초기화 
    /// </summary>
    public void ResetData() 
    {
        foreach (IControllObject member in livingMemberList)
        {
            member.onDie = null;
            member.ResetData();
        }
        onAssemble = null;
        onMove = null;
        onStop = null;
        leaderUnit = null;
        memberArray = null;
        livingMemberList.Clear();
    }
    /// <summary>
    /// 유닛이 죽을때 처리 하기위한 함수 
    /// 배열및 리스트에서 제거 및 리더 가죽었으면 리더변경
    /// 모든 유닛이 죽었으면 처리할로직 
    /// </summary>
    /// <param name="unit">죽을 유닛</param>
    private void UnitDieAndNextLeaderSetting(IControllObject unit)
    {
        livingMemberList.Remove(unit);
        memberArray[unit.ArrayIndex] = null;

        DeleteAction(unit);
        
        if (unit.IsLeader)  //죽은 유닛이 리더면 
        {
            if (livingMemberList.Count > 0) //살아있는 유닛이 있을때  
            {
                livingMemberList[0].IsLeader = true; //맨처음 유닛을 리더로 변경 
            }
        }
        if (livingMemberList.Count < 1) // 살아있는 유닛이 없는경우 처리 
        {
            //해당군체는 전멸했으니 처리할 로직 작성 
        }
    }


    /// <summary>
    /// 액션 연결하는 함수
    /// </summary>
    /// <param name="unit">연결할 유닛</param>
    private void AddAction(IControllObject unit)
    {
        unit.onDie  += UnitDieAndNextLeaderSetting;
        onAssemble  += unit.OnAssemble;
        onMove      += unit.CharcterMove;
        onStop      += unit.OnFlockingMovingStop;
    }

    /// <summary>
    /// 액션 연결끊는 함수
    /// </summary>
    /// <param name="unit">연결끊을 유닛</param>
    private void DeleteAction(IControllObject unit)
    {
        unit.onDie  -= UnitDieAndNextLeaderSetting;
        onAssemble  -= unit.OnAssemble;
        onMove      -= unit.CharcterMove;
        onStop      -= unit.OnFlockingMovingStop;
    }
}

