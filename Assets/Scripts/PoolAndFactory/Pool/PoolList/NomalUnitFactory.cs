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
    /// 생성된 풀 배열
    /// </summary>
    Pool_UnitObject[] nomalUnitPoolArray;
    public Pool_UnitObject[] NomalUnitPoolArray => nomalUnitPoolArray;

    /// <summary>
    /// 생성할 유닛 프리팹 종류 
    /// </summary>
    [SerializeField]
    UnitBaseNode[] prefabs;

    /// <summary>
    /// 유닛 종류별 기본 베이스저장해둔 스크립터블 오브젝트 받아오기 
    /// </summary>
    [SerializeField]
    Scriptable_UnitType_DataBase[] scriptableObjectArray;
    public Scriptable_UnitType_DataBase[] ScriptableObjectArray => scriptableObjectArray;

    /// <summary>
    /// 풀로 다시 돌려줄 델리게이트
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
