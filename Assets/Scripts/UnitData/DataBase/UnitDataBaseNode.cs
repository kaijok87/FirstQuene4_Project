using System;
using UnityEngine;

/// <summary>
/// 캐릭터 기본 능력치 정의 클래스
/// </summary>
public class UnitDataBaseNode : MonoBehaviour , IUnitDataTable, IUnitDataBase
{
    [Header("캐릭터 기본 값")]
    /// <summary>
    /// 캐릭터 이름
    /// </summary>
    [SerializeField]
    string unitName;
    public string UnitName => unitName;
    
    /// <summary>
    /// 최대 성장 가능한 HP 수치
    /// </summary>
    [SerializeField]
    float limitHealthPoint = 9999;

    /// <summary>
    /// 최대 피
    /// </summary>
    [SerializeField]
    float maxHealthPoint;
    public float MaxHP 
    {
        get => maxHealthPoint;
        set 
        {
            maxHealthPoint = value; //수정하고 
            if (limitHealthPoint < value) // 최대치보다 크면 
            {
                maxHealthPoint = limitHealthPoint; //최대치 만큼만
            }
            else if (value < 0)  //0보다 작으면 
            {
                maxHealthPoint = 1.0f; // 0이면 안되니 최소값인 1만큼
            }
            onChangeBattleState?.Invoke(Info_Name.MXHP); 
        }  
    } 

    /// <summary>
    /// 현재 피
    /// </summary>
    [SerializeField]
    float healthPoint;
    public float HealthPoint
    {
        get => healthPoint;
        set 
        {
            //죽었는지 체크하고 
            if (value < 0) //체력이 0이하면  
            {
                healthPoint = 0; //0으로 맞추고 
                OnDie(); //죽는 로직 실행
                return;
            } 
            //안죽었으면 
            healthPoint = value; 
            if (value > maxHealthPoint) 
            {
                healthPoint = maxHealthPoint;

            } 
            onChangeBattleState?.Invoke(Info_Name.HP); //체력 변화 신호 보내기
        }

    }

    /// <summary>
    /// 최대 누적가능한 피로도 
    /// </summary>
    [SerializeField]
    float limitFatigue = 99;

    /// <summary>
    /// 피로도 
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
    /// 현재 상태 (공격,대기 ,후퇴 등 전투상황 및 상태이상)
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
    /// 레벨 최대치
    /// </summary>
    [SerializeField]
    uint limitLevel = 999;
    /// <summary>
    /// 캐릭터 레벨
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
    /// 힘 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitStrength = 100;

    /// <summary>
    /// 힘 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Strength); //기존 능력치 변경을알림
        }
    }

    /// <summary>
    /// 물리 공격력 최대치
    /// </summary>
    [SerializeField]
    int limitPhysicalAttackPower = 100;

    /// <summary>
    /// 물리 공격력
    /// </summary>
    [SerializeField]
    int physicalAttackPower;

    public int PhysicalAttackPower 
    {
        get => physicalAttackPower;
        set
        {
            physicalAttackPower += value; //추가된 값 적용 (버프 or 디버프)
            physicalAttackPower = strength + //기본 힘에다가  
                UnitEquipData(UnitEquipBase, EquipStateType.PhysicalPower); //현재 장비데이터능력치 추가하고
            if (limitPhysicalAttackPower < physicalAttackPower) //최대치 보다 높으면 
            {
                physicalAttackPower = limitPhysicalAttackPower; //최대치로 고정
            }
            else if(physicalAttackPower < 0) //0보다 낮아지면
            {
                physicalAttackPower = 0; //0으로 셋팅
            }

            onChangeBattleState?.Invoke(Info_Name.AT); //상태 변화 됬음을 알림
        }
    }

    /// <summary>
    /// 손 재주 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitDexterity = 100;

    /// <summary>
    /// 손 재주 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Dexterity); //기존 능력치 변경을알림
        }
    }

   

    /// <summary>
    /// 명중치 최대값
    /// </summary>
    [SerializeField]
    int limitPhysicalHit = 200;

    /// <summary>
    /// 물리 명중
    /// </summary>
    [SerializeField]
    int physicalHit;
    public int PhysicalHit 
    {
        get => physicalHit;
        set 
        {
            physicalHit = value; //추가된 값 적용 (버프 or 디버프)
            physicalHit = UnitEquipData(UnitEquipBase, EquipStateType.PhysicalHit); //장비의 명중률 가져와서 적용
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
    /// 건강 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitConstitution = 100;

    /// <summary>
    /// 건강 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Constitution); //기존 능력치 변경을알림
        }
    }

    /// <summary>
    /// 최대 물리방어력
    /// </summary>
    [SerializeField]
    int limitPhysicalDef = 100;

    /// <summary>
    /// 물리 방어
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
    /// 최대 원거리 방어력
    /// </summary>
    [SerializeField]
    uint limitRangedDef = 100;

    /// <summary>
    /// 원거리 방어
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
    /// 민첩 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitAgility = 100;

    /// <summary>
    /// 민첩 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Agility); //기존 능력치 변경을알림
        }
    }

    /// <summary>
    /// 최대 회피 가능한 수치
    /// </summary>
    [SerializeField]
    uint limitPhysicalEvasion = 200;

    /// <summary>
    /// 회피
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
    /// 최대 원거리 회피
    /// </summary>
    [SerializeField]
    uint limitRangedEvasion = 200;

    /// <summary>
    /// 원거리 회피
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
    /// 지능 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitIntelligence = 100;

    /// <summary>
    /// 지능 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Intelligence); //기존 능력치 변경을알림
        }
    }

    /// <summary>
    /// 최대 마법 공격력
    /// </summary>
    [SerializeField]
    uint limitMagicPower = 100;

    /// <summary>
    /// 마법 공격력
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
    /// 지혜 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitWisdom = 100;

    /// <summary>
    /// 지혜 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Wisdom); //기존 능력치 변경을알림
        }
    }

    /// <summary>
    /// 최대 마법 방어
    /// </summary>
    [SerializeField]
    uint limitMagicDef = 100;

    /// <summary>
    /// 마법 방어
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
    /// 행운 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitLuck = 100;

    /// <summary>
    /// 행운 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Luck); //기존 능력치 변경을알림
        }
    }

    /// <summary>
    /// 최대 크리티컬 확율
    /// </summary>
    [SerializeField]
    uint limitCriticalProbability = 100;

    /// <summary>
    /// 크리티컬 확율
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
    /// 최대 크리티컬 데미지
    /// </summary>
    [SerializeField]
    uint limitCriticalDamage = 500;

    /// <summary>
    /// 크리티컬 데미지
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
    /// 저항 능력치 최대값
    /// </summary>
    [SerializeField]
    int limitRegistance = 100;

    /// <summary>
    /// 저항 능력치 
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
            onUnitStateChange?.Invoke(UnitStateChangeType.Registance); //기존 능력치 변경을알림
        }
    }





    /// <summary>
    /// 배틀 데이터 수정됬다고 알리는 델리게이트
    /// </summary>
    public Action<Info_Name> onChangeBattleState;

    /// <summary>
    /// 유닛의 기본 능력치가 수정됬다고 알리는 델리게이트
    /// </summary>
    public Action<UnitStateChangeType> onUnitStateChange;

    /// <summary>
    /// 장비하고있는 장비클래스 
    /// </summary>
    UnitEquipBase UnitEquipBase { get; set; }

    public Action onDie { get; set; }
    /// <summary>
    /// 캐릭터 죽을때 처리할 함수
    /// </summary>
    protected virtual void OnDie() 
    {
        onDie?.Invoke();    
    }

    /// <summary>
    /// 장비 클래스에서 데이터를 가져오는 함수
    /// </summary>
    /// <param name="equipData">장비 클래스</param>
    /// <returns>장비 에서 추가될 능력치</returns>
    protected virtual int UnitEquipData(UnitEquipBase equipData, EquipStateType stateType) 
    {
        return int.MinValue; //오버라이딩 해서 사용해야한다. 
    }

    /// <summary>
    /// 캐릭터 이름 변경 함수
    /// </summary>
    /// <param name="newName">변경될 이름</param>
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
