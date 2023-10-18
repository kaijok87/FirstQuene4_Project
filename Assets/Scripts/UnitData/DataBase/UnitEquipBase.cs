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
/// 유닛이 착용하고 있는 아이템 정의 
/// </summary>
public class UnitEquipBase : MonoBehaviour
{
    
}
