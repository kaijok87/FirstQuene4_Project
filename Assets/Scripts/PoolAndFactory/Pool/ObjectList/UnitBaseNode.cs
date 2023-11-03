using System;
using UnityEngine;

/// <summary>
/// 유닛 데이터 잡아둘 노드데이터
/// </summary>
public class UnitBaseNode : MonoBehaviour, IUnitDefaultBase
{
    [Header("데이터 셋팅 ")]
    /// <summary>
    /// 유닛 정보 
    /// </summary>
    UnitData unitData;
    public UnitData UnitData => unitData;

    /// <summary>
    /// 배틀 맵에서 실시간으로 변동할 데이터
    /// </summary>
    protected UnitRealTimeState unitRealTimeState;
    public UnitRealTimeState UnitRealTimeState => unitRealTimeState;

    /// <summary>
    /// 유닛 컨트롤 할 컴퍼넌트 저장해두기 
    /// </summary>
    UnitMoveController unitMoveController;
    public UnitMoveController UnitMoveController => unitMoveController;

    /// <summary>
    /// 자신이 속한 팀 
    /// </summary>
    ITeamUnit teamUnit;
    public ITeamUnit CurrentTeam
    {
        get => teamUnit;
        set => teamUnit = value;
    }

    /// <summary>
    /// 현재위치를 군집의 기본위치로 잡기위해 기준점(리더)와의 거리좌표를 반환하는 프로퍼티
    /// </summary>
    public Vector3 GetFlockingPos => teamUnit.LeaderUnit.transform.position - transform.position;

    /// <summary>
    /// 유닛이 죽을때 실행할 델리게이트
    /// </summary>
    public Action<IUnitDefaultBase> onDie { get; set; }

    /// <summary>
    /// 자신이 속한 큐로 돌리기신호를 보낼 델리게이트
    /// </summary>
    public Action onResetData;


    protected virtual void Awake()
    {
        unitRealTimeState = new UnitRealTimeState();    //틀 잡아두고 
        unitMoveController = GetComponent<UnitMoveController>();
    }

    /// <summary>
    /// 유닛 데이터로 초기값 셋팅용 함수 
    /// 로드시에도 사용 
    /// </summary>
    /// <param name="unitData">유닛 데이터</param>
    public virtual void InitDataSetting(UnitData unitData)
    {
        this.unitData = unitData;
        SetUnitDataSetting();
    }
  
    /// <summary>
    /// 기본값 가지고 실시간 전투 데이터 셋팅용 함수 
    /// 전체적으로 셋팅할 용도 
    /// </summary>
    protected virtual void SetUnitDataSetting()
    {
        unitMoveController = transform.GetComponentInChildren<UnitMoveController>();
        //데이터 셋팅 
        AppendActionDelegate();
    }

    /// <summary>
    /// 필요한 델리게이트 연결하는함수 
    /// </summary>
    protected virtual void AppendActionDelegate()
    {
        unitRealTimeState.onDie += OnDie;
    }

    /// <summary>
    /// 연결된 델리게이트 제거 하는함수 
    /// </summary>
    protected virtual void RemoveActionDelegate()
    {
        unitRealTimeState.onDie -= OnDie;
    }

    /// <summary>
    /// 장비가 바뀔때 부분적으로 셋팅할 용도 
    /// </summary>
    protected virtual void SetEquipChange() 
    {

    }

    /// <summary>
    /// 유닛이 죽거나 로드및 다른캐릭터 정보가 입력될시 
    /// 데이터 입력하기 전의 상태로 돌리는 함수 
    /// </summary>
    public virtual void ResetData() 
    {
        unitData = null;
        unitRealTimeState.ResetData();
        onDie = null;
        RemoveActionDelegate();
        gameObject.SetActive(false);
        onResetData?.Invoke();
    }

    /// <summary>
    /// 캐릭터 죽을때 처리할 함수
    /// </summary>
    protected virtual void OnDie() 
    {
        ResetData();
        onDie?.Invoke(this);    
    }

    /// <summary>
    /// 캐릭터 이름 변경 함수
    /// 게임내부에서 캐릭터 이름변경시 사용할 함수
    /// </summary>
    /// <param name="newName">변경될 이름</param>
    protected virtual void UnitNameChange(string newName) 
    {
        //unitData.UnitStateData.UnitName = newName;
    }

    /// <summary>
    ///  맴버로 셋팅된 이동 범위값 반환
    /// </summary>
    /// <returns>이동범위 값</returns>
    public float GetUnitMoveRange()
    {
        return unitRealTimeState.MoveRange;
    }

    /// <summary>
    /// 맴버로 셋팅된 번호값 가져오는 함수
    /// </summary>
    /// <returns>맴버 인덱스값</returns>
    public int GetMemberIndex()
    {
        return unitRealTimeState.MemberIndex;
    }

    /// <summary>
    /// 맴버유닛의 이동속도 반환 
    /// </summary>
    /// <returns></returns>
    public float GetUnitMoveSpeed()
    {
        return unitRealTimeState.MoveSpeed;
    }
}
