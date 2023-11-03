
using System;
using UnityEngine;
/// <summary>
/// 유닛의 기본 데이터 셋팅용 
/// 1. 스크립터블에서 사용
/// 2. 저장에서 사용 
/// 3. 게임내에서 유닛의 각각의 데이터 저장용으로 사용 
/// </summary>
[Serializable]
public struct UnitDataBase
{
    [Header("캐릭터 기본 값")]

    /// <summary>
    /// 해당유닛의 종류 (창병, 활병, 마법병 등등)
    /// </summary>
    [SerializeField]
    BattleUnitType unitType;
    public BattleUnitType UnitType => unitType;

    /// <summary>
    /// 해당유닛의 등급(일반 병사, 영웅)
    /// </summary>
    [SerializeField]
    UnitGrade unitGrade;
    public UnitGrade UnitGrade => unitGrade;

    [SerializeField]
    float defaultMaxHp;
    public float DefaultMaxHP
    {
        get => defaultMaxHp;
        set
        {
            if (defaultMaxHp != value)
            {
                defaultMaxHp = value;
                onDefaultMaxHPChange?.Invoke(defaultMaxHp);
            }
        }
    }
    public Action<float> onDefaultMaxHPChange;

    [SerializeField]
    int strength;
    public int Strength
    {
        get => strength;
        set
        {
            if (strength != value)
            {
                strength = value;
                onStrengthChange?.Invoke(strength);
            }
        }
    }
    public Action<int> onStrengthChange;

    [SerializeField]
    int dexterity;
    public int Dexterity
    {
        get => dexterity;
        set
        {
            if (dexterity != value)
            {
                dexterity = value;
                onDexterityChange?.Invoke(dexterity);
            }
        }
    }
    public Action<int> onDexterityChange;

    [SerializeField]
    int agility;
    public int Agility
    {
        get => agility;
        set
        {
            if (agility != value)
            {
                agility = value;
                onAgilityChange?.Invoke(agility);
            }
        }
    }
    public Action<int> onAgilityChange;

    [SerializeField]
    int constitution;
    public int Constitution
    {
        get => constitution;
        set
        {
            if (constitution != value)
            {
                constitution = value;
                onConstitutionChange?.Invoke(constitution);
            }
        }
    }
    public Action<int> onConstitutionChange;

    [SerializeField]
    int intelligence;
    public int Intelligence
    {
        get => intelligence;
        set
        {
            if (intelligence != value)
            {
                intelligence = value;
                onIntelligenceChange?.Invoke(intelligence);
            }
        }
    }
    public Action<int> onIntelligenceChange;

    [SerializeField]
    int wisdom;
    public int Wisdom
    {
        get => wisdom;
        set
        {
            if (wisdom != value)
            {
                wisdom = value;
                onWisdomChange?.Invoke(wisdom);
            }
        }
    }
    public Action<int> onWisdomChange;

    [SerializeField]
    int luck;
    public int Luck
    {
        get => luck;
        set
        {
            if (luck != value)
            {
                luck = value;
                onLuckChange?.Invoke(luck);
            }
        }
    }
    public Action<int> onLuckChange;

    [SerializeField]
    int registance;
    public int Registance
    {
        get => registance;
        set
        {
            if (registance != value)
            {
                registance = value;
                onRegistanceChange?.Invoke(registance);
            }
        }
    }
    public Action<int> onRegistanceChange;
}

