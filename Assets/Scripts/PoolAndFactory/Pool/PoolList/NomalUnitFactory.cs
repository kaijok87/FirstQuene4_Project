using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public enum Scriptable_UnitType
{
    Warrior = 0,
    Guardian,
    SpearMan,
    Monk,
    Archer,
    Mage,
}
public class NomalUnitFactory : MonoBehaviour
{
    /// <summary>
    /// ������ Ǯ �迭
    /// </summary>
    Pool_UnitObject[] nomalUnitPoolArray;
    public Pool_UnitObject[] NomalUnitPoolArray => nomalUnitPoolArray;

    /// <summary>
    /// ������ ���� ������ ���� 
    /// </summary>
    [SerializeField]
    UnitBaseNode[] prefabs;

    /// <summary>
    /// ���� ������ �⺻ ���̽������ص� ��ũ���ͺ� ������Ʈ �޾ƿ��� 
    /// </summary>
    [SerializeField]
    Scriptable_UnitType_DataBase[] scriptableObjectArray;
    public Scriptable_UnitType_DataBase[] ScriptableObjectArray => scriptableObjectArray;

    /// <summary>
    /// Ǯ�� �ٽ� ������ ��������Ʈ
    /// </summary>
    public Action<Pool_UnitObject>[] onReturns;

    private void Awake()
    {
        int forSize =  prefabs.Length;
        nomalUnitPoolArray = new Pool_UnitObject[forSize];
        GameObject obj;
        for (int i = 0; i < forSize; i++)
        {
            obj = new GameObject($"{prefabs[i].name}_Pool");
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            nomalUnitPoolArray[i] = obj.AddComponent<Pool_UnitObject>();
            nomalUnitPoolArray[i].SetPrefab(prefabs[i]);
            obj.SetActive(true);
        }
    }
}
