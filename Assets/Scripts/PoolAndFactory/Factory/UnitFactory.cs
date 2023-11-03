using UnityEngine;

public enum Scriptable_UnitType
{
    Archer = 0,
    Guardian,
    Mage,
    Monk,
    SpearMan,
    Warrior,
}
public class UnitFactory : MonoBehaviour
{
    /// <summary>
    /// 생성된 풀 배열
    /// </summary>
    Pool_UnitObject[] nomalUnitPoolArray;
    public Pool_UnitObject[] NomalUnitPoolArray => nomalUnitPoolArray;
    [SerializeField]
    Scriptable_UnitType_DataBase[] nomalScriptableObjects;


    /// <summary>
    /// 영웅 유닛 따로 관리하기위한 풀 
    /// </summary>
    Pool_UnitObject[] heroUnitPoolArray;
    public Pool_UnitObject[] HeroUnitPoolArray => heroUnitPoolArray;

    [SerializeField]
    Scriptable_UnitType_DataBase[] heroScriptableObjects;


    /// <summary>
    /// 게임 기믹 함정 아이템 등등 기타 오브젝트 생성용 풀
    /// </summary>
    Pool_UnitObject[] gameElementArray;
    public Pool_UnitObject[] GameElementArray => gameElementArray;


    private void Awake()
    {
        nomalUnitPoolArray = Unit_PoolGenerate(nomalScriptableObjects);
        heroUnitPoolArray = Unit_PoolGenerate(heroScriptableObjects);
        gameElementArray = ElementsObject_PoolGenerate();
    }
    private Pool_UnitObject[] ElementsObject_PoolGenerate() 
    {
        return null;
    }
    /// <summary>
    /// 유닛 들의 생성해둘 풀 을 만드는 함수
    /// </summary>
    /// <param name="generateScriptables">생성할 풀의 스크립터블오브젝트 배열</param>
    /// <returns></returns>
    private Pool_UnitObject[] Unit_PoolGenerate(Scriptable_UnitType_DataBase[] generateScriptables) 
    {
        int forSize = generateScriptables.Length;
        Pool_UnitObject[] generatePool = new Pool_UnitObject[forSize];
        GameObject obj;
        for (int i = 0; i < forSize; i++)
        {
            obj = new GameObject($"{generateScriptables[i].name}_{i}");
            obj.transform.SetParent(transform);
            generatePool[i] = obj.AddComponent<Pool_UnitObject>();  //풀 컴퍼넌트 추가하고 
            generatePool[i].SetPrefab(generateScriptables[i].UnitPrefab, generateScriptables[i].PoolSizeCapasity); //프리팹과 사이즈 정하고 
            generatePool[i].InitDataSetting();  //풀 안의 내용 만들기 
        }
        return generatePool;
    }
}
