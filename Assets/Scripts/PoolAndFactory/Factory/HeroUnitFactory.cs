using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class HeroUnitFactory : MonoBehaviour
{
    /// <summary>
    /// 생성된 풀 배열
    /// </summary>
    Pool_UnitObject[] heroUnitPoolArray;
    public Pool_UnitObject[] HeroUnitPoolArray => heroUnitPoolArray;

    [SerializeField]
    Scriptable_UnitType_DataBase[] prefabs;

    private void Awake()
    {
        int forSize =  prefabs.Length;
        heroUnitPoolArray = new Pool_UnitObject[forSize];
        GameObject obj;
        for (int i = 0; i < forSize; i++)
        {
            obj = new GameObject($"{prefabs[i].name}_{i}");
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            heroUnitPoolArray[i] = obj.AddComponent<Pool_UnitObject>();
            heroUnitPoolArray[i].SetPrefab(prefabs[i].UnitPrefab, prefabs[i].PoolSizeCapasity);
            obj.SetActive(true);
        }
    }
}
