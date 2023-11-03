using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� 
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
/// ���� �������ʴ� �⺻ ���� 
/// </summary>
[Serializable]
public struct BattleMapData 
{
    /// <summary>
    /// ���� ���� 
    /// </summary>
    [SerializeField]
    MapType type;
    public MapType Type => type;
    /// <summary>
    /// ���� ���� �̺�Ʈ ���� 
    /// </summary>
    [SerializeField]
    EventData[] eventDatas; //�̷��� �����ϸ� ����Ÿ��
    public EventData[] EventDatas => eventDatas; 

    /// <summary>
    /// ����ʿ����� ������ ���� ����
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
/// ���ϳ��ϳ����� ���ֵ��� ������������ �󸶳� �������������� �ʱ� ���� �����ص� ����ü
/// </summary>
[Serializable]
public struct TeamFlockingPositions
{
    /// <summary>
    /// �������� �ɹ����� ������
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
/// ��Ʋ �ʿ��� NonPlayer�� Ǯ���� �����ͼ� �����ϱ����� �����͸� ���� Ŭ����
/// </summary>
[Serializable]
public class BattleMapGeneratePoolData
{
    /// <summary>
    /// ������ ������ ������ ���� ���� 
    /// </summary>
    [SerializeField]
    TeamType teamType;
    public TeamType TeamType => teamType;

    /// <summary>
    /// ������ ������ ������ ��� ����
    /// </summary>
    [SerializeField]
    UnitGrade[] unitGrade;
    public UnitGrade[] UnitGrade => unitGrade;

    /// <summary>
    /// ������ ������ ������ ���� ����
    /// </summary>
    [SerializeField]
    int[] teamIndex;
    public int[] TeamIndex => teamIndex;

    /// <summary>
    /// ������ ������ ������ �������� ����
    /// </summary>
    [SerializeField]
    int[] memberIndex;
    public int[] MemberIndex => memberIndex;

    /// <summary>
    /// ������ ������ ������ ���� ���ֵ����� ������ ����
    /// </summary>
    [SerializeField]
    int[] unitIndex;
    public int[] UnitIndex => unitIndex;

    /// <summary>
    /// ������ ������ ������ ���� 
    /// </summary>
    [SerializeField]
    Scriptable_UnitType[] generateUnitType;
    public Scriptable_UnitType[] GenerateUnitType => generateUnitType;

    /// <summary>
    /// ���� ���� ������������ �ɹ��� ������������ �迭 
    /// </summary>
    [SerializeField]
    TeamFlockingPositions[] teamFlockingPos;
    public TeamFlockingPositions[] TeamFlockingPos => teamFlockingPos;  

    /// <summary>
    /// ������ ������ ������ �ʱ� ��ġ��
    /// </summary>
    [SerializeField]
    Vector3 initPos;
    public  Vector3 InitPos => initPos; //������ �߻�
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
/// �ʰ��� ������ ����� 
/// </summary>
[Serializable]
public class BattleMapDataTable 
{
    /// <summary>
    /// ���� �������� 
    /// </summary>
    int[] levels;
    public int[] Levels => levels;

    /// <summary>
    /// ���� ���� ���� ���� 
    /// </summary>
    [SerializeField]
    BattleMapData[] mapDatas;   //Array �̴ϱ� ����Ÿ��
    public BattleMapData[] MapDatas => mapDatas;

    /// <summary>
    /// �ʱ� ������ ���ÿ� �Լ�
    /// </summary>
    public void InitDataSetting()
    {

    }

    /// <summary>
    /// ���� ���� ������ �Լ� 
    /// </summary>
    /// <param name="index">���� �ε�������</param>
    /// <param name="level">������ �������� </param>
    public void SetLevel(int index, int level)
    {
    }
    /// <summary>
    /// �ʱ� ������ ���ÿ� �Լ�
    /// </summary>
    public void TestInitDataSetting(int length , Vector3[] testPos)
    {
        levels = new int[length];
        mapDatas = new BattleMapData[length];
        for (int i = 0; i < length; i++)
        {
            mapDatas[i].TestInit(length,testPos);
        }
        //foreach (var item in mapDatas) //����ü �迭�� ���� item�� ����ü�� ���簡�Ǿ� �б� �������� ���ȴٰ� ����ȴ� 
        //{
        //    item.TestInit(length);
        //}
    }
}

/*
 ���½� ����Ҹ��
  1. �Ʊ������� �ƴ� ��� ��Ʋ�� �ƴѰ����� ��ȯ�ɶ��� ������ Ǯ�� ���� �������Ѵ�. 
  2. �Ʊ� �� �� ������ �ʱ�ȭ ����(Ÿ��Ʋ�� ���ư��ų� ���� �ε�)�϶��� ���� Ǯ�ε������Ѵ� 
    2-1. �Ʊ��� ��Ʋ���� �ƴѰ����� �̵��Ҷ��� �ʱ� ��ġ(Position)�� ���������Ѵ�.
    2-2. ������� ����� �ʱ� ���������� ������ �Ǿ���Ѵ�.
 */
/// <summary>
/// ��Ʋ�ʿ��� ����� ���� ������ �����ϰ� ó���ϴ� �Ŵ��� ���� 
/// </summary>
public class BattleUnitGenerateManager : NomalSingleton<BattleUnitGenerateManager>
{
    //== ��ü �� ���� 

   
    
    //=== ���ϳ��� ���� �� ����Ʈ 

    /// <summary>
    /// �߸� �� ( ��� , ���� �� ������ )
    /// </summary>
    [SerializeField]
    List<ITeamUnit> neutralTeam;
    public List<ITeamUnit> NeutralTeam => neutralTeam;

    /// <summary>
    /// ���� ��
    /// </summary>
    [SerializeField]
    List<ITeamUnit> enemyTeam;
    public List<ITeamUnit> EnemyTeam => enemyTeam;

    /// <summary>
    /// �Ʊ� ��
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
    /// ��Ʋ�� ���۽� ����κ� ������ ���� 
    /// <param name="mapIndex">�ʱ�ȭ�� ���� �ε�����</param>
    /// </summary>
    public void BattleDataSetting(int mapIndex )
    {
        BattleMapData mapData = MapDataGenerateManager.Instance.BattleMapDataTable.MapDatas[mapIndex]; //���� �ʱ�ȭ ������ �������� 
        BattleMapGeneratePoolData[] teamArray = mapData.Teams;  // ������ ������Ʈ �迭 �����ͼ� 
        if (teamArray != null)      //������ �� ������ ����
        {
            int teamSize = teamArray.Length;    
            if (teamSize > 0) //������ ���� �����Ҷ�
            {
                TeamType type = TeamType.None;                                          // �� �� Ÿ�� üũ������ �̸�����
                TeamBaseNode teamObject = null;                                         // �� ���� �̸����� 
                UnitBaseNode[] unitBaseNodes;
                BattleMapGeneratePoolData teams;                                         //
                UnitFactory unitFactory = GameManager.Instance.UnitPools;               //������ ���丮 �̸��������� 
                int unitSize = 0;
                int typeIndex = 0;
                for (int i = 0; i < teamSize; i++)
                {
                    teams = teamArray[i];                                                    // �ε����� �ش��ϴ� ��ü �̸� ���� 
                    if (teams != null)                                                   // ������ �����ϸ� �߰�
                    {
                        unitSize = teams.UnitIndex.Length;                                           // ���� �迭ũ�� �������� 
                        teamObject = (TeamBaseNode)BattleMapObjectFactory.Instance.GetTeamUnit();// �� ���� ���丮���� �������� 
                        teamObject.transform.SetParent(null);
                        teamObject.gameObject.SetActive(true);
                        unitBaseNodes = new UnitBaseNode[unitSize];                                 // �߰��� ���ֹ迭 ���������
                        type = teams.TeamType;                                                       // �� Ÿ�� ���ϰ� 
                        for (int j = 0; j < unitSize; j++)
                        {
                            typeIndex = (int)teams.GenerateUnitType[j];                      //�ڵ������� Ǯ�ε����� ��ȯ
                            switch (teams.UnitGrade[j])
                            {
                                case UnitGrade.None:
                                                    //��� �� ���� ������Ʈ�� �߰� 
                                    break;

                                case UnitGrade.Grunt:   //�Ϲݺ��� ����
                                    unitBaseNodes[j] = unitFactory.NomalUnitPoolArray[typeIndex].GetPoolObject();
                                    unitBaseNodes[j].transform.SetParent(teamObject.transform);
                                    UnitDataSetting(unitBaseNodes[j], mapData);
                                    break;
                                case UnitGrade.Hero:    //���� ����
                                    unitBaseNodes[j] = unitFactory.HeroUnitPoolArray[typeIndex].GetPoolObject();
                                    unitBaseNodes[j].transform.SetParent(teamObject.transform);
                                    UnitDataSetting(unitBaseNodes[j], mapData);
                                    break;
                                default:
                                    Debug.Log("���� ����� �����ϴ�.");
                                    break;
                            }
                        }
                        teamObject.SetFlockingPosArray(teams.TeamFlockingPos[i].MemberIntVector);
                        teamObject.InitDataSetting(type,unitBaseNodes ,teams.InitPos); //�� �⺻ �� ���� 
                    }
                    if (teamObject != null) //�߰��� �迭�� �����ϸ� 
                    {
                        switch (type) //�˻��ؼ� �߰� 
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
                    BattleInputManager.Instance.OnChangedTeam(0);                           //�̵� ���� ����
                }
                
            }
            else 
            {
                Debug.Log("������ ����̾���");
            }
        }
    }

    /// <summary>
    /// �ʱ�ȭ �̵����� ���ᵵ �ʿ��ѰŰ��� 
    /// </summary>
    /// <param name="unit"></param>
    private void UnitDataSetting(UnitBaseNode unit ,BattleMapData mapData) 
    {
        unit.gameObject.SetActive(true);
    }
  

    /// <summary>
    /// Ÿ�Կ� �´� ���� �߰��Ѵ�.
    /// </summary>
    /// <param name="teamType">�߰��� �� ����</param>
    /// <param name="teamUnits">�߰��� �� ������ ���</param>
    private void AppendTeam(TeamType type, IUnitDefaultBase[] teamUnits, Vector3 initPos)
    {
        ITeamUnit teamType = BattleMapObjectFactory.Instance.GetTeamUnit(); //���� ������Ʈ �߰� 

        teamType.InitDataSetting(type,teamUnits, initPos); 
        
        switch (type)
        {
            case TeamType.None:
                break;
            case TeamType.Neutral:
                neutralTeam.Add(teamType);  //������ �߰� 
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
    /// Ÿ�Կ� �´� ���� �����.
    /// �׻� ���ٴҰ��̰� ���� �پ��� ���� �����ϱ����� �ۼ�
    /// </summary>
    /// <param name="type">������ �� ����</param>
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
    /// �� ��ȯ �� ��Ʋ ��Ȳ���� �ʱ�ȭ �� �Լ�
    /// </summary>
    public void ResetData_SceneMove() 
    {
        neutralTeam.Clear();
        enemyTeam.Clear();
    }

    /// <summary>
    /// Ÿ��Ʋ �� �ε� �� ������ �ʱ�ȭ�� �Լ� 
    /// �Ŵ����ȿ� �����͸� �ʱ����(���ӽ���� �ʱⰪ)�� ���������� �Լ� 
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
