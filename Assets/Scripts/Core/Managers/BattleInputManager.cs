using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 클릭 해서 이동시킬때 유닛을 이동하는 타입
/// </summary>
public enum UnitMoveControlType 
{
    None = 0,   // 작동안함
    OnlyOne,    // 유닛 하나만 이동
    All,        // 유닛 전체 이동
}

/// <summary>
/// 액션이 들어올때 처리할 함수 정의용
/// 처리할 내용 
/// 1. 캐릭터 or 팀 포커스 주는 기능 
/// 2. 마우스로 유닛 클릭시 선택한 기준으로 이동 제어권 넘기기 
/// 3. 마우스로 팀 클릭시 선택한 기준으로 이동제어권 넘기기 
/// </summary>
public class BattleInputManager : NomalSingleton<BattleInputManager>
{
    /// <summary>
    /// 이동 처리방식 
    /// </summary>
    UnitMoveControlType moveType = UnitMoveControlType.None;
    public UnitMoveControlType MoveType => moveType;

    /// <summary>
    /// 현재 조작중인 팀 
    /// </summary>
    TeamMoveController currentTeamControl;
    public TeamMoveController CurrentTeamControl => currentTeamControl;

    /// <summary>
    /// 선택 중인 팀 번호
    /// </summary>
    [SerializeField]
    int teamIndex = 0;
    int TeamIndex => teamIndex;

    /// <summary>
    /// 선택중인 유닛의 번호
    /// </summary>
    [SerializeField]
    int unitIndex = 0;
    public int UnitIndex => unitIndex;

    /// <summary>
    /// 컨트롤 하는 팀이 변경될시 실행
    /// </summary>
    /// <param name="index">변경될 팀의 인덱스</param>
    public void OnChangedTeam(int index)
    {
        teamIndex = index;
        moveType = UnitMoveControlType.All;
        currentTeamControl = BattleUnitGenerateManager.Instance.PlayerTeam[teamIndex].MoveController;
        ChangeFocusUnit(teamIndex);
    }

    /// <summary>
    /// 컨트롤 하는 유닛이 변경될시 실행
    /// </summary>
    /// <param name="index">변경될 위치</param>
    public void OnChangeUnit(int index)
    {
        unitIndex = index;
        moveType = UnitMoveControlType.OnlyOne;
        IUnitDefaultBase[] tempArray = BattleUnitGenerateManager.Instance.PlayerTeam[teamIndex].Units;
        ChangeFocusUnit(index);
    }

    /// <summary>
    /// 포커스 이동시키는 함수 
    /// </summary>
    /// <param name="index">이동할 인덱스</param>
    public void ChangeFocusUnit(int index) 
    {
        switch (moveType)
        {
            case UnitMoveControlType.None:
                break;
            case UnitMoveControlType.OnlyOne:
                break;
            case UnitMoveControlType.All:
                break;
            default:
                break;
        }
    }

}
