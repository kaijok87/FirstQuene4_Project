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
/// 유닛의 종류
/// </summary>
public enum UnitGrade 
{
    None = 0,
    Grunt,          //일반 병사
    Hero ,          //영웅 

}

/// <summary>
/// 유닛의 기본적인 데이터 정의용 인터페이스
/// </summary>
public interface IUnitDefaultBase
{
    /// <summary>
    /// 컴포넌트에 넣을것이기때문에 기본 트랜스폼 값을 연결 
    /// </summary>
    Transform transform { get; }

    /// <summary>
    /// 유닛의 기본 정보를 담아둘 객체 
    /// </summary>
    UnitData UnitData { get; }


    ITeamUnit CurrentTeam { get; set; }

    /// <summary>
    /// 현재 군체 위치값 반환하기 
    /// </summary>
    Vector3 GetFlockingPos { get; }

    /// <summary>
    /// 유닛의 맴버 인덱스 번호 
    /// </summary>
    /// <returns>유닛의 설정된 맴버번호 </returns>
    int GetMemberIndex();

    /// <summary>
    /// 유닛의 이동속도 반환
    /// </summary>
    /// <returns>반환할 이동속도 </returns>
    float GetUnitMoveSpeed();
    /// <summary>
    /// 유닛의 이동범위 반환
    /// </summary>
    /// <returns>반환할 이동범위 </returns>
    float GetUnitMoveRange();

    /// <summary>
    /// 이동로직 연결용 
    /// </summary>
    public UnitMoveController UnitMoveController { get; }

    /// <summary>
    /// 맴버 유닛이 죽을때 연결할 델리게이트
    /// </summary>
    Action<IUnitDefaultBase> onDie { get; set; }

    /// <summary>
    /// 유닛 초기화용 함수 
    /// </summary>
    /// <param name="unitData">초기화될 데이터</param>
    void InitDataSetting(UnitData unitData);


    /// <summary>
    /// 데이터를 초기상태로 돌리는 함수 
    /// </summary>
    void ResetData() 
    {
    }
}
