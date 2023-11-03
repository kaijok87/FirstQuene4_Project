using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 팀의 종류 
/// </summary>
public enum TeamType
{
    None,
    Neutral,
    Player,
    Enemy
}

public enum MapType 
{
    None = 0,
    Town ,
    Battle,

}

[Serializable]
public struct EventData 
{
}

/// <summary>
/// 맵의 변하지않는 기본 정보 
/// </summary>
[Serializable]
public struct BattleMapData 
{
    /// <summary>
    /// 맵의 종류 
    /// </summary>
    [SerializeField]
    MapType type;
    public MapType Type => type;
    /// <summary>
    /// 현재 맵의 이벤트 정보 
    /// </summary>
    [SerializeField]
    EventData[] eventDatas; //이렇게 선언하면 참조타입
    public EventData[] EventDatas => eventDatas; 

    /// <summary>
    /// 현재맵에서의 생성할 팀의 종류
    /// </summary>
    [SerializeField]
    BattleMapGeneratePoolData[] teams;
    public BattleMapGeneratePoolData[] Teams => teams;

    public void TestInit(int length, Vector3[] testPos) 
    {
        type = MapType.Battle;
        eventDatas = new EventData[length]; 
        teams = new BattleMapGeneratePoolData[length];
        for (int i = 0; i < length; i++)
        {
            teams[i] = new BattleMapGeneratePoolData();
            teams[i].TestInit(length,testPos);
        }
    }
}


/// <summary>
/// 팀하나하나마다 유닛들의 리더기준으로 얼마나 떨어질지에대한 초기 값을 저장해둘 구조체
/// </summary>
[Serializable]
public struct TeamFlockingPositions
{
    /// <summary>
    /// 현재팀의 맴버들의 분포도
    /// </summary>
    [SerializeField]
    Vector3[] memberInitVector;
    public Vector3[] MemberIntVector => memberInitVector;

    public void TestInit(Vector3[] testPos) 
    {
        memberInitVector = testPos;
    }
}

/// <summary>
/// 배틀 맵에서 NonPlayer을 풀에서 가져와서 셋팅하기위한 데이터를 담을 클래스
/// </summary>
[Serializable]
public class BattleMapGeneratePoolData
{
    /// <summary>
    /// 생성할 프리팹 유닛의 팀의 종류 
    /// </summary>
    [SerializeField]
    TeamType teamType;
    public TeamType TeamType => teamType;

    /// <summary>
    /// 생성할 프리팹 유닛의 등급 종류
    /// </summary>
    [SerializeField]
    UnitGrade[] unitGrade;
    public UnitGrade[] UnitGrade => unitGrade;

    /// <summary>
    /// 생성할 프리팹 유닛의 팀의 순번
    /// </summary>
    [SerializeField]
    int[] teamIndex;
    public int[] TeamIndex => teamIndex;

    /// <summary>
    /// 생성할 프리팹 유닛의 팀에서의 순번
    /// </summary>
    [SerializeField]
    int[] memberIndex;
    public int[] MemberIndex => memberIndex;

    /// <summary>
    /// 생성할 프리팹 유닛의 게임 유닛데이터 에서의 순번
    /// </summary>
    [SerializeField]
    int[] unitIndex;
    public int[] UnitIndex => unitIndex;

    /// <summary>
    /// 생성할 프리팹 유닛의 종류 
    /// </summary>
    [SerializeField]
    Scriptable_UnitType[] generateUnitType;
    public Scriptable_UnitType[] GenerateUnitType => generateUnitType;

    /// <summary>
    /// 현재 팀의 리더기준으로 맴버의 분포도저장할 배열 
    /// </summary>
    [SerializeField]
    TeamFlockingPositions[] teamFlockingPos;
    public TeamFlockingPositions[] TeamFlockingPos => teamFlockingPos;  

    /// <summary>
    /// 생성할 프리팹 유닛의 초기 위치값
    /// </summary>
    [SerializeField]
    Vector3 initPos;
    public  Vector3 InitPos => initPos; //값복사 발생
    public void TestInit(int length, Vector3[] testPos) 
    {
        unitGrade           = new UnitGrade [length];
        teamIndex           = new int[length];
        memberIndex         = new int[length];
        unitIndex           = new int[length];
        generateUnitType    = new Scriptable_UnitType[length];
        teamFlockingPos = new TeamFlockingPositions[length];
        int length_ = Enum.GetValues(typeof(TeamType)).Length;
        teamType = (TeamType)UnityEngine.Random.Range(0,length_);
        length_ = Enum.GetValues (typeof(UnitGrade)).Length;
        for (int i = 0; i < length; i++) 
        {
            unitGrade[i] = (UnitGrade)UnityEngine.Random.Range(0, length_);
            TeamFlockingPos[i].TestInit(testPos);
        }
    }
}


/// <summary>
/// 맵관련 데이터 저장용 
/// </summary>
[Serializable]
public class BattleMapDataTable 
{
    /// <summary>
    /// 맵의 레벨정보 
    /// </summary>
    int[] levels;
    public int[] Levels => levels;

    /// <summary>
    /// 현재 맵의 셋팅 정보 
    /// </summary>
    [SerializeField]
    BattleMapData[] mapDatas;   //Array 이니깐 참조타입
    public BattleMapData[] MapDatas => mapDatas;

    /// <summary>
    /// 초기 데이터 셋팅용 함수
    /// </summary>
    public void InitDataSetting()
    {

    }

    /// <summary>
    /// 레벨 정보 수정용 함수 
    /// </summary>
    /// <param name="index">맵의 인덱스정보</param>
    /// <param name="level">저장할 레벨정보 </param>
    public void SetLevel(int index, int level)
    {
    }
    /// <summary>
    /// 초기 데이터 셋팅용 함수
    /// </summary>
    public void TestInitDataSetting(int length , Vector3[] testPos)
    {
        levels = new int[length];
        mapDatas = new BattleMapData[length];
        for (int i = 0; i < length; i++)
        {
            mapDatas[i].TestInit(length,testPos);
        }
        //foreach (var item in mapDatas) //구조체 배열을 사용시 item에 구조체가 복사가되어 읽기 전용으로 사용된다고 보면된다 
        //{
        //    item.TestInit(length);
        //}
    }
}

/*
 리셋시 고려할목록
  1. 아군유닛이 아닐 경우 배틀맵 아닌곳으로 전환될때는 무조건 풀로 각각 돌려야한다. 
  2. 아군 팀 및 유닛은 초기화 상태(타이틀로 돌아가거나 게임 로드)일때는 각각 풀로돌려야한다 
    2-1. 아군이 배틀맵이 아닌곳으로 이동할때는 초기 위치(Position)로 돌려놔야한다.
    2-2. 재시작일 경우의 초기 값셋팅으로 셋팅이 되어야한다.
 */
/// <summary>
/// 배틀맵에서 사용할 팀별 유닛을 관리하고 처리하는 매니저 로직 
/// </summary>
public class BattleUnitGenerateManager : NomalSingleton<BattleUnitGenerateManager>
{
    //== 전체 팀 종류 

   
    
    //=== 맵하나당 사용될 팀 리스트 

    /// <summary>
    /// 중립 팀 ( 기믹 , 함정 및 아이템 )
    /// </summary>
    [SerializeField]
    List<ITeamUnit> neutralTeam;
    public List<ITeamUnit> NeutralTeam => neutralTeam;

    /// <summary>
    /// 적군 팀
    /// </summary>
    [SerializeField]
    List<ITeamUnit> enemyTeam;
    public List<ITeamUnit> EnemyTeam => enemyTeam;

    /// <summary>
    /// 아군 팀
    /// </summary>
    [SerializeField]
    List<ITeamUnit> playerTeam;
    public List<ITeamUnit> PlayerTeam => playerTeam;

  
    protected override void Awake()
    {
        base.Awake();
        neutralTeam = new(1);
        enemyTeam = new(4);
        playerTeam = new(2);
    }

    /// <summary>
    /// 배틀맵 시작시 공통부분 데이터 셋팅 
    /// <param name="mapIndex">초기화할 맵의 인덱스값</param>
    /// </summary>
    public void BattleDataSetting(int mapIndex )
    {
        BattleMapData mapData = MapDataGenerateManager.Instance.BattleMapDataTable.MapDatas[mapIndex]; //맵의 초기화 데이터 가져오고 
        BattleMapGeneratePoolData[] teamArray = mapData.Teams;  // 셋팅할 오브젝트 배열 가져와서 
        if (teamArray != null)      //셋팅할 게 있으면 셋팅
        {
            int teamSize = teamArray.Length;    
            if (teamSize > 0) //생성할 팀이 존재할때
            {
                TeamType type = TeamType.None;                                          // 팀 의 타입 체크용으로 미리선언
                TeamBaseNode teamObject = null;                                         // 팀 유닛 미리선언 
                UnitBaseNode[] unitBaseNodes;
                BattleMapGeneratePoolData teams;                                         //
                UnitFactory unitFactory = GameManager.Instance.UnitPools;               //생성할 팩토리 미리가져오고 
                int unitSize = 0;
                int typeIndex = 0;
                for (int i = 0; i < teamSize; i++)
                {
                    teams = teamArray[i];                                                    // 인덱스에 해당하는 객체 미리 셋팅 
                    if (teams != null)                                                   // 데이터 존재하면 추가
                    {
                        unitSize = teams.UnitIndex.Length;                                           // 유닛 배열크기 가져오고 
                        teamObject = (TeamBaseNode)BattleMapObjectFactory.Instance.GetTeamUnit();// 팀 유닛 팩토리에서 가져오고 
                        teamObject.transform.SetParent(null);
                        teamObject.gameObject.SetActive(true);
                        unitBaseNodes = new UnitBaseNode[unitSize];                                 // 추가할 유닛배열 사이즈잡고
                        type = teams.TeamType;                                                       // 팀 타입 정하고 
                        for (int j = 0; j < unitSize; j++)
                        {
                            typeIndex = (int)teams.GenerateUnitType[j];                      //자동생성된 풀인덱스로 변환
                            switch (teams.UnitGrade[j])
                            {
                                case UnitGrade.None:
                                                    //기믹 및 함정 오브젝트도 추가 
                                    break;

                                case UnitGrade.Grunt:   //일반병사 셋팅
                                    unitBaseNodes[j] = unitFactory.NomalUnitPoolArray[typeIndex].GetPoolObject();
                                    unitBaseNodes[j].transform.SetParent(teamObject.transform);
                                    UnitDataSetting(unitBaseNodes[j], mapData);
                                    break;
                                case UnitGrade.Hero:    //영웅 셋팅
                                    unitBaseNodes[j] = unitFactory.HeroUnitPoolArray[typeIndex].GetPoolObject();
                                    unitBaseNodes[j].transform.SetParent(teamObject.transform);
                                    UnitDataSetting(unitBaseNodes[j], mapData);
                                    break;
                                default:
                                    Debug.Log("유닛 등급이 없습니다.");
                                    break;
                            }
                        }
                        teamObject.SetFlockingPosArray(teams.TeamFlockingPos[i].MemberIntVector);
                        teamObject.InitDataSetting(type,unitBaseNodes ,teams.InitPos); //팀 기본 값 셋팅 
                    }
                    if (teamObject != null) //추가할 배열이 존재하면 
                    {
                        switch (type) //검색해서 추가 
                        {
                            case TeamType.None:

                                break;
                            case TeamType.Neutral:
                                neutralTeam.Add(teamObject);
                                break;
                            case TeamType.Player:
                                playerTeam.Add(teamObject);
                                break;
                            case TeamType.Enemy:
                                enemyTeam.Add(teamObject);
                                break;
                            default:
                                break;
                        }
                    }
                    BattleInputManager.Instance.OnChangedTeam(0);                           //이동 로직 연결
                }
                
            }
            else 
            {
                Debug.Log("생성할 목록이없다");
            }
        }
    }

    /// <summary>
    /// 초기화 이동로직 연결도 필요한거같다 
    /// </summary>
    /// <param name="unit"></param>
    private void UnitDataSetting(UnitBaseNode unit ,BattleMapData mapData) 
    {
        unit.gameObject.SetActive(true);
    }
  

    /// <summary>
    /// 타입에 맞는 팀을 추가한다.
    /// </summary>
    /// <param name="teamType">추가할 팀 종류</param>
    /// <param name="teamUnits">추가할 팀 데이터 목록</param>
    private void AppendTeam(TeamType type, IUnitDefaultBase[] teamUnits, Vector3 initPos)
    {
        ITeamUnit teamType = BattleMapObjectFactory.Instance.GetTeamUnit(); //게임 오브젝트 추가 

        teamType.InitDataSetting(type,teamUnits, initPos); 
        
        switch (type)
        {
            case TeamType.None:
                break;
            case TeamType.Neutral:
                neutralTeam.Add(teamType);  //데이터 추가 
                break;
            case TeamType.Player:
                playerTeam.Add(teamType);
                break;
            case TeamType.Enemy:
                enemyTeam.Add(teamType);
                break;
            default:
                break;
        }
            
    }

    /// <summary>
    /// 타입에 맞는 팀을 지운다.
    /// 항상 들고다닐것이고 팀이 줄어드는 것을 관리하기위해 작성
    /// </summary>
    /// <param name="type">제거할 팀 정보</param>
    private void RemoveTeam(ITeamUnit teamUnit)
    {
        switch (teamUnit.Type)
        {
            case TeamType.None:
                break;
            case TeamType.Neutral:
                neutralTeam.Remove(teamUnit);
                break;
            case TeamType.Player:
                playerTeam.Remove(teamUnit);
                break;
            case TeamType.Enemy:
                enemyTeam.Remove(teamUnit);
                break;
            default:
                break;
        }
        teamUnit.ResetData();
    }

    /// <summary>
    /// 씬 전환 및 배틀 상황에서 초기화 할 함수
    /// </summary>
    public void ResetData_SceneMove() 
    {
        neutralTeam.Clear();
        enemyTeam.Clear();
    }

    /// <summary>
    /// 타이틀 및 로드 시 데이터 초기화용 함수 
    /// 매니저안에 데이터를 초기상태(게임실행시 초기값)로 돌리기위한 함수 
    /// </summary>
    public void ResetData() 
    {
        ListDataReset(neutralTeam);
        ListDataReset(enemyTeam);
        ListDataReset(playerTeam);
        
    }
    private void ListDataReset(List<ITeamUnit> resetList) 
    {
        foreach (var item in resetList)
        {
            item.ResetData();
        }
        resetList.Clear();
    }
}
