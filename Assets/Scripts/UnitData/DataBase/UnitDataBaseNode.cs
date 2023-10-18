using System;
using UnityEngine;

/// <summary>
/// ĳ���� �⺻ �ɷ�ġ ���� Ŭ����
/// </summary>
public class UnitDataBaseNode : MonoBehaviour , IUnitDataTable, IUnitDataBase
{
    [Header("ĳ���� �⺻ ��")]
    /// <summary>
    /// ĳ���� �̸�
    /// </summary>
    [SerializeField]
    string unitName;
    public string UnitName => unitName;
    
    /// <summary>
    /// �ִ� ���� ������ HP ��ġ
    /// </summary>
    [SerializeField]
    float limitHealthPoint = 9999;

    /// <summary>
    /// �ִ� ��
    /// </summary>
    [SerializeField]
    float maxHealthPoint;
    public float MaxHP 
    {
        get => maxHealthPoint;
        set 
        {
            maxHealthPoint = value; //�����ϰ� 
            if (limitHealthPoint < value) // �ִ�ġ���� ũ�� 
            {
                maxHealthPoint = limitHealthPoint; //�ִ�ġ ��ŭ��
            }
            else if (value < 0)  //0���� ������ 
            {
                maxHealthPoint = 1.0f; // 0�̸� �ȵǴ� �ּҰ��� 1��ŭ
            }
            onChangeBattleState?.Invoke(Info_Name.MXHP); 
        }  
    } 

    /// <summary>
    /// ���� ��
    /// </summary>
    [SerializeField]
    float healthPoint;
    public float HealthPoint
    {
        get => healthPoint;
        set 
        {
            //�׾����� üũ�ϰ� 
            if (value < 0) //ü���� 0���ϸ�  
            {
                healthPoint = 0; //0���� ���߰� 
                OnDie(); //�״� ���� ����
                return;
            } 
            //���׾����� 
            healthPoint = value; 
            if (value > maxHealthPoint) 
            {
                healthPoint = maxHealthPoint;

            } 
            onChangeBattleState?.Invoke(Info_Name.HP); //ü�� ��ȭ ��ȣ ������
        }

    }

    /// <summary>
    /// �ִ� ���������� �Ƿε� 
    /// </summary>
    [SerializeField]
    float limitFatigue = 99;

    /// <summary>
    /// �Ƿε� 
    /// </summary>
    [SerializeField]
    float fatigue;
    public float Fatigue 
    {
        get => fatigue;
        set 
        {
            fatigue = value;
            if (limitFatigue < value)
            {
                fatigue = limitFatigue;
            }
            else if (value < 0) 
            {
                fatigue = 0;
            }
            onChangeBattleState?.Invoke(Info_Name.FT);
        }
    }

    /// <summary>
    /// ���� ���� (����,��� ,���� �� ������Ȳ �� �����̻�)
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
                onChangeBattleState?.Invoke(Info_Name.ST);
            }
        }

    }

    /// <summary>
    /// ���� �ִ�ġ
    /// </summary>
    [SerializeField]
    uint limitLevel = 999;
    /// <summary>
    /// ĳ���� ����
    /// </summary>
    [SerializeField]
    uint level;
    public uint Level 
    {
        get => level;
        set 
        {
            level = value;
            if (limitLevel < value)
            {
                level = limitLevel;
            }
            else if (value < 1) 
            {
                level = 1;
            }
            onChangeBattleState?.Invoke(Info_Name.LV);
        }
    }

    /// <summary>
    /// �� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitStrength = 100;

    /// <summary>
    /// �� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int strength;
    public int Strength 
    { 
        get => strength;
        set 
        {
            strength = value;
            if (limitStrength < value)
            {
                strength = limitStrength;
            }
            else if (strength < 1) 
            { 
                strength = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Strength); //���� �ɷ�ġ �������˸�
        }
    }

    /// <summary>
    /// ���� ���ݷ� �ִ�ġ
    /// </summary>
    [SerializeField]
    int limitPhysicalAttackPower = 100;

    /// <summary>
    /// ���� ���ݷ�
    /// </summary>
    [SerializeField]
    int physicalAttackPower;

    public int PhysicalAttackPower 
    {
        get => physicalAttackPower;
        set
        {
            physicalAttackPower += value; //�߰��� �� ���� (���� or �����)
            physicalAttackPower = strength + //�⺻ �����ٰ�  
                UnitEquipData(UnitEquipBase, EquipStateType.PhysicalPower); //���� ������ʹɷ�ġ �߰��ϰ�
            if (limitPhysicalAttackPower < physicalAttackPower) //�ִ�ġ ���� ������ 
            {
                physicalAttackPower = limitPhysicalAttackPower; //�ִ�ġ�� ����
            }
            else if(physicalAttackPower < 0) //0���� ��������
            {
                physicalAttackPower = 0; //0���� ����
            }

            onChangeBattleState?.Invoke(Info_Name.AT); //���� ��ȭ ������ �˸�
        }
    }

    /// <summary>
    /// �� ���� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitDexterity = 100;

    /// <summary>
    /// �� ���� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int dexterity;
    public int Dexterity
    {
        get => dexterity;
        set
        {
            dexterity = value;
            if (limitDexterity < value)
            {
                dexterity = limitDexterity;
            }
            else if (dexterity < 1)
            {
                dexterity = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Dexterity); //���� �ɷ�ġ �������˸�
        }
    }

   

    /// <summary>
    /// ����ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitPhysicalHit = 200;

    /// <summary>
    /// ���� ����
    /// </summary>
    [SerializeField]
    int physicalHit;
    public int PhysicalHit 
    {
        get => physicalHit;
        set 
        {
            physicalHit = value; //�߰��� �� ���� (���� or �����)
            physicalHit = UnitEquipData(UnitEquipBase, EquipStateType.PhysicalHit); //����� ���߷� �����ͼ� ����
            if (limitPhysicalHit < value)
            {
                physicalHit = limitPhysicalHit;
            }
            else if (physicalHit < 0) 
            {
                physicalHit = 0;
            }
            onChangeBattleState?.Invoke(Info_Name.AR);
        }
    }

    /// <summary>
    /// �ǰ� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitConstitution = 100;

    /// <summary>
    /// �ǰ� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int constitution;
    public int Constitution
    {
        get => constitution;
        set
        {
            constitution = value;
            if (limitConstitution < value)
            {
                constitution = limitConstitution;
            }
            else if (Constitution < 1)
            {
                constitution = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Constitution); //���� �ɷ�ġ �������˸�
        }
    }

    /// <summary>
    /// �ִ� ��������
    /// </summary>
    [SerializeField]
    int limitPhysicalDef = 100;

    /// <summary>
    /// ���� ���
    /// </summary>
    [SerializeField]
    int physicalDef;
    public int PhysicalDefence 
    {
        get => physicalDef;
        set 
        {
            physicalDef = value;
            physicalDef = UnitEquipData(UnitEquipBase, EquipStateType.PhysicalDef);
            if (limitPhysicalDef < value) 
            {
                physicalDef = limitPhysicalDef;
            }
            else if (physicalDef < 0)
            {
                physicalDef = 0;
            }
            onChangeBattleState?.Invoke(Info_Name.DF);
        }
    }

    /// <summary>
    /// �ִ� ���Ÿ� ����
    /// </summary>
    [SerializeField]
    uint limitRangedDef = 100;

    /// <summary>
    /// ���Ÿ� ���
    /// </summary>
    [SerializeField]
    uint rangedDef;
    public uint RangedDef
    {
        get => rangedDef;
        set
        {
            rangedDef = value;
            if (limitRangedDef < value)
            {
                rangedDef = limitRangedDef;
            }
            onChangeBattleState?.Invoke(Info_Name.AD);
        }
    }

    /// <summary>
    /// ��ø �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitAgility = 100;

    /// <summary>
    /// ��ø �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int agility;
    public int Agility
    {
        get => agility;
        set
        {
            agility = value;
            if (limitAgility < value)
            {
                agility = limitAgility;
            }
            else if (agility < 1)
            {
                agility = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Agility); //���� �ɷ�ġ �������˸�
        }
    }

    /// <summary>
    /// �ִ� ȸ�� ������ ��ġ
    /// </summary>
    [SerializeField]
    uint limitPhysicalEvasion = 200;

    /// <summary>
    /// ȸ��
    /// </summary>
    [SerializeField]
    uint physicalEvasion;
    public uint PhysicalEvasion 
    {
        get => physicalEvasion;
        set
        {
            physicalEvasion = value;
            if (limitPhysicalEvasion < value) 
            {
                physicalEvasion = limitPhysicalEvasion;
            }
            onChangeBattleState?.Invoke(Info_Name.DR);
        }
    }

    /// <summary>
    /// �ִ� ���Ÿ� ȸ��
    /// </summary>
    [SerializeField]
    uint limitRangedEvasion = 200;

    /// <summary>
    /// ���Ÿ� ȸ��
    /// </summary>
    [SerializeField]
    uint rangedEvasion;
    public uint RangedEvasion 
    {
        get => rangedEvasion;
        set 
        {
            rangedEvasion = value;
            if (limitRangedEvasion < value) 
            {
                rangedEvasion = limitRangedEvasion;
            }
            onChangeBattleState?.Invoke(Info_Name.AA);
        }
    }

    /// <summary>
    /// ���� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitIntelligence = 100;

    /// <summary>
    /// ���� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int Intelligence;
    public int intelligence
    {
        get => Intelligence;
        set
        {
            intelligence = value;
            if (limitIntelligence < value)
            {
                intelligence = limitIntelligence;
            }
            else if (Intelligence < 1)
            {
                intelligence = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Intelligence); //���� �ɷ�ġ �������˸�
        }
    }

    /// <summary>
    /// �ִ� ���� ���ݷ�
    /// </summary>
    [SerializeField]
    uint limitMagicPower = 100;

    /// <summary>
    /// ���� ���ݷ�
    /// </summary>
    [SerializeField]
    uint magicPower;
    public uint MagicPower
    {
        get => magicPower;
        set
        {
            magicPower = value;
            if (limitMagicPower < value)
            {
                magicPower = limitMagicPower;
            }
            onChangeBattleState?.Invoke(Info_Name.MT);
        }
    }

    /// <summary>
    /// ���� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitWisdom = 100;

    /// <summary>
    /// ���� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int wisdom;
    public int Wisdom
    {
        get => wisdom;
        set
        {
            wisdom = value;
            if (limitWisdom < value)
            {
                wisdom = limitWisdom;
            }
            else if (value < 1)
            {
                wisdom = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Wisdom); //���� �ɷ�ġ �������˸�
        }
    }

    /// <summary>
    /// �ִ� ���� ���
    /// </summary>
    [SerializeField]
    uint limitMagicDef = 100;

    /// <summary>
    /// ���� ���
    /// </summary>
    [SerializeField]
    uint magicDef;
    public uint MagicDef 
    {
        get => magicDef;
        set 
        {
            magicDef = value;
            if (limitMagicDef < value) 
            {
                magicDef = limitMagicDef;
            }
            onChangeBattleState?.Invoke(Info_Name.MD);
        }
    }


    /// <summary>
    /// ��� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitLuck = 100;

    /// <summary>
    /// ��� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int luck;
    public int Luck
    {
        get => luck;
        set
        {
            luck = value;
            if (limitLuck < value)
            {
                luck = limitLuck;
            }
            else if (value < 1)
            {
                luck = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Luck); //���� �ɷ�ġ �������˸�
        }
    }

    /// <summary>
    /// �ִ� ũ��Ƽ�� Ȯ��
    /// </summary>
    [SerializeField]
    uint limitCriticalProbability = 100;

    /// <summary>
    /// ũ��Ƽ�� Ȯ��
    /// </summary>
    [SerializeField]
    uint criticalProbability;
    public uint CriticalProbability
    {
        get => criticalProbability;
        set
        {
            criticalProbability = value;
            if (limitCriticalProbability < value)
            {
                criticalProbability = limitCriticalProbability;
            }
            onChangeBattleState?.Invoke(Info_Name.CP);
        }
    }

    /// <summary>
    /// �ִ� ũ��Ƽ�� ������
    /// </summary>
    [SerializeField]
    uint limitCriticalDamage = 500;

    /// <summary>
    /// ũ��Ƽ�� ������
    /// </summary>
    [SerializeField]
    uint criticalDamage;
    public uint CriticalDamage
    {
        get => criticalDamage;
        set
        {
            criticalDamage = value;
            if (limitCriticalDamage < value)
            {
                criticalDamage = limitCriticalDamage;
            }
            onChangeBattleState?.Invoke(Info_Name.CD);
        }
    }

    /// <summary>
    /// ���� �ɷ�ġ �ִ밪
    /// </summary>
    [SerializeField]
    int limitRegistance = 100;

    /// <summary>
    /// ���� �ɷ�ġ 
    /// </summary>
    [SerializeField]
    int registance;
    public int Registance
    {
        get => registance;
        set
        {
            registance = value;
            if (limitRegistance < value)
            {
                registance = limitRegistance;
            }
            else if (value < 1)
            {
                registance = 1;
            }
            onUnitStateChange?.Invoke(UnitStateChangeType.Registance); //���� �ɷ�ġ �������˸�
        }
    }





    /// <summary>
    /// ��Ʋ ������ ������ٰ� �˸��� ��������Ʈ
    /// </summary>
    public Action<Info_Name> onChangeBattleState;

    /// <summary>
    /// ������ �⺻ �ɷ�ġ�� ������ٰ� �˸��� ��������Ʈ
    /// </summary>
    public Action<UnitStateChangeType> onUnitStateChange;

    /// <summary>
    /// ����ϰ��ִ� ���Ŭ���� 
    /// </summary>
    UnitEquipBase UnitEquipBase { get; set; }

    public Action onDie { get; set; }
    /// <summary>
    /// ĳ���� ������ ó���� �Լ�
    /// </summary>
    protected virtual void OnDie() 
    {
        onDie?.Invoke();    
    }

    /// <summary>
    /// ��� Ŭ�������� �����͸� �������� �Լ�
    /// </summary>
    /// <param name="equipData">��� Ŭ����</param>
    /// <returns>��� ���� �߰��� �ɷ�ġ</returns>
    protected virtual int UnitEquipData(UnitEquipBase equipData, EquipStateType stateType) 
    {
        return int.MinValue; //�������̵� �ؼ� ����ؾ��Ѵ�. 
    }

    /// <summary>
    /// ĳ���� �̸� ���� �Լ�
    /// </summary>
    /// <param name="newName">����� �̸�</param>
    protected virtual void UnitNameChange(string newName) 
    {
        
    }

    public void InitDataSetting()
    {
    }

    public void ResetData()
    {
        
    }
}
