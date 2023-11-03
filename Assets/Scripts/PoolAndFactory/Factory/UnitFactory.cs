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
    /// ������ Ǯ �迭
    /// </summary>
    Pool_UnitObject[] nomalUnitPoolArray;
    public Pool_UnitObject[] NomalUnitPoolArray => nomalUnitPoolArray;
    [SerializeField]
    Scriptable_UnitType_DataBase[] nomalScriptableObjects;


    /// <summary>
    /// ���� ���� ���� �����ϱ����� Ǯ 
    /// </summary>
    Pool_UnitObject[] heroUnitPoolArray;
    public Pool_UnitObject[] HeroUnitPoolArray => heroUnitPoolArray;

    [SerializeField]
    Scriptable_UnitType_DataBase[] heroScriptableObjects;


    /// <summary>
    /// ���� ��� ���� ������ ��� ��Ÿ ������Ʈ ������ Ǯ
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
    /// ���� ���� �����ص� Ǯ �� ����� �Լ�
    /// </summary>
    /// <param name="generateScriptables">������ Ǯ�� ��ũ���ͺ������Ʈ �迭</param>
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
            generatePool[i] = obj.AddComponent<Pool_UnitObject>();  //Ǯ ���۳�Ʈ �߰��ϰ� 
            generatePool[i].SetPrefab(generateScriptables[i].UnitPrefab, generateScriptables[i].PoolSizeCapasity); //�����հ� ������ ���ϰ� 
            generatePool[i].InitDataSetting();  //Ǯ ���� ���� ����� 
        }
        return generatePool;
    }
}
