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
/// ������ �⺻ ������ ���ÿ� 
/// 1. ��ũ���ͺ��� ���
/// 2. ���忡�� ��� 
/// 3. ���ӳ����� ������ ������ ������ ��������� ��� 
/// </summary>
[Serializable]
public struct UnitDataBase
{
    [Header("ĳ���� �⺻ ��")]
    [SerializeField]
    BattleUnitType unitType;
    public BattleUnitType UnitType => unitType;

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

    public UnitDataBase(UnitDataBase defaultData) 
    {
        this = defaultData;
    }

}


/// <summary>
/// ���� ������ ����뵵�� ����� ���°�
/// </summary>
[Serializable]
public struct UnitStateData 
{
    /// <summary>
    /// ������ �̸���
    /// </summary>
    [SerializeField]
    string unitName;
    public string UnitName { get => unitName; set => unitName = value; }

    /// <summary>
    /// ������ ����
    /// </summary>
    [SerializeField]
    int level;
    public int Level
    {
        get => level;
        set
        {
            if (level != value)
            {
                level = value;
                onLevelChange?.Invoke(level);
            }
        }
    }
    public Action<int> onLevelChange;   

    /// <summary>
    /// ������ ���� �Ƿε� 
    /// </summary>
    [SerializeField]
    float fatigue;
    public float Fatigue
    {
        get => fatigue;
        set
        {
            if (fatigue != value)
            {
                fatigue = value;
                onFatigueChange?.Invoke(fatigue);
            }
        }
    }
    public Action<float> onFatigueChange;

    public UnitStateData( string unitName ,float fatigue) 
    {
        level = 1;
        this.unitName = unitName;
        this.fatigue = fatigue;
        onFatigueChange = null;
        onLevelChange = null;
    }
}

/// <summary>
/// ������ �ǽð� ������ ó���� 
/// </summary>
[Serializable]
public struct UnitRealTimeState 
{
    /// <summary>
    /// ���� ���� ����
    /// ���� , �ǰ� , ���� , �̵� , ��� , ���� 
    /// </summary>
    [SerializeField]
    UnitCondition condition;
    public UnitCondition Condition 
    { 
        get => condition;
        set 
        {
            if (condition != value) 
            {
                condition = value;
                onConditionChange?.Invoke(condition);
            }
        }
    }
    public Action<UnitCondition> onConditionChange;   

    /// <summary>
    /// ������ ���� ü��
    /// </summary>
    [SerializeField]
    float hp;
    public float HealthPoint 
    {
        get => hp;
        set 
        {
            if (hp != value) 
            {
                hp = value;
                if (hp < 0.0f) 
                {
                    hp = 0.0f;
                }
                onHpChange?.Invoke(hp);
            } 
        }
    }
    public Action<float> onHpChange;

    /// <summary>
    /// ���� ���ݷ�
    /// </summary>
    [SerializeField]
    float physicalAttackPower;
    public float PhysicalAttackPower 
    { 
        get => physicalAttackPower;
        set 
        {
            if (physicalAttackPower != value) 
            {
                physicalAttackPower = value;
                if (physicalAttackPower < 0.0f) 
                {
                    physicalAttackPower = 0.0f;
                }
                onPhysicalPowerChange?.Invoke(physicalAttackPower);
            }
        }
    }
    public Action<float> onPhysicalPowerChange;

    /// <summary>
    /// ���� ����Ȯ��
    /// </summary>
    [SerializeField]
    float physicalAttackHit;
    public float PhysicalAttackHit
    {
        get => physicalAttackHit;
        set 
        {
            if (physicalAttackHit != value) 
            {
                physicalAttackHit = value;
                if (physicalAttackHit < 0.0f) 
                {
                    physicalAttackHit = 0.0f;
                }
                onPhysicalAttackHitChange?.Invoke(physicalAttackHit);
            }
        }
    }
    public Action<float> onPhysicalAttackHitChange;

    /// <summary>
    /// ���� ����
    /// </summary>
    [SerializeField]
    float physicalDefence;
    public float PhysicalDefence 
    { 
        get => physicalDefence;
        set 
        {
            if (physicalDefence != value) 
            {
                physicalDefence = value;
                if (physicalDefence <  0.0f)
                {
                    physicalDefence = 0.0f;
                }
                onPhysicalDefenceChange?.Invoke(physicalDefence);
            } 
        }
    }
    public Action<float> onPhysicalDefenceChange;

    /// <summary>
    /// ���� ȸ��Ȯ��
    /// </summary>
    [SerializeField]
    float physicalEvasion;
    public float PhysicalEvasion
    {
        get => physicalEvasion;
        set
        {
            if (physicalEvasion != value)
            {
                physicalEvasion = value;
                if (physicalEvasion < 0.0f)
                {
                    physicalEvasion = 0.0f;
                }
                onPhysicalEvasionChange?.Invoke(physicalEvasion);
            }
        }
    }
    public Action<float> onPhysicalEvasionChange;

    /// <summary>
    /// ���� ����
    /// </summary>
    [SerializeField]
    float attackRange;
    public float AttackRange 
    {
        get => attackRange;
        set
        {
            if (attackRange != value)
            {
                attackRange = value;
                if (attackRange < 0.0f)
                {
                    attackRange = 0.0f;
                }
                onAttackRangeChange?.Invoke(attackRange);
            }
        }
    }
    public Action<float> onAttackRangeChange;

    /// <summary>
    /// ���� �ӵ�
    /// </summary>
    [SerializeField]
    float attackSpeed;
    public float AttackSpeed
    {
        get => attackSpeed;
        set
        {
            if (attackSpeed != value)
            {
                attackSpeed = value;
                if (attackSpeed < 0.0f)
                {
                    attackSpeed = 0.0f;
                }
                onAttackSpeedChange?.Invoke(attackSpeed);
            }
        }
    }
    public Action<float> onAttackSpeedChange;

    /// <summary>
    /// ���Ÿ� ����
    /// </summary>
    [SerializeField]
    float rangedDefence;
    public float RangedDefence
    {
        get => rangedDefence;
        set
        {
            if (rangedDefence != value)
            {
                rangedDefence = value;
                if (rangedDefence < 0.0f)
                {
                    rangedDefence = 0.0f;
                }
                onRangedDefenceChange?.Invoke(rangedDefence);
            }
        }
    }
    public Action<float> onRangedDefenceChange;

    /// <summary>
    /// ���Ÿ� ���� ȸ�� Ȯ��
    /// </summary>
    [SerializeField]
    float rangedEvasion;
    public float RangedEvasion
    {
        get => rangedEvasion;
        set
        {
            if (rangedEvasion != value)
            {
                rangedEvasion = value;
                if (rangedEvasion < 0.0f)
                {
                    rangedEvasion = 0.0f;
                }
                onRangedEvasionChange?.Invoke(rangedEvasion);
            }
        }
    }
    public Action<float> onRangedEvasionChange;

    /// <summary>
    /// ���� ���� �ӵ�
    /// </summary>
    [SerializeField]
    float magicCastSpeed;
    public float MagicCastSpeed  
    {
        get => magicCastSpeed;
        set
        {
            if (magicCastSpeed != value)
            {
                magicCastSpeed = value;
                if (magicCastSpeed < 0.0f)
                {
                    magicCastSpeed = 0.0f;
                }
                onMagicCastSpeedChange?.Invoke(magicCastSpeed);
            }
        }
    }
    public Action<float> onMagicCastSpeedChange;

    /// <summary>
    /// ���� ���� Ÿ�ݵ� Ȯ��
    /// </summary>
    [SerializeField]
    float magicHit;
    public float MagicHit  
    {
        get => magicHit;
        set
        {
            if (magicHit != value)
            {
                magicHit = value;
                if (magicHit < 0.0f)
                {
                    magicHit = 0.0f;
                }
                onMagicHitChange?.Invoke(magicHit);
            }
        }
    }
    public Action<float> onMagicHitChange;

    /// <summary>
    /// ���� ���ݷ�
    /// </summary>
    [SerializeField]
    float magicPower;
    public float MagicPower
    {
        get => magicPower;
        set
        {
            if (magicPower != value)
            {
                magicPower = value;
                if (magicPower < 0.0f)
                {
                    magicPower = 0.0f;
                }
                onMagicPowerChange?.Invoke(magicPower);
            }
        }
    }
    public Action<float> onMagicPowerChange;

    /// <summary>
    /// ���� ����
    /// </summary>
    [SerializeField]
    float magicDef;
    public float MagicDef
    {
        get => magicDef;
        set
        {
            if (magicDef != value)
            {
                magicDef = value;
                if (magicDef < 0.0f)
                {
                    magicDef = 0.0f;
                }
                onMagicDefChange?.Invoke(magicDef);
            }
        }
    }
    public Action<float> onMagicDefChange;

    /// <summary>
    /// ũ��Ƽ�� Ȯ��
    /// </summary>
    [SerializeField]
    float criticalProbability;
    public float CriticalProbability
    {
        get => criticalProbability;
        set
        {
            if (criticalProbability != value)
            {
                criticalProbability = value;
                if (criticalProbability < 0.0f)
                {
                    criticalProbability = 0.0f;
                }
                onCriticalProbabilityChange?.Invoke(criticalProbability);
            }
        }
    }
    public Action<float> onCriticalProbabilityChange;

    /// <summary>
    /// ũ��Ƽ�� ������
    /// </summary>
    [SerializeField]
    float criticalDamage;
    public float CriticalDamage
    {
        get => criticalDamage;
        set
        {
            if (criticalDamage != value)
            {
                criticalDamage = value;
                if (criticalDamage < 0.0f)
                {
                    criticalDamage = 0.0f;
                }
                onCriticalDamageChange?.Invoke(criticalDamage);
            }
        }
    }
    public Action<float> onCriticalDamageChange;

    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            if (moveSpeed != value)
            {
                moveSpeed = value;
                if (moveSpeed < 0.0f)
                {
                    moveSpeed = 0.0f;
                }
                onMoveSpeedChange?.Invoke(moveSpeed);
            }
        }
    }
    public Action<float> onMoveSpeedChange;

    /// <summary>
    /// �̵� ����
    /// </summary>
    [SerializeField]
    float moveRange;
    public float MoveRange
    {
        get => moveRange;
        set
        {
            if (moveRange != value)
            {
                moveRange = value;
                if (moveRange < 0.0f)
                {
                    moveRange = 0.0f;
                }
                onMoveRangeChange?.Invoke(moveRange);
            }
        }
    }
    public Action<float> onMoveRangeChange;
}

/// <summary>
/// ���� ������ ��ü�� ������ Ŭ���� 
/// </summary>
[Serializable]
public class UnitData
{
    /// <summary>
    /// ���� ��ü�� ������ �ε����� 
    /// </summary>
    [SerializeField]
    int unitIndex = -1;
    public int UnitIndex => unitIndex;

    /// <summary>
    /// ������ �������� ������ ������ ������ ����ü 
    /// </summary>
    [SerializeField]
    UnitDataBase unitDataBase;
    public UnitDataBase UnitDataBase => unitDataBase;

    /// <summary>
    /// ������ �������� ������ ������ ������ ����ü 
    /// </summary>
    [SerializeField]
    UnitStateData unitStateData;
    public UnitStateData UnitStateData => unitStateData;


    /// <summary>
    /// ����ϰ��ִ� ���Ŭ���� 
    /// </summary>
    [SerializeField]
    UnitEquipBase unitEquipBase;
    UnitEquipBase UnitEquipBase => unitEquipBase;

    public void OnDataChange(int unitIndex, UnitDataBase unitDataBase, UnitStateData unitStateData)
    {
        this.unitIndex = unitIndex;
        this.unitDataBase = unitDataBase;
        this.unitStateData = unitStateData;
    }

    public UnitData(int index , string unitName, UnitDataBase unitTypeDefaultData) 
    {
        this.unitIndex = index;
        unitDataBase = unitTypeDefaultData;
        unitStateData = new UnitStateData(unitName,0.0f);
    }
}

/// <summary>
/// ������ �⺻���� ������ ���ǿ� �������̽�
/// </summary>
public interface IUnitDefaultBase
{

    /// <summary>
    /// ������ �⺻ ������ ��Ƶ� ��ü 
    /// </summary>
    UnitData UnitData { get; }

    /// <summary>
    /// ������ ������ �� 
    /// </summary>
    public GameObject UnitPrefab { get; }

    /// <summary>
    /// �����ʿ������� �ɷ�ġ��
    /// </summary>
    UnitRealTimeState UnitRealTimeState { get; }

    /// <summary>
    /// �ɹ� ������ ������ ������ ��������Ʈ
    /// </summary>
    Action onDie { get; set; }

    /// <summary>
    /// �ɹ��� ���� �ʱ� ������ ���ÿ� �Լ� 
    /// </summary>
    /// <param name="unitData">�ɹ��� ���õ� ����</param>
    /// <param name="unitPrefab">�ɹ��� ���õ� ���� �����ո�</param>
    void InitDataSetting(UnitData unitData,GameObject unitPrefab);

    /// <summary>
    /// �ɹ��� ���� ������ ������ �Լ�
    /// </summary>
    /// <param name="unitData">������ ���� ������</param>
    void InitDataSetting(UnitData unitData);

    /// <summary>
    /// �����͸� �ʱ���·� ������ �Լ� 
    /// </summary>
    void ResetData() 
    {
    }
}
