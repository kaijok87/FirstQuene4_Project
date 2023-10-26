using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 죽인 유닛 직렬화하여 json 저장용으로 사용 
/// </summary>
[Serializable]
public class DeadUnitTableData 
{

    /// <summary>
    /// 캐릭터 이미지 보여주기위한 타입설정
    /// </summary>
    [SerializeField]
    BattleUnitType unitType;
    
    /// <summary>
    /// 캐릭터 이름
    /// </summary>
    [SerializeField]
    string unitName;

    /// <summary>
    /// 죽은 시간
    /// </summary>
    [SerializeField]
    string deadTime;
    
    /// <summary>
    /// 죽은 위치
    /// </summary>
    [SerializeField]
    string deadZone;

    /// <summary>
    /// 죽인 유닛 타입
    /// </summary>
    [SerializeField]
    BattleUnitType killerUnitType;

    /// <summary>
    /// 죽인 유닛
    /// </summary>
    [SerializeField]
    string killerUnit;
    public DeadUnitTableData(BattleUnitType type , string name , BattleUnitType killerUnitType, string killerUnit, string time, string zone)
    {
        unitType = type;
        unitName = name;
        this.killerUnitType = killerUnitType;
        this.killerUnit = killerUnit;
        deadTime = time;
        deadZone = zone;
    }
}

public class DeadUnitTable
{
    public List<DeadUnitTableData> DeadUnitList;

    public DeadUnitTable() 
    {
        DeadUnitList = new List<DeadUnitTableData>();
    }

    /// <summary>
    /// 로드시 저장된 배열받아서 리스트로 저장하는함수
    /// </summary>
    /// <param name="deadUnitArray">로드시 받아올 죽은 유닛배열</param>
    public void InitDeadUnit(DeadUnitTableData[] deadUnitArray) 
    {
        DeadUnitList.Clear();
        DeadUnitList.AddRange(deadUnitArray);
    }

    /// <summary>
    /// 죽은유닛의 내용을 배열로 받아오는 함수 
    /// 저장시 사용 
    /// </summary>
    /// <returns>죽은유닛의 배열 값</returns>
    public DeadUnitTableData[] GetDeadUnitArray()
    {
        return DeadUnitList.ToArray();
    }

    /// <summary>
    /// 유닛이 죽었을때 리스트에 추가하는 함수 
    /// </summary>
    /// <param name="unit">죽은 유닛</param>
    /// <param name="killerUnit">죽인 유닛</param>
    /// <param name="deadZone">죽인 위치</param>
    public void AddDeadUnitData(UnitData unit, UnitData killerUnit ,string deadZone)
    {
        BattleUnitType type = unit.UnitDataBase.UnitType;
        string name = unit.UnitStateData.UnitName;

        BattleUnitType killerUnitType = killerUnit.UnitDataBase.UnitType;
        string killerName = killerUnit.UnitStateData.UnitName;

        string deadTime = DateTime.Now.ToString();

        DeadUnitList.Add(new DeadUnitTableData(type, name, killerUnitType, killerName, deadZone, deadTime));
    }

    /// <summary>
    /// 타이틀로 이동시 초기화 할 함수 
    /// </summary>
    public void ResetData() 
    {
        DeadUnitList.Clear();
    }

}
