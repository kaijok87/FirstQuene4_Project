using System.IO;
using System.Threading.Tasks;
using UnityEngine;


public class MapDataGenerateManager : NomalSingleton<MapDataGenerateManager>
{
    string directory;
    string filePath ;

    /// <summary>
    /// 맵의 데이터 저장용 클래스 
    /// </summary>
    [SerializeField]
    BattleMapDataTable battleMapDataTable;
    public BattleMapDataTable BattleMapDataTable => battleMapDataTable;


    protected override void Awake()
    {
        directory = $"{Application.dataPath}/Test";
        filePath = "/Test.json";
        base.Awake();
        LoadDataAsync(); //비동기로 실행시키라고 호출만 하는함수 
    }


    public void SaveData(BattleMapDataTable data)
    {
        string fullPath = $"{directory}{filePath}";
        //testMapData
        if (!Directory.Exists(directory))
        {
            Debug.Log("폴더없어");
            Directory.CreateDirectory(directory);
        }
        if (!File.Exists(fullPath))
        {
            Debug.Log("파일없어");
            using (File.Create(fullPath)){}; //반환값인 스트림이 iDispose 를 상속받아서 자원반납을위해 사용 
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