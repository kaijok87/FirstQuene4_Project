using System;
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
    public void ResetData() 
    {
        moveSpeed = 0f;
        fatigueValue = 0f;
    }
}

/// <summary>
/// 팀의 데이터를 관리할 컴포넌트 
/// </summary>
public class TeamBaseNode : PoolObjectBase , ITeamUnit
{
    /// <summary>
    /// 현재팀의 종류 
    /// </summary>
    TeamType teamType;
    public TeamType Type => teamType;

    /// <summary>
    /// 팀의 최대 인원수 설정할 값
    /// </summary>
    [SerializeField]
    int unitMaxCapacity = 20;
    public int UnitMaxCapacity => unitMaxCapacity;

    /// <summary>
    /// 해당군체가 가지고있는 맴버 배열
    /// 여기에 들어가는 배열 순서위치의 값들은 셋팅하는 법이 3종류 있다
    /// 1. 전투맵에서 해당 맴버의 유닛이 죽었을경우 제거 v
    /// 2. 전투맵에서 적군의 유닛이 항복해서 아군유닛으로 편입할때 배열이 비어있으면 추가  v
    /// 3. 편성 화면에서 편성한내용을 적용 (해당내용은 수정이완료됬을때 한번만한다) 
    /// </summary>
    IUnitDefaultBase[] units;
    public IUnitDefaultBase[] Units => units;

    /// <summary>
    /// 리더 유닛 기본적으로 0번의 유닛이다.
    /// 군체의 중심에 있는 기준점이될 유닛의 위치 
    /// </summary>
    IUnitDefaultBase leaderUnit;
    public IUnitDefaultBase LeaderUnit => leaderUnit;


    /// <summary>
    /// 히든 속성 정보
    /// </summary>
    HiddenLeaderAbility mainUnitHiddenAbility;
    public HiddenLeaderAbility MainUnitHiddenAbility => mainUnitHiddenAbility;


    /// <summary>
    /// 현재 실시간 유닛 컨트롤 용으로 사용할 리스트 
    /// </summary>
    List<IUnitDefaultBase> livingMemberList;
    public List<IUnitDefaultBase> LivingMemberList => livingMemberList;



    /// <summary>
    /// 군체의 분포위치를 저장할 백터값
    /// </summary>
    Vector3[] flockingPosArray;
    public Vector3[] FlockingPosArray => flockingPosArray;

    /// <summary>
    /// 이동로직 저장해둘 변수
    /// </summary>
    TeamMoveController moveController;
    public TeamMoveController MoveController => moveController;
    

    protected virtual void Awake()
    {
        units = new IUnitDefaultBase[unitMaxCapacity];
        livingMemberList = new List<IUnitDefaultBase>(unitMaxCapacity);
        moveController = GetComponent<TeamMoveController>();
    }

    /// <summary>
    /// 외부에서 데이터 초기화 하기위한 함수
    /// </summary>
    /// <param name="type">팀의 타입 (아군 ,적군, 중립, 기믹)</param>
    /// <param name="units">팀의 데이터 </param>
    /// <param name="initPos">in을 사용해 복사를 방지 팀의 처음 위치값</param>
    public void InitDataSetting(TeamType type, IUnitDefaultBase[] units, in Vector3 initPos)
    {
        this.teamType = type;
        InitData(units,initPos);
    }

    /// <summary>
    /// 군체의 진형 포지션값 셋팅하기 
    /// </summary>
    /// <param name="floackingPos"> 군체 진형 포지션값 의 배열</param>
    public void SetFlockingPosArray(Vector3[] floackingPos)
    {
        flockingPosArray = floackingPos;
    }

    /// <summary>
    /// 군체 맴버 셋팅하는 함수 
    /// 매번 새롭게 리스트를 셋팅 하기때문에 자주실행하면 안좋다.
    /// <param name="appendUnits"> 맴버에 추가될 유닛들</param>
    /// <param name="initPos">in을 사용해 복사를 방지 팀의 처음 위치값</param>
    /// </summary>
    public void InitData(IUnitDefaultBase[] appendUnits ,in Vector3 initPos)
    {
        if (appendUnits != null) 
        {
            int forSize = appendUnits.Length;
            if (units.Length >= forSize) //들어온 유닛 수가 최대 유닛 수보다 같거나 작을때만 셋팅  
            {
                IUnitDefaultBase appendUnit;
                for (int i = 0; i < forSize; i++)
                {
#if UNITY_EDITOR
                    if (GameManager.Instance.IsDebugCheck) 
                    {
                        Debug.Log($"TeamObject 초기화 {i}번째 : {appendUnits[i]}");
                    }
#endif
                    appendUnit = appendUnits[i];
                    if (appendUnit != null) //배열 중간중간 비어있는 값이 존재함으로 값이 있는경우만 체크 
                    {
                        if (leaderUnit == null) //첫번째 유닛에 리더를 설정 
                        {
                            leaderUnit = appendUnit;
                            SettingHiddenLeaderAbility();
                            leaderUnit.transform.position = initPos;
                        }
                        else //리더가 기준점이기때문에 리더빼고는 전부 포지션 셋팅 
                        {
                            appendUnit.UnitData.SetFlockingPos(flockingPosArray[i]);
                            appendUnit.transform.position = initPos+ flockingPosArray[i];
                        }
                        AddUnit(i, appendUnit);
                    }
                }
                return;
            }
            Debug.LogWarning($"초기화할 데이터가 많습니다 추가할 크기 :{appendUnits.Length}  최대 크기 : {units.Length}");

        }
        else
        {
            Debug.LogWarning($"초기화할 데이터가 존재하지 않습니다 data : {appendUnits}");
        }
    }

    /// <summary>
    /// 리더의 숨겨진 속성을 찾아서 히든 속성에 적용하는함수 
    /// </summary>
    private void SettingHiddenLeaderAbility() 
    {
        //리더의 정보에서 숨겨진 속성값 찾아서 적용하는 내용 작성 
    }

    /// <summary>
    /// 팀에 유닛정보 추가 처리하는 함수 
    /// </summary>
    /// <param name="index">입력할 배열위치</param>
    /// <param name="unit">입력할 데이터 </param>
    private void AddUnit(int index, IUnitDefaultBase unit) 
    {
        //unit.UnitRealTimeState.MemberIndex = index;
        unit.CurrentTeam = this;
        units[index] = unit;
        livingMemberList.Add(unit);
        OnAppendAction(unit);
    }


    /// <summary>
    /// 맴버군체에 유닛을 추가하는 함수
    /// </summary>
    /// <param name="appendUnit">추가할 유닛</param>
    /// <returns>성공여부 성공true  실패 false</returns>
    public bool AppendUnit(IUnitDefaultBase appendUnit) 
    {
        int forSize = units.Length;
        IUnitDefaultBase unit;
        for (int i = 0; i < forSize; i++)
        {
            unit = units[i];
            if (unit == null) //유닛이 존재하지않을때만 추가 
            {
                //unit.UnitRealTimeState.ColonyMemberPosition = flockingPosArray[i];
                AddUnit(i,unit);
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// 항복한 유닛이 현재 팀의 유닛으로 이동 시킬수있는지 체크해서 이동시키는 함수 
    /// </summary>
    /// <param name="surrenderUnit">항복한 맴버</param>
    /// <param name="surrenderTeam">항복한 유닛의 팀</param>
    /// <returns>항복 성공시 true 실패시 false </returns>
    public bool SurrenderUnit(ITeamUnit surrenderTeam, IUnitDefaultBase surrenderUnit) 
    {
        if (AppendUnit(surrenderUnit)) //유닛 추가 시도
        { 
            surrenderTeam.RemoveUnit(surrenderUnit);    //항복한 팀에서의 내용 지우기 
            return true;
        }
        return false;
    }


    /// <summary>
    /// 현재 위치를 맴버 군체위치로 다시셋팅하는 함수
    /// </summary>
    public void SettingFlockingPos() 
    {
        foreach (IUnitDefaultBase liveMember in livingMemberList)
        {
            flockingPosArray[liveMember.GetMemberIndex()] = liveMember.GetFlockingPos;
        }

    }

    /// <summary>
    /// 셋팅된 데이터 초기화 
    /// </summary>
    public override void ResetData()
    {
        leaderUnit = null;

        int forSize = units.Length;
        IUnitDefaultBase unit;
        for (int i = 0; i < forSize; i++) //참조만 끊어버리고 데이터는 다른곳에서 처리 
        {
            unit = units[i];
            if (unit != null) 
            {
                unit.ResetData();
                OnRemoveAction(unit);
            }
            units[i] = null;
        }

        livingMemberList.Clear();
        
        mainUnitHiddenAbility.ResetData();
        
        base.ResetData();
    }

    /// <summary>
    /// 유닛이 죽을때 처리 하기위한 함수 
    /// 배열및 리스트에서 제거 및 리더 가죽었으면 리더변경
    /// 모든 유닛이 죽었으면 처리할로직 
    /// </summary>
    /// <param name="removeUnit">제거할 유닛</param>
    public void RemoveUnit(IUnitDefaultBase removeUnit)
    {
        livingMemberList.Remove(removeUnit);
        int index = removeUnit.GetMemberIndex();
        
        OnRemoveAction(removeUnit);
        units[index] = null;

        if (livingMemberList.Count > 0) //살아있는 유닛이 있을때  
        {
            if (removeUnit == leaderUnit)  //죽은 유닛이 리더면 
            {
                leaderUnit = livingMemberList[0]; //맨처음 유닛을 리더로 변경 
                SettingHiddenLeaderAbility();
            }
        }
        else // 살아있는 유닛이 없는경우 처리 
        {
            //해당군체는 전멸했으니 처리할 로직 작성 
        }
    }
    
    /// <summary>
    /// 델리게이터 연결이 필요한 작업이 있는경우 여기에 전부 작성한다 .
    /// </summary>
    /// <param name="index">추가할 팀 맴버의 인덱스</param>
    private void OnAppendAction(IUnitDefaultBase unit)
    {
        unit.onDie += RemoveUnit;
    }

    /// <summary>
    /// 델리게이터 연결을 해제할 작업이 있는경우 여기에 전부 작성한다 .
    /// </summary>
    /// <param name="index">제거할 팀 맴버의 인덱스</param>
    private void OnRemoveAction(IUnitDefaultBase unit)
    {
        unit.onDie -= RemoveUnit;
    }


}

