using System;
using Unity.VisualScripting;
using UnityEngine;

/*
 스크립터블 에 필요한 최소데이터 셋팅 
 1. 유닛 정보 
 2. 유닛의 모델 프리팹 
 3. 초기화에 필요한 델리게이트 및 함수 
  1,2번은 에디터상에서 수정이 가능하게 만든다.
 */

/// <summary>
/// 유닛 타입별 기본 데이터 셋팅용
/// </summary>
[CreateAssetMenu(fileName = "UnitBase", menuName = "CreateUnit_Type", order = 1)]
public class Scriptable_UnitType_DataBase : ScriptableObject
{
    /// <summary>
    /// 유닛 기본정보 
    /// </summary>
    [SerializeField]
    UnitStateData unitStateData;
    public UnitStateData UnitStateData => unitStateData;

    /// <summary>
    /// 스크립터블 의 데이터의 구조체 맴버는 Instance과정에서 같은 해시 값을 가진다.
    /// </summary>
    [SerializeField]
    UnitDataBase unitBaseData;
    public UnitDataBase UnitDataBase  => unitBaseData;


    /// <summary>
    /// 유닛의 프리팹 
    /// </summary>
    [SerializeField]
    UnitBaseNode unitPrefabBase;

    public UnitBaseNode UnitPrefab => unitPrefabBase;
    
    [SerializeField]
    int poolSizeCapasity = 0;
    public int PoolSizeCapasity => poolSizeCapasity;
   
}
