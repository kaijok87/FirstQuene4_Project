using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ����ȭ�Ͽ� json ��������� ��� 
/// </summary>
[Serializable]
public class DeadUnitTableData 
{

    /// <summary>
    /// ĳ���� �̹��� �����ֱ����� Ÿ�Լ���
    /// </summary>
    [SerializeField]
    BattleUnitType unitType;
    
    /// <summary>
    /// ĳ���� �̸�
    /// </summary>
    [SerializeField]
    string unitName;

    /// <summary>
    /// ���� �ð�
    /// </summary>
    [SerializeField]
    string deadTime;
    
    /// <summary>
    /// ���� ��ġ
    /// </summary>
    [SerializeField]
    string deadZone;

    /// <summary>
    /// ���� ���� Ÿ��
    /// </summary>
    [SerializeField]
    BattleUnitType killerUnitType;

    /// <summary>
    /// ���� ����
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
    /// �ε�� ����� �迭�޾Ƽ� ����Ʈ�� �����ϴ��Լ�
    /// </summary>
    /// <param name="deadUnitArray">�ε�� �޾ƿ� ���� ���ֹ迭</param>
    public void InitDeadUnit(DeadUnitTableData[] deadUnitArray) 
    {
        DeadUnitList.Clear();
        DeadUnitList.AddRange(deadUnitArray);
    }

    /// <summary>
    /// ���������� ������ �迭�� �޾ƿ��� �Լ� 
    /// ����� ��� 
    /// </summary>
    /// <returns>���������� �迭 ��</returns>
    public DeadUnitTableData[] GetDeadUnitArray()
    {
        return DeadUnitList.ToArray();
    }

    /// <summary>
    /// ������ �׾����� ����Ʈ�� �߰��ϴ� �Լ� 
    /// </summary>
    /// <param name="unit">���� ����</param>
    /// <param name="killerUnit">���� ����</param>
    /// <param name="deadZone">���� ��ġ</param>
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
    /// Ÿ��Ʋ�� �̵��� �ʱ�ȭ �� �Լ� 
    /// </summary>
    public void ResetData() 
    {
        DeadUnitList.Clear();
    }

}
