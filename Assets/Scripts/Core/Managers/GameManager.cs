using System;
using UnityEngine;

/*
  ���� 
  1. ���� ������Ʈ ����� ������ �Ŵ��� 
    1-1. Ǯ�� ������ ��� 
        - NPC ���� ������ ������ (����, �߸�NPC, ����NPC ��)
        - ������ �� ��Ÿ ���� ������ ������ (������ , ���� , ��� ��)
        -
    1-2. Ǯ�� ������ �ʱ�ȭ�� ���
        - Ǯ(ť)���� ������ �ʱⵥ���ͼ��� �� �����̵�(�θ� Transform �̵�) 
    1-3. Ǯ�� ������ ������ ���
        - Ǯ(ť)�� ������ �����ǰ� Zero �� ������ 
        - ����� ��������Ʈ ����
        - ���� �ǰ��ִ� ������ ����

  2. ���� ���� ������ ������ �Ŵ��� 
  3. ���� ������Ʈ ���۽�ų �Ŵ���
 
 */


/// <summary>
/// ���� �ӵ� ������ �̳Ѱ�
/// ����� ������ int ���� �����Ѵ�.
/// </summary>
public enum GameSpeedType 
{
    Stop            = 0,            // ���� 0���
    OneFourth       = 25,           // 1/4���
    HalfSlow        = 50,           // 1/2���
    Nomal           = 100,          // 1  ���
    DoubleFast      = 200,          // 2  ���
    QuadrupleFast   = 400,          // 4  ���
    OctupleFast     = 800,          // 8  ���
}




public class GameManager : DontDestroySingleton<GameManager>
{
    /// <summary>
    /// ���Ӽӵ� ������ �̳� ��
    /// ���Ӽӵ� ���꺯�� ������ 
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
    /// BGM �Ҹ� ũ�� ��
    /// </summary>
    [SerializeField]
    float backGroundMusicVolumSize;

    /// <summary>
    /// ����Ʈ �Ҹ� ũ�� ��
    /// </summary>
    [SerializeField]
    float effectSoundVolumSize;


    /// <summary>
    /// �÷��̾ �������ִ� ���� ���� 
    /// </summary>
    [SerializeField]
    int maxUnitSize = 100;

    /// <summary>
    /// �����÷��̵��߿� �Ʊ� ���� �� ��Ƶ� �迭
    /// </summary>
    [SerializeField]
    UnitBaseNode[] unitDatas;
    public UnitBaseNode[] UnitDatas => unitDatas;

    /// <summary>
    /// ������ ���ֿ�����Ʈ���� ���� ��ġ
    /// </summary>
    Transform unitListParent;

    /// <summary>
    /// ���ӵ��� �������� ��Ƶ� Ŭ����
    /// </summary>
    DeadUnitTable deadUnitTable;
    public DeadUnitTable DeadUnitTable => deadUnitTable;


    /// <summary>
    /// ���� ������ Ǯ 
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
    /// �Ϲ� ������ �����ؼ� �迭�� ����ִ� ���� 
    /// </summary>
    /// <param name="unitType">���� ����</param>
    /// <param name="unitName">���� �̸�</param>
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
    /// ���� �迭�� �����͸� �߰��Ҽ� �ִ��� üũ�ϰ� 
    /// ���� �������ִ�ũ�Ⱑ ���߿� �þ���� �þ��ŭ �迭�� Ȯ���Ͽ� �߰��Ҽ��ִ� ��ġ�� �ε������� ��ȯ�ϴ� �Լ� 
    /// </summary>
    /// <returns>������ �߰��ɼ� ������ �ش� �迭�� �ε����� ��ȯ    �߰��ɼ� ������ -1 ��</returns>
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
        if (forSize < maxUnitSize) //���� �迭�� �����̲��������� �迭Ȯ���� �ʿ��Ұ�� ���� 
        {
            UnitBaseNode[] newUnitDatas = new UnitBaseNode[maxUnitSize];
            for (i = 0; i < forSize; i++) //���������� �ű�� 
            {
                newUnitDatas[i] = unitDatas[i];
            }
            unitDatas = newUnitDatas;   //�迭 ��ü 
            return forSize;
        }
        Debug.Log("������ ���̻� �߰��Ҽ� �����ϴ�.");
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
