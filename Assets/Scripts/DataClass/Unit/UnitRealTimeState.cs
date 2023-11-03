
using System;
using UnityEngine;
/// <summary>
/// ������ �ǽð� ������ ó���� 
/// </summary>
[Serializable]
public struct UnitRealTimeState
{
    /// <summary>
    /// �������� �ɹ� ��ġ
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
                    onDie?.Invoke();
                }
                onHpChange?.Invoke(hp);
            }
        }
    }
    public Action<float> onHpChange;
    public Action onDie;

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

    /// <summary>
    /// �ɹ����� �ʱ�ȭ �ϴ��Լ� 
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