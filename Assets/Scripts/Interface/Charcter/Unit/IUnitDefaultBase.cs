using System;
using UnityEngine;

/// <summary>
/// ���� �⺻ �ɷ�ġ Ÿ��
/// </summary>
public enum UnitStateChangeType
{
    None = -1,
    Strength,           //��
    Dexterity,          //�� ����
    Agility,            //��� ��
    Intelligence,       //����
    Wisdom,             //����
    Luck,               //���
    Constitution,       //�ǰ�,ü�� 
    Registance,         //�����̻� ���׷� 
}

/// <summary>
/// ���� ���� ����
/// </summary>
public enum BattleUnitType
{
    Guardian,
    Warrior,
    Mage,
    Archer,
    SpearMan,
    Monk,
}

/// <summary>
/// ������ ����
/// </summary>
public enum UnitGrade 
{
    None = 0,
    Grunt,          //�Ϲ� ����
    Hero ,          //���� 

}

/// <summary>
/// ������ �⺻���� ������ ���ǿ� �������̽�
/// </summary>
public interface IUnitDefaultBase
{
    /// <summary>
    /// ������Ʈ�� �������̱⶧���� �⺻ Ʈ������ ���� ���� 
    /// </summary>
    Transform transform { get; }

    /// <summary>
    /// ������ �⺻ ������ ��Ƶ� ��ü 
    /// </summary>
    UnitData UnitData { get; }


    ITeamUnit CurrentTeam { get; set; }

    /// <summary>
    /// ���� ��ü ��ġ�� ��ȯ�ϱ� 
    /// </summary>
    Vector3 GetFlockingPos { get; }

    /// <summary>
    /// ������ �ɹ� �ε��� ��ȣ 
    /// </summary>
    /// <returns>������ ������ �ɹ���ȣ </returns>
    int GetMemberIndex();

    /// <summary>
    /// ������ �̵��ӵ� ��ȯ
    /// </summary>
    /// <returns>��ȯ�� �̵��ӵ� </returns>
    float GetUnitMoveSpeed();
    /// <summary>
    /// ������ �̵����� ��ȯ
    /// </summary>
    /// <returns>��ȯ�� �̵����� </returns>
    float GetUnitMoveRange();

    /// <summary>
    /// �̵����� ����� 
    /// </summary>
    public UnitMoveController UnitMoveController { get; }

    /// <summary>
    /// �ɹ� ������ ������ ������ ��������Ʈ
    /// </summary>
    Action<IUnitDefaultBase> onDie { get; set; }

    /// <summary>
    /// ���� �ʱ�ȭ�� �Լ� 
    /// </summary>
    /// <param name="unitData">�ʱ�ȭ�� ������</param>
    void InitDataSetting(UnitData unitData);


    /// <summary>
    /// �����͸� �ʱ���·� ������ �Լ� 
    /// </summary>
    void ResetData() 
    {
    }
}
