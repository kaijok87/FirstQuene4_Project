using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���з�
/// </summary>
public enum EquipType 
{
    None= -1,
    Weapone,        // ����         
    Sheild,         // ����
    Armor,          // ���ʷ�
    Helm,           // ������
    Glove,          // �尩��
    Boot,           // �Ź߷�
    Accessories     // �Ǽ��縮��
}

/// <summary>
/// ��� �з� �ɷ�ġ�� 
/// </summary>
public enum EquipStateType 
{
    None = -1,
    PhysicalPower,      //���� ���� Ÿ��
    PhysicalHit,        //���� ���� Ÿ��
    PhysicalDef,        //���� ��� Ÿ��
    PhysicalEvasion,    //���� ȸ�� Ÿ��
    RangedDef,          //���Ÿ� ��� Ÿ��
    RangedEvasion,      //���Ÿ� ȸ�� Ÿ��
    MagicPower,         //���� �Ŀ� Ÿ��
    MagicDef,           //���� ��� Ÿ��
}
/// <summary>
/// �Ҹ�ǰ Ÿ��
/// </summary>
public enum ConsumeType 
{
    None = -1,          //�Ҹ�ȵǴ� ������
    Equip,              //���Ǵ� ������
    Regain,             //ȸ�� ������
    Nerf,               //���� ������
    Buff,               //���� ������
    Flag,               //�̺�Ʈ �÷��� ������
}

/// <summary>
/// ��� ���
/// </summary>
public enum EquipGrade 
{

}

[Serializable]
public struct EquipItem
{
    /// <summary>
    /// �������� ������ȣ 
    /// ���ó : ������ �񱳿��꿡 ���  
    /// </summary>
    [SerializeField]
    int itemIndex;

    [SerializeField]
    EquipType type;
    public EquipType Type => type;
    EquipGrade grade;

    [SerializeField]
    int powerValue;

    /// <summary>
    /// ������ �ٸ��� �񱳿��� ���̱����� ������ �����ε� 
    /// </summary>
    public static bool operator ==(EquipItem a, EquipItem b)
    {
        return a.itemIndex == b.itemIndex;
    }

    public static bool operator !=(EquipItem a, EquipItem b)
    {
        return a.itemIndex != b.itemIndex;
    }

    public override bool Equals(object a)
    {
        if (a is EquipItem b) 
        {
            return b.itemIndex.Equals(itemIndex);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(itemIndex);
    }
}

/// <summary>
/// ��񺯰�� ������ �����ϱ����� ��������Ʈ 
/// </summary>
/// <param name="prevEquip">���� ������� </param>
/// <param name="newEquip">����� �������</param>
public delegate void OnChaneState(in EquipItem prevEquip ,in EquipItem newEquip);

/// <summary>
/// ������ �����ϰ� �ִ� ������ ���� 
/// </summary>
public struct UnitEquipBase
{
    /// <summary>
    /// �޼� ��� 
    /// </summary>
    EquipItem weaponLeft;
    public EquipItem WeaponLeft 
    {
        get => weaponLeft;
        set 
        {
            if (weaponLeft != value) 
            {
                onChaneState?.Invoke(in weaponLeft,in value);
                weaponLeft = value;
            }
        }
    }
    
    /// <summary>
    /// ������ ���
    /// </summary>
    EquipItem weaponRight;
    public EquipItem WeaponRight
    {
        get => weaponRight;
        set
        {
            if (weaponRight != value)
            {
                onChaneState?.Invoke(in weaponRight, in value);
                weaponRight = value;
            }
        }
    }

    /// <summary>
    /// �Ӹ�
    /// </summary>
    EquipItem head;
    public EquipItem Head
    {
        get => head;
        set
        {
            if (head != value)
            {
                onChaneState?.Invoke(in head, in value);
                head = value;
            }
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    EquipItem armor;
    public EquipItem Armor
    {
        get => armor;
        set
        {
            if (armor != value)
            {
                onChaneState?.Invoke(in armor, in value);
                armor = value;
            }
        }
    }

    /// <summary>
    /// ����, �ٸ�
    /// </summary>
    EquipItem leg;
    public EquipItem Leg
    {
        get => leg;
        set
        {
            if (leg != value)
            {
                onChaneState?.Invoke(in leg, in value);
                leg = value;
            }
        }
    }

    /// <summary>
    /// �Ź�
    /// </summary>
    EquipItem foot;
    public EquipItem Foot
    {
        get => foot;
        set
        {
            if (foot != value)
            {
                onChaneState?.Invoke(in foot, in value);
                foot = value;
            }
        }
    }

    /// <summary>
    /// �尩
    /// </summary>
    EquipItem hand;
    public EquipItem Hand
    {
        get => hand;
        set
        {
            if (hand != value)
            {
                onChaneState?.Invoke(in hand, in value);
                hand = value;
            }
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    EquipItem ringLeft;
    public EquipItem RingLeft
    {
        get => ringLeft;
        set
        {
            if (ringLeft != value)
            {
                onChaneState?.Invoke(in ringLeft, in value);
                ringLeft = value;
            }
        }
    }

    /// <summary>
    /// �����ʹ��� 
    /// </summary>
    EquipItem ringRight;
    public EquipItem RingRight
    {
        get => ringRight;
        set
        {
            if (ringRight != value)
            {
                onChaneState?.Invoke(in ringRight, in value);
                ringRight = value;
            }
        }
    }

    /// <summary>
    /// ����� 
    /// </summary>
    EquipItem amulet;
    public EquipItem Amulet
    {
        get => amulet;
        set
        {
            if (amulet != value)
            {
                onChaneState?.Invoke(in amulet, in value);
                amulet = value;
            }
        }
    }

    /// <summary>
    /// ��� ����ɽ� ����� ����� ��ġ�� ������ �����Ű������ ��������Ʈ 
    /// </summary>
    OnChaneState onChaneState;
    public OnChaneState OnChaneState 
    {
        get => onChaneState; 
        set => onChaneState = value; 
    }

    /// <summary>
    /// ���� ������ ������ ��񺰷� �����ؼ� ��ȯ���ִ� �Լ� 
    /// </summary>
    /// <param name="type">���� ��� </param>
    /// <param name="value">������ ��ġ</param>
    /// <returns>����� ������ ��ġ </returns>
    public float TakeDamage(DamageType type, float value) 
    {
        switch (type)
        {
            default:
                break;
        }
        return 0.0f;
    }

    /// <summary>
    /// ���� ������ ��� ������ ������ ���� �Ұ��� ��ȯ�ϴ� �Լ� 
    /// </summary>
    /// <param name="type">���� ��� </param>
    /// <param name="value">�ɷ�ġ ������ ���������갪</param>
    /// <returns>��񿡵��� ������ �߰���</returns>
    public float AttackDamage(DamageType type, float value) 
    {
        switch (type)
        {
            default:
                break;
        }
        return 0.0f;    
    }

}
