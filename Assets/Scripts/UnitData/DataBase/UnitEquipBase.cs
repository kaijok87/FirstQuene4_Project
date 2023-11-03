using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비분류
/// </summary>
public enum EquipType 
{
    None= -1,
    Weapone,        // 무기         
    Sheild,         // 방패
    Armor,          // 갑옷류
    Helm,           // 투구류
    Glove,          // 장갑류
    Boot,           // 신발류
    Accessories     // 악세사리류
}

/// <summary>
/// 장비 분류 능력치별 
/// </summary>
public enum EquipStateType 
{
    None = -1,
    PhysicalPower,      //물리 공격 타입
    PhysicalHit,        //물리 명중 타입
    PhysicalDef,        //물리 방어 타입
    PhysicalEvasion,    //물리 회피 타입
    RangedDef,          //원거리 방어 타입
    RangedEvasion,      //원거리 회피 타입
    MagicPower,         //마법 파워 타입
    MagicDef,           //마법 방어 타입
}
/// <summary>
/// 소모품 타입
/// </summary>
public enum ConsumeType 
{
    None = -1,          //소모안되는 아이템
    Equip,              //장비되는 아이템
    Regain,             //회복 아이템
    Nerf,               //너프 아이템
    Buff,               //버프 아이템
    Flag,               //이벤트 플래그 아이템
}

/// <summary>
/// 장비 등급
/// </summary>
public enum EquipGrade 
{

}

[Serializable]
public struct EquipItem
{
    /// <summary>
    /// 아이템의 고유번호 
    /// 사용처 : 아이템 비교연산에 사용  
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
    /// 같은지 다른지 비교연산 줄이기위한 연산자 오버로딩 
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
/// 장비변경시 데이터 수정하기위한 델리게이트 
/// </summary>
/// <param name="prevEquip">이전 장비정보 </param>
/// <param name="newEquip">변경될 장비정보</param>
public delegate void OnChaneState(in EquipItem prevEquip ,in EquipItem newEquip);

/// <summary>
/// 유닛이 착용하고 있는 아이템 정의 
/// </summary>
public struct UnitEquipBase
{
    /// <summary>
    /// 왼손 장비 
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
    /// 오른손 장비
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
    /// 머리
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
    /// 갑옷 윗옷
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
    /// 바지, 다리
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
    /// 신발
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
    /// 장갑
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
    /// 왼쪽 반지
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
    /// 오른쪽반지 
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
    /// 목걸이 
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
    /// 장비가 변경될시 변경된 장비의 수치로 데이터 변경시키기위한 델리게이트 
    /// </summary>
    OnChaneState onChaneState;
    public OnChaneState OnChaneState 
    {
        get => onChaneState; 
        set => onChaneState = value; 
    }

    /// <summary>
    /// 들어온 데미지 값으로 장비별로 감산해서 반환해주는 함수 
    /// </summary>
    /// <param name="type">공격 방법 </param>
    /// <param name="value">데미지 수치</param>
    /// <returns>감산된 데미지 수치 </returns>
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
    /// 공격 종류별 장비 에따른 데미지 가산 할값을 반환하는 함수 
    /// </summary>
    /// <param name="type">공격 방법 </param>
    /// <param name="value">능력치 에따른 데미지연산값</param>
    /// <returns>장비에따른 데미지 추가값</returns>
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
