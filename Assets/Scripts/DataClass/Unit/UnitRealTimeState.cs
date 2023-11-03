
using System;
using UnityEngine;
/// <summary>
/// 유닛의 실시간 데이터 처리용 
/// </summary>
[Serializable]
public struct UnitRealTimeState
{
    /// <summary>
    /// 팀에서의 맴버 위치
    /// </summary>
    [SerializeField]
    int memberIndex;
    public int MemberIndex
    {
        get => memberIndex;
        set
        {
            if (memberIndex != value)
            {
                memberIndex = value;
                onChangeMemberIndex?.Invoke(memberIndex);
            }
        }
    }
    public Action<int> onChangeMemberIndex;

  

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
                    onDie?.Invoke();
                }
                onHpChange?.Invoke(hp);
            }
        }
    }
    public Action<float> onHpChange;
    public Action onDie;

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
                if (physicalDefence < 0.0f)
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

    /// <summary>
    /// 맴버변수 초기화 하는함수 
    /// </summary>
    public void ResetData()
    {
        this.memberIndex = -1;
        onChangeMemberIndex = null;

        this.condition = UnitCondition.Wait;
        onConditionChange = null;

        this.hp = 0.0f;
        onHpChange = null;

        this.physicalAttackHit = 0.0f;
        onPhysicalAttackHitChange = null;

        this.physicalAttackPower = 0.0f;
        onPhysicalPowerChange = null;

        this.physicalDefence = 0.0f;
        onPhysicalDefenceChange = null;

        this.physicalEvasion = 0.0f;
        onPhysicalEvasionChange = null;

        this.rangedDefence = 0.0f;
        onRangedDefenceChange = null;

        this.rangedEvasion = 0.0f;
        onRangedEvasionChange = null;

        this.magicPower = 0.0f;
        onMagicPowerChange = null;

        this.magicDef = 0.0f;
        onMagicDefChange = null;

        this.magicHit = 0.0f;
        onMagicHitChange = null;

        this.magicCastSpeed = 0.0f;
        onMagicCastSpeedChange = null;

        this.attackRange = 0.0f;
        onAttackRangeChange = null;

        this.attackSpeed = 0.0f;
        onAttackSpeedChange = null;

        this.moveRange = 0.0f;
        onMoveRangeChange = null;

        this.moveSpeed = 0.0f;
        onMoveSpeedChange = null;

        this.criticalDamage = 0.0f;
        onCriticalDamageChange = null;

        this.criticalProbability = 0.0f;
        onCriticalProbabilityChange = null;


    }
}