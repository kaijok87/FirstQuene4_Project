using System;
using UnityEngine;



/// <summary>
/// 유닛 타입별 기본 데이터 셋팅용
/// </summary>
[CreateAssetMenu(fileName = "UnitBase", menuName = "CreateUnit_Type", order = 1)]
public class Scriptable_UnitType_DataBase : ScriptableObject
{
    /// <summary>
    /// 스크립터블 의 데이터의 구조체 맴버는 Instance과정에서 같은 해시 값을 가진다.
    /// </summary>
    [SerializeField]
    UnitDataBase unitBaseData;
    public UnitDataBase UnitDataBase  => unitBaseData;

    /// <summary>
    /// 해당 유닛의 기본 프리팹
    /// </summary>
    [SerializeField]
    GameObject unitPrefab;
    public GameObject UnitPrefab => unitPrefab;
}
