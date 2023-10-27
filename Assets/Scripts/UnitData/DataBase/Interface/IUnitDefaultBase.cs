using System;
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
/// 유닛 직업 종류
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
/// 유닛의 기본 데이터 셋팅용 
/// 1. 스크립터블에서 사용
/// 2. 저장에서 사용 
/// 3. 게임내에서 유닛의 각각의 데이터 저장용으로 사용 
/// </summary>
[Serializable]
public struct UnitDataBase
{
    [Header("캐릭터 기본 값")]
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
/// 유닛 데이터 저장용도로 사용할 상태값
/// </summary>
[Serializable]
public struct UnitStateData 
{
    /// <summary>
    /// 유닛의 이름값
    /// </summary>
    [SerializeField]
    string unitName;
    public string UnitName { get => unitName; set => unitName = value; }

    /// <summary>
    /// 유닛의 레벨
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
    /// 유닛의 현재 피로도 
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
/// 유닛의 실시간 데이터 처리용 
/// </summary>
[Serializable]
public struct UnitRealTimeState 
{
    /// <summary>
    /// 유닛 현재 상태
    /// 공격 , 피격 , 마법 , 이동 , 대기 , 도망 
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
    /// 유닛의 현재 체력
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
    /// 물리 공격력
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
    /// 물리 공격확율
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
    /// 물리 방어력
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
    /// 물리 회피확율
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
    /// 공격 범위
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
    /// 공격 속도
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
    /// 원거리 방어력
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
    /// 원거리 공격 회피 확율
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
    /// 마법 시전 속도
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
    /// 마법 공격 타격될 확율
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
    /// 마법 공격력
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
    /// 마법 방어력
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
    /// 크리티컬 확율
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
    /// 크리티컬 데미지
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
    /// 이동 속도
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
    /// 이동 범위
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
/// 유닛 각각의 개체를 관리할 클래스 
/// </summary>
[Serializable]
public class UnitData
{
    /// <summary>
    /// 유닛 개체를 관리할 인덱스값 
    /// </summary>
    [SerializeField]
    int unitIndex = -1;
    public int UnitIndex => unitIndex;

    /// <summary>
    /// 유닛의 정적으로 생성된 데이터 저장할 구조체 
    /// </summary>
    [SerializeField]
    UnitDataBase unitDataBase;
    public UnitDataBase UnitDataBase => unitDataBase;

    /// <summary>
    /// 유닛의 동적으로 생성된 데이터 저장할 구조체 
    /// </summary>
    [SerializeField]
    UnitStateData unitStateData;
    public UnitStateData UnitStateData => unitStateData;


    /// <summary>
    /// 장비하고있는 장비클래스 
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
/// 유닛의 기본적인 데이터 정의용 인터페이스
/// </summary>
public interface IUnitDefaultBase
{

    /// <summary>
    /// 유닛의 기본 정보를 담아둘 객체 
    /// </summary>
    UnitData UnitData { get; }

    /// <summary>
    /// 유닛의 프리팹 모델 
    /// </summary>
    public GameObject UnitPrefab { get; }

    /// <summary>
    /// 전투맵에서사용될 능력치들
    /// </summary>
    UnitRealTimeState UnitRealTimeState { get; }

    /// <summary>
    /// 맴버 유닛이 죽을때 연결할 델리게이트
    /// </summary>
    Action onDie { get; set; }

    /// <summary>
    /// 맴버의 유닛 초기 데이터 셋팅용 함수 
    /// </summary>
    /// <param name="unitData">맴버에 선택된 유닛</param>
    /// <param name="unitPrefab">맴버에 선택된 유닛 프리팹모델</param>
    void InitDataSetting(UnitData unitData,GameObject unitPrefab);

    /// <summary>
    /// 맴버의 유닛 데이터 수정용 함수
    /// </summary>
    /// <param name="unitData">수정할 유닛 데이터</param>
    void InitDataSetting(UnitData unitData);

    /// <summary>
    /// 데이터를 초기상태로 돌리는 함수 
    /// </summary>
    void ResetData() 
    {
    }
}
