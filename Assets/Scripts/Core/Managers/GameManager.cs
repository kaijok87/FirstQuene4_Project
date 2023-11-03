using System;
using UnityEngine;

/*
  정리 
  1. 게임 오브젝트 만들고 관리할 매니저 
    1-1. 풀로 생성할 목록 
        - NPC 유닛 프리팹 생성용 (몬스터, 중립NPC, 병사NPC 등)
        - 아이템 및 기타 유닛 프리팹 생성용 (아이템 , 함정 , 기믹 등)
        -
    1-2. 풀로 생성시 초기화할 목록
        - 풀(큐)에서 꺼내서 초기데이터셋팅 및 계층이동(부모 Transform 이동) 
    1-3. 풀로 생성시 리셋할 목록
        - 풀(큐)로 돌리고 포지션값 Zero 로 돌리기 
        - 연결된 델리게이트 끊기
        - 참조 되고있는 데이터 끊기

  2. 게임 내부 데이터 관리할 매니저 
  3. 게임 오브젝트 동작시킬 매니저
 
 */


/// <summary>
/// 게임 속도 조절용 이넘값
/// 백분율 단위의 int 값을 저장한다.
/// </summary>
public enum GameSpeedType 
{
    Stop            = 0,            // 정지 0배속
    OneFourth       = 25,           // 1/4배속
    HalfSlow        = 50,           // 1/2배속
    Nomal           = 100,          // 1  배속
    DoubleFast      = 200,          // 2  배속
    QuadrupleFast   = 400,          // 4  배속
    OctupleFast     = 800,          // 8  배속
}




public class GameManager : DontDestroySingleton<GameManager>
{
    /// <summary>
    /// 게임속도 조절용 이넘 값
    /// 게임속도 연산변수 조절용 
    /// </summary>
    [SerializeField]
    GameSpeedType gameSpeedType = GameSpeedType.Nomal;
    public GameSpeedType GameSpeedType
    {
        get => gameSpeedType;
        set
        {
            if (value != gameSpeedType)
            {
                gameSpeedType = value;
                switch (gameSpeedType)
                {
                    case GameSpeedType.Stop:
                        Time.timeScale = 0.0f;
                        break;
                    case GameSpeedType.OneFourth:
                    case GameSpeedType.HalfSlow:
                    case GameSpeedType.Nomal:
                    case GameSpeedType.DoubleFast:
                    case GameSpeedType.QuadrupleFast:
                    case GameSpeedType.OctupleFast:
                    default:
                        Time.timeScale = 1.0f * ((float)gameSpeedType / 100.0f);
                        break;
                }
#if UNITY_EDITOR
                tempGameSpeedType = gameSpeedType;
#endif

            }
        }
    }

    /// <summary>
    /// BGM 소리 크기 값
    /// </summary>
    [SerializeField]
    float backGroundMusicVolumSize;

    /// <summary>
    /// 이팩트 소리 크기 값
    /// </summary>
    [SerializeField]
    float effectSoundVolumSize;


    /// <summary>
    /// 플레이어가 가질수있는 유닛 갯수 
    /// </summary>
    [SerializeField]
    int maxUnitSize = 100;

    /// <summary>
    /// 게임플레이도중에 아군 유닛 을 담아둘 배열
    /// </summary>
    [SerializeField]
    UnitBaseNode[] unitDatas;
    public UnitBaseNode[] UnitDatas => unitDatas;

    /// <summary>
    /// 관리될 유닛오브젝트들을 담을 위치
    /// </summary>
    Transform unitListParent;

    /// <summary>
    /// 게임도중 죽은유닛 담아둘 클래스
    /// </summary>
    DeadUnitTable deadUnitTable;
    public DeadUnitTable DeadUnitTable => deadUnitTable;


    /// <summary>
    /// 유닛 생성용 풀 
    /// </summary>
    [SerializeField]
    UnitFactory unitPools;
    public UnitFactory UnitPools => unitPools;





    protected override void Awake()
    {
        base.Awake();
        unitPools = FindObjectOfType<UnitFactory>(true);
        deadUnitTable = new DeadUnitTable();
        unitDatas = new UnitBaseNode[maxUnitSize];
        unitListParent = transform.GetChild(0);

        int enumLength = Enum.GetValues(typeof(Scriptable_UnitType)).Length;
        for (int i = 0; i < enumLength; i++)
        {
            GenerateUnit((Scriptable_UnitType)i, ((Scriptable_UnitType)i).ToString());
        }
    }
















   

    /// <summary>
    /// 일반 유닛을 생성해서 배열에 집어넣는 로직 
    /// </summary>
    /// <param name="unitType">유닛 종류</param>
    /// <param name="unitName">유닛 이름</param>
    public void GenerateUnit(Scriptable_UnitType unitType, string unitName) 
    {
        int unitIndex = IsAppendUnitIndex();
        if (unitIndex > -1) 
        {
            int typeIndex = (int)unitType;
            //unitDatas[unitIndex] = nomalUnitPools.NomalUnitPoolArray[typeIndex].GetPoolObject();
            //unitDatas[unitIndex].InitDataSetting(
            //    new UnitData(unitIndex, unitName, nomalUnitPools.ScriptableObjectArray[typeIndex].UnitDataBase), 
            //    nomalUnitPools.ScriptableObjectArray[typeIndex].UnitPrefab);

        }
    }

    /// <summary>
    /// 유닛 배열에 데이터를 추가할수 있는지 체크하고 
    /// 유닛 담을수있는크기가 도중에 늘어났으면 늘어난만큼 배열을 확장하여 추가할수있는 위치의 인덱스값을 반환하는 함수 
    /// </summary>
    /// <returns>유닛이 추가될수 있으면 해당 배열의 인덱스값 반환    추가될수 없으면 -1 값</returns>
    public int IsAppendUnitIndex() 
    {
        int forSize = unitDatas.Length;
        int i;
        for (i = 0; i< forSize; i++)         
        {
            if (unitDatas[i] == null) 
            {
                return i;
            }
        }
        if (forSize < maxUnitSize) //기존 배열엔 유닛이꽉차있지만 배열확장이 필요할경우 실행 
        {
            UnitBaseNode[] newUnitDatas = new UnitBaseNode[maxUnitSize];
            for (i = 0; i < forSize; i++) //기존데이터 옮기고 
            {
                newUnitDatas[i] = unitDatas[i];
            }
            unitDatas = newUnitDatas;   //배열 교체 
            return forSize;
        }
        Debug.Log("유닛을 더이상 추가할수 없습니다.");
        return -1;
    }





#if UNITY_EDITOR

    GameSpeedType tempGameSpeedType = GameSpeedType.Nomal;
    private void OnValidate()
    {
        gameSpeedType = tempGameSpeedType;
    }

    [SerializeField]
    bool isDebugCheck = false;
    public bool IsDebugCheck => isDebugCheck;
#endif
}
