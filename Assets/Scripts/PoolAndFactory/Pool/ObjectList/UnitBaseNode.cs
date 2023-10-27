using System;
using UnityEngine;

/// <summary>
/// 유닛 데이터 잡아둘 노드데이터
/// </summary>
public class UnitBaseNode : PoolUnitBase, IUnitDefaultBase
{
    [Header("데이터 셋팅 ")]
    /// <summary>
    /// 팀에서의 순번
    /// 유닛 배치와 연관있는 변수
    /// </summary>
    [SerializeField]
    int memberIndex = -1;
    public int MemberIndex
    {
        get => memberIndex;
        set => memberIndex = value;
    }

    /// <summary>
    /// 유닛 정보 담아둘 객체 
    /// </summary>
    UnitData unitData;
    public UnitData UnitData => unitData;
    
    /// <summary>
    /// 배틀 맵에서 사용할 데이터를 저장할 구조체 
    /// </summary>
    protected UnitRealTimeState unitRealTimeState;
    public UnitRealTimeState UnitRealTimeState => unitRealTimeState;

    /// <summary>
    /// 죽을때 동시에 실행할 내용이 필요할시 연결할 델리게이트
    /// </summary>
    public Action onDie { get; set; }

    /// <summary>
    /// 유닛의 프리팹
    /// </summary>
    [SerializeField]
    GameObject unitPrefab;
    public GameObject UnitPrefab => unitPrefab;

    protected virtual void Awake()
    {
        unitRealTimeState = new UnitRealTimeState();    //틀 잡아두고 
    }

    /// <summary>
    /// 유닛 데이터로 초기값 셋팅용 함수 
    /// 로드시에도 사용 
    /// </summary>
    /// <param name="unitData">유닛 데이터</param>
    /// <param name="unitPrefab">유닛 모델</param>
    public virtual void InitDataSetting(UnitData unitData, GameObject unitPrefab)
    {
        this.unitPrefab = unitPrefab;
        InitDataSetting(unitData);
        SetUnitDataSetting();
    }
    public void InitDataSetting(UnitData unitData)
    {
        this.unitData = unitData;
    }

    /// <summary>
    /// 기본값 가지고 실시간 전투 데이터 셋팅용 함수 
    /// 전체적으로 셋팅할 용도 
    /// </summary>
    protected virtual void SetUnitDataSetting() 
    {
    }

    /// <summary>
    /// 장비가 바뀔때 부분적으로 셋팅할 용도 
    /// </summary>
    protected virtual void SetEquipChange() 
    {

    }



    /// <summary>
    /// 캐릭터 죽을때 처리할 함수
    /// </summary>
    protected virtual void OnDie() 
    {
        onDie?.Invoke();    
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

 
}
