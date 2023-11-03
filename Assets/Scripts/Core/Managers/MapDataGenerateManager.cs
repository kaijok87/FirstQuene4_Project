using System.IO;
using System.Threading.Tasks;
using UnityEngine;


public class MapDataGenerateManager : NomalSingleton<MapDataGenerateManager>
{
    string directory;
    string filePath ;

    /// <summary>
    /// ���� ������ ����� Ŭ���� 
    /// </summary>
    [SerializeField]
    BattleMapDataTable battleMapDataTable;
    public BattleMapDataTable BattleMapDataTable => battleMapDataTable;


    protected override void Awake()
    {
        directory = $"{Application.dataPath}/Test";
        filePath = "/Test.json";
        base.Awake();
        LoadDataAsync(); //�񵿱�� �����Ű��� ȣ�⸸ �ϴ��Լ� 
    }


    public void SaveData(BattleMapDataTable data)
    {
        string fullPath = $"{directory}{filePath}";
        //testMapData
        if (!Directory.Exists(directory))
        {
            Debug.Log("��������");
            Directory.CreateDirectory(directory);
        }
        if (!File.Exists(fullPath))
        {
            Debug.Log("���Ͼ���");
            using (File.Create(fullPath)){}; //��ȯ���� ��Ʈ���� iDispose �� ��ӹ޾Ƽ� �ڿ��ݳ������� ��� 
        }
        string text = JsonUtility.ToJson(data, true);
        File.WriteAllText(fullPath, text);
    }

    public async void LoadDataAsync()
    {
        await LoadFile();
        Debug.LogFormat("");
    }

    private Task LoadFile()
    {
        string fullPath = $"{directory}{filePath}";
        //testMapData
        if (!Directory.Exists(directory))
        {
            return Task.CompletedTask;
        }
        if (!File.Exists(fullPath))
        {
            return Task.CompletedTask;
        }

        string text = File.ReadAllText(fullPath);
        battleMapDataTable = JsonUtility.FromJson<BattleMapDataTable>(text);
        return Task.CompletedTask;
    }

}