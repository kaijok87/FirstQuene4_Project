using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 유닛 기본 능력치 타입
/// </summary>
public enum UnitStateChangeType
{
    None = -1,
    Strength,           //힘
    Dexterity,          //손 재주
    Agility,            //기민 성
    Intelligence,       //지능
    Wisdom,             //지혜
    Luck,               //행운
    Constitution,       //건강,체질 
    Registance,         //상태이상 저항력 
}

/// <summary>
/// 캐릭터 데이터 테이블
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
