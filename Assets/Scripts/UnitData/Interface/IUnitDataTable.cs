using System;
using System.Collections;
using System.Collections.Generic;
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
/// ĳ���� ������ ���̺�
/// </summary>
interface IUnitDataTable
{
   
    public string UnitName { get; }
    public float MaxHP { get; set; }
    public float HealthPoint { get; set; }
    public float Fatigue { get; set; }
    public UnitCondition Condition { get; set; }
    public uint Level { get; set; }
    public int Strength { get; set; }
    public int PhysicalAttackPower { get; set; }
    public int Dexterity { get; set; }
    public int PhysicalHit { get; set; }
    public int Constitution { get; set; }
    public int PhysicalDefence { get; set; }
    public uint RangedDef { get; set; }
    public int Agility { get; set; }
    public uint PhysicalEvasion { get; set; }
    public uint RangedEvasion { get; set; }
    public int intelligence { get; set; }
    public uint MagicPower { get; set; }
    public int Wisdom { get; set; }
    public uint MagicDef { get; set; }
    public int Luck { get; set; }
    public uint CriticalProbability { get; set; }
    public uint CriticalDamage { get; set; }
    public int Registance { get; set; }
    
}
