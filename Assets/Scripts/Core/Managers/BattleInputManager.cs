using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ŭ�� �ؼ� �̵���ų�� ������ �̵��ϴ� Ÿ��
/// </summary>
public enum UnitMoveControlType 
{
    None = 0,   // �۵�����
    OnlyOne,    // ���� �ϳ��� �̵�
    All,        // ���� ��ü �̵�
}

/// <summary>
/// �׼��� ���ö� ó���� �Լ� ���ǿ�
/// ó���� ���� 
/// 1. ĳ���� or �� ��Ŀ�� �ִ� ��� 
/// 2. ���콺�� ���� Ŭ���� ������ �������� �̵� ����� �ѱ�� 
/// 3. ���콺�� �� Ŭ���� ������ �������� �̵������ �ѱ�� 
/// </summary>
public class BattleInputManager : NomalSingleton<BattleInputManager>
{
    /// <summary>
    /// �̵� ó����� 
    /// </summary>
    UnitMoveControlType moveType = UnitMoveControlType.None;
    public UnitMoveControlType MoveType => moveType;

    /// <summary>
    /// ���� �������� �� 
    /// </summary>
    TeamMoveController currentTeamControl;
    public TeamMoveController CurrentTeamControl => currentTeamControl;

    /// <summary>
    /// ���� ���� �� ��ȣ
    /// </summary>
    [SerializeField]
    int teamIndex = 0;
    int TeamIndex => teamIndex;

    /// <summary>
    /// �������� ������ ��ȣ
    /// </summary>
    [SerializeField]
    int unitIndex = 0;
    public int UnitIndex => unitIndex;

    /// <summary>
    /// ��Ʈ�� �ϴ� ���� ����ɽ� ����
    /// </summary>
    /// <param name="index">����� ���� �ε���</param>
    public void OnChangedTeam(int index)
    {
        teamIndex = index;
        moveType = UnitMoveControlType.All;
        currentTeamControl = BattleUnitGenerateManager.Instance.PlayerTeam[teamIndex].MoveController;
        ChangeFocusUnit(teamIndex);
    }

    /// <summary>
    /// ��Ʈ�� �ϴ� ������ ����ɽ� ����
    /// </summary>
    /// <param name="index">����� ��ġ</param>
    public void OnChangeUnit(int index)
    {
        unitIndex = index;
        moveType = UnitMoveControlType.OnlyOne;
        IUnitDefaultBase[] tempArray = BattleUnitGenerateManager.Instance.PlayerTeam[teamIndex].Units;
        ChangeFocusUnit(index);
    }

    /// <summary>
    /// ��Ŀ�� �̵���Ű�� �Լ� 
    /// </summary>
    /// <param name="index">�̵��� �ε���</param>
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
