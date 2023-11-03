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
    /// ĳ���� �̹��� 
    /// </summary>
    [SerializeField]
    Sprite unitSprite;
    
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
    /// ���� ���� �̹���
    /// </summary>
    [SerializeField]
    Sprite killerUnitSprite;

    /// <summary>
    /// ���� ����
    /// </summary>
    [SerializeField]
    string killerUnitName;
    public DeadUnitTableData(Sprite type , string name , Sprite killerUnitType, string killerUnit, string time, string zone)
    {
        unitSprite = type;
        unitName = name;
        killerUnitSprite = killerUnitType;
        killerUnitName = killerUnit;
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
        Sprite unitImage = GetUnitImage(unit);
        string name = unit.GetUnitName();

        Sprite killerUnitImage = GetUnitImage(killerUnit);
        string killerName = killerUnit.GetUnitName();

        string deadTime = DateTime.Now.ToString();

        DeadUnitList.Add(new DeadUnitTableData(unitImage, name, killerUnitImage, killerName, deadZone, deadTime));
    }
    /// <summary>
    /// ������ �������� ��ȯ�ϴ� �Լ� 
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    private Sprite GetUnitImage(UnitData unit) 
    {
        return null;
    }
    /// <summary>
    /// Ÿ��Ʋ�� �̵��� �ʱ�ȭ �� �Լ� 
    /// </summary>
    public void ResetData() 
    {
        DeadUnitList.Clear();
    }

}
