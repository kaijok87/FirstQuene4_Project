using System;
using UnityEngine;

/// <summary>
/// �̵� ȸ�� ���� ��Ʈ�� ���� ��������� �������̽�
/// </summary>
public interface IControllObject
{
    /// <summary>
    /// ����Ƽ ������Ʈ�� ����ؼ� ��Ʈ���ϱ����� �⺻������ ���� Ʈ������
    /// </summary>
    Transform transform { get; }

    /// <summary>
    /// ���� ��ü �ɹ��迭�� �ε�����
    /// </summary>
    int ArrayIndex { get; }

    /// <summary>
    /// �ɹ��� �������� üũ
    /// </summary>
    bool IsLeader { get; set; }

    /// <summary>
    /// �ش籺ü �ɹ��� ��ϵ� ����
    /// </summary>
    IUnitDataBase UnitData { get; }

    /// <summary>
    /// �׾����� ������ ��������Ʈ
    /// </summary>
    Action<IControllObject> onDie { get; set; }

    /// <summary>
    /// �ɹ��� �ʱ���ġ�� �ڽ��� ��ġ�� �����ϴ� �Լ�
    /// </summary>
    Vector3 SetFlockingDirectionPos();

    /// <summary>
    /// ĳ���� �����̵��Լ�
    /// </summary>
    /// <param name="endPos">���� ��ġ��</param>
    public void OnAssemble(Vector3 endPos);

    /// <summary>
    /// ������ �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="parnetNode">�θ� ��ü</param>
    /// <param name="flockingPos">��ü������ �ڽ��� ��ġ��</param>
    /// <param name="index">�ڽ��� �ε�����</param>
    void InitDataSetting(BattleMapTeamManager parnetNode, PoolObj_Unit unitObject, Vector3 flockingPos , int index);

    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    void ResetData();
}
