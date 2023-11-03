
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ������ ���� �������� ���� 
/// </summary>
public interface ITeamUnit
{
    /// <summary>
    /// ���� ���� ���� 
    /// </summary>
    TeamType Type { get; }

    /// <summary>
    /// ���� �迭ũ�Ⱚ
    /// </summary>
    public int UnitMaxCapacity { get; }

    /// <summary>
    /// ���� ���� 
    /// </summary>
    public IUnitDefaultBase LeaderUnit { get; }

    /// <summary>
    /// ���� �������ִ� ���� �迭
    /// </summary>
    IUnitDefaultBase[] Units { get; }

    /// <summary>
    /// ����ִ� ���� ����Ʈ �ϰ� ó����
    /// </summary>
    List<IUnitDefaultBase> LivingMemberList { get; }

    /// <summary>
    /// ���� �̵�����
    /// </summary>
    TeamMoveController MoveController { get; }

    /// <summary>
    /// ���� �߰��� �Լ�
    /// </summary>
    /// <param name="unit">�߰��� ����</param>
    /// <returns>�߰� ������ true ���н� false</returns>
    bool AppendUnit(IUnitDefaultBase unit);

    /// <summary>
    /// ���� ���ſ� �Լ�
    /// </summary>
    /// <param name="unit">������ ����</param>
    void RemoveUnit(IUnitDefaultBase unit);

    /// <summary>
    /// ������ �� ������ �ʱ�ȭ ��Ű�� �Լ� 
    /// </summary>
    /// <param name="type">�� Ÿ��</param>
    /// <param name="units">�� ���� ����Ʈ</param>
    /// <param name="initPos">�� �ʱ���ġ ��ǥ</param>
    void InitDataSetting(TeamType type, IUnitDefaultBase[] units, in Vector3 initPos);

    /// <summary>
    /// Ǯ�� ���������� �����Լ�
    /// </summary>
    void ResetData();
}
