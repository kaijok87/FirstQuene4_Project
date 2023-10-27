using System;
/// <summary>
/// 맴버 번호를 관리하고  
/// 유닛 모델을 관리해야하고
/// 유닛 을 관리해야하고 
/// 이동 및 회전 을 관리해야하고 
/// 공격및 이동 검색을 관리해야한다.
/// 죽었을때 기능을 관리해야하고
/// 
/// </summary>
public interface IMemberBase 
{
    int MemberIndex { get; }
    UnitData UnitData { get; }

    IMoveBase CharcterMoveProcess { get; }

    IRotateBase CharcterRotateProcess { get; }

    Action OnUnitDie { get; set; }

    void InitDataSetting(int memberIndex, UnitData unitData);
    

}
