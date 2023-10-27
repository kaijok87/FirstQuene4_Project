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
    IUnitDefaultBase UnitData { get; }

    /// <summary>
    /// ���� �����̴� ���� 
    /// </summary>
    public IMoveBase CharcterMoveProcess { get; }

    /// <summary>
    /// �ɹ���ü�� ������ġ���� �󸶳��������ִ��������� ��ġ��
    /// </summary>
    Vector3 FlockingDirectionPos { get; set; }

    /// <summary>
    /// �׾����� ������ ��������Ʈ
    /// </summary>
    Action<IControllObject> onDie { get; set; }

    /// <summary>
    /// ��ֹ��� �浹 ������ ��ֹ� ���������� �����ϴ� �Լ�
    /// </summary>
    //Action<bool> onCollisionOnOff { get; set; }

    /// <summary>
    /// �ɹ��� �ʱ���ġ�� �ڽ��� ��ġ�� �����ϴ� �Լ�
    /// </summary>
    Vector3 SetFlockingDirectionPos();

    /// <summary>
    /// ĳ���� �����̵��Լ�
    /// </summary>
    /// <param name="endPos">���� ��ġ��</param>
    void OnAssemble(Vector3 endPos);

    /// <summary>
    /// ������ �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="parnetNode">�θ� ��ü</param>
    /// <param name="unitData">���� ���� </param>
    /// <param name="index">�ڽ��� �ε�����</param>
    void InitDataSetting(TeamObject parnetNode, IUnitDefaultBase unitData , int index);

    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    void ResetData();

    /// <summary>
    /// �׺��� ������ ���ÿ� �Լ�
    /// </summary>
    void SurrenderDataReset();
}
