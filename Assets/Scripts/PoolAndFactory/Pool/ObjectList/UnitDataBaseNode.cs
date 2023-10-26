using System;
using UnityEngine;

/// <summary>
/// 캐릭터 기본 능력치 정의 클래스
/// </summary>
public class UnitDataBaseNode : PoolObjectBase, IUnitStateTable
{
    /// <summary>
    /// 스크립터블로셋팅된 기본 데이터 받아올 객체 
    /// </summary>
    [SerializeField]
    Scriptable_UnitType_DataBase scriptableObject;

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
    /// 장비하고있는 장비클래스 
    /// </summary>
    UnitEquipBase UnitEquipBase { get; set; }

    /// <summary>
    /// 죽을때 동시에 실행할 내용이 필요할시 연결할 델리게이트
    /// </summary>
    public Action onDie { get; set; }


    protected virtual void Awake()
    {
        unitRealTimeState = new UnitRealTimeState();    //틀 잡아두고 
        unitData = new UnitData();
    }

    /// <summary>
    /// 초기값 셋팅용 
    /// </summary>
    /// <param name="index">유닛 관리할 인덱스값</param>
    /// <param name="unitStateData">초기데이터</param>
    public virtual void InitDataSetting(int index, UnitStateData unitStateData)
    {
        unitData.OnDataChange(index,scriptableObject.UnitDataBase, unitStateData);
        SetUnitDataSetting();
    }

    /// <summary>
    /// 저장파일에서 읽어와서 파싱용도로 사용할 함수
    /// </summary>
    /// <param name="index">유닛 관리할 인덱스값</param>
    /// <param name="unitDataBase">직업별 기본능력치값</param>
    /// <param name="unitStateData">초기데이터</param>
    public virtual void LoadDataParsing(int index, UnitDataBase unitDataBase, UnitStateData unitStateData) 
    {
        unitData.OnDataChange(index, unitDataBase, unitStateData);
        SetUnitDataSetting();
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
    /// 장비 클래스에서 데이터를 가져오는 함수
    /// </summary>
    /// <param name="equipData">장비 클래스</param>
    /// <returns>장비 에서 추가될 능력치</returns>
    protected virtual int UnitEquipData(UnitEquipBase equipData, EquipStateType stateType) 
    {
        return int.MinValue; //오버라이딩 해서 사용해야한다. 
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

    public void InitDataSetting(UnitStateData unitData)
    {
        throw new NotImplementedException();
    }
}
