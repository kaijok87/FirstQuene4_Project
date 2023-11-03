
using System;
using UnityEngine;
public enum DamageType 
{ }

/// <summary>
/// ���� ������ ��ü�� ������ Ŭ���� 
/// �ɹ��� ����ü�� ����ϱ⶧���� ����ü�� �����Ҽ��ִ� ����� ��������� �Ѵ�.
/// ����ü�� �⺻������ �Һ�( ReadOnly �� ����Ѱ��� ) ������ �ݹ��� ���� ������ ���� ����ȴ�. 
/// ����ü�� Set Get ������Ƽ������ϸ� ��ü������ ����ȴ� ref ������ �۵������ʴ´� return ���� ref �� ó�������ʴ´�. 
/// </summary>
[Serializable]
public class UnitData
{
    /// <summary>
    /// ���� ��ü�� ������ �ε����� 
    /// </summary>
    [SerializeField]
    int unitIndex;
    public int UnitIndex => unitIndex;

    /// <summary>
    /// ������ �������� ������ ������ ������ ����ü 
    /// </summary>
    [SerializeField]
    UnitDataBase unitDataBase;

    /// <summary>
    /// ������ �������� ������ ������ ������ ����ü 
    /// </summary>
    [SerializeField]
    UnitStateData unitStateData;

    /// <summary>
    /// ����ϰ��ִ� ���Ŭ���� 
    /// </summary>
    [SerializeField]
    UnitEquipBase unitEquipBase;

  
    public UnitData() 
    {
        unitEquipBase = new UnitEquipBase();        //�⺻Ʋ ��´� 
    }

    /// <summary>
    /// ������ �̸��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>������ �̸�</returns>
    public string GetUnitName() 
    {
        return unitStateData.UnitName;
    }

    public void SetFlockingPos(in Vector3 initPos) 
    {
        unitStateData.ColonyMemberPosition = initPos;   
    }

    /// <summary>
    /// ��� �����û 
    /// ó�� �Ұ͵� 
    /// </summary>
    /// <param name="equipData">������ ��� </param>
    public void ChangeEquip(in EquipItem equipData) 
    {

        switch (equipData.Type) //��� Ÿ������ ���� ó�� 
        {
            case EquipType.None:
                //��� ���� 
                break;
            case EquipType.Weapone:
                //unitEquipBase.WeaponLeft;
                //unitEquipBase.WeaponRight;
                //��������
                // 1. ������ ���� ���� ������������� �޼�
                // 2. ���и� �޼����� ����
                // 3. ����������¿��� ���������� ���� ���� ���� 
                break;
            case EquipType.Sheild:
                break;
            case EquipType.Armor:
                break;
            case EquipType.Helm:
                break;
            case EquipType.Glove:
                break;
            case EquipType.Boot:
                break;
            case EquipType.Accessories:
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// �������� ���� �� ó���� �Լ� 
    /// </summary>
    public void TakeDamage() 
    {

    }
    public float AttackDamage(DamageType type) 
    {
        float damage = 0.0f;
        return damage;
    }

    private void MethodTest() 
    {
    }
    public void OnDataChange(int unitIndex, in UnitDataBase unitDataBase, in UnitStateData unitStateData)
    {
        this.unitIndex = unitIndex;
        this.unitDataBase = unitDataBase;
        this.unitStateData = unitStateData;
    }
  
}