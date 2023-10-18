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
    /// ����ȣ��� ������ �Լ�
    /// </summary>
    void OnAssemble(){ }

    /// <summary>
    /// ���� �̵�ȣ��� ������ �Լ�
    /// </summary>
    void CharcterMove(Vector3 direction, float distance, float radius = 0.0f) { }

    /// <summary>
    /// ���� �̵����� ���߶�� ȣ��� ������ �Լ�
    /// </summary>
    void OnFlockingMovingStop() { }

    /// <summary>
    /// ������ �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="parnetNode">�θ� ��ü</param>
    /// <param name="index">�ڽ��� �ε�����</param>
    void InitDataSetting(FlockingManager parnetNode, PoolObj_Unit unitObject, int index);

    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    void ResetData();
}
