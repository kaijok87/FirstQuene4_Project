using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class FlockingMemberNodeData : PoolObjectBase , IControllObject
{
    [SerializeField]
    /// <summary>
    /// ���� �ɹ��� �������� üũ�� ����
    /// </summary>
    bool isLeader = false;

    /// <summary>
    /// ��ü�� ������ �޴���
    /// </summary>
    FlockingManager parentNode;
    
    /// <summary>
    /// ������ Ǯ�� ���������� ������Ʈ 
    /// </summary>
    PoolObj_Unit unitObject;

    /// <summary>
    /// ĳ������ �̵��� ������ ������Ʈ
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;

    /// <summary>
    /// ��ü�� ���������� �󸶳����������������� ��ǥ��
    /// ���������� �ش� �ɹ��� ��ġ�� ������� ���갪
    /// </summary>
    Vector3 flockingDirectionPos;

    /// <summary>
    /// ��ü�� �ε��� ��ȣ
    /// </summary>
    int flockingIndex = -1;

    /// <summary>
    /// ��ü�� �ε����� ���� 
    /// </summary>
    public int ArrayIndex => flockingIndex;

    /// <summary>
    /// ������ ������ �� ������ �������̽�
    /// </summary>
    IUnitDataBase unitData;
    public IUnitDataBase UnitData => unitData;

    /// <summary>
    /// ���� ���� �����ϴ� ������Ƽ
    /// </summary>
    public bool IsLeader 
    {
        get => isLeader; 
        set => isLeader = value; 
    }


    /// <summary>
    /// ������ �׾����� ��ȣ���� ��������Ʈ
    /// </summary>
    public Action<IControllObject> onDie { get ; set ; }


    /// <summary>
    /// �ش� ��ü �ɹ������͹� ���������͸� �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="parnetNode">��ü�� �߽ɺ�</param>
    /// <param name="unitObject">�ɹ������ ���ֿ�����Ʈ</param>
    /// <param name="index">��ü������ �ε�����</param>
    public void InitDataSetting(FlockingManager parnetNode, PoolObj_Unit unitObject, int index) 
    {
        this.parentNode = parnetNode;
        this.flockingIndex = index;
        this.unitObject = unitObject;

        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting();
        
        unitData = GetComponentInChildren<IUnitDataBase>();
        unitData.InitDataSetting();
        unitData.onDie = OnDie;
    }

    /// <summary>
    /// ĳ���� �̵������� �۾��� �����Ѵ�.
    /// </summary>
    /// <param name="endPos">��������</param>
    /// <param name="radius">Ÿ�����ִ°�� Ÿ���� ��������</param>
    private void CharcterMove(Vector3 direction, float distance , float radius = 0.0f)
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(radius);
        charcterMoveProcess.OnMove(direction, distance);
    }

   

    /// <summary>
    /// ��ü�� ���ս�ȣ�� �޾Ƽ� ������ġ�� �̵���Ű�� ����
    /// </summary>
    private void OnAssemble() 
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(0.0f);
        Vector3 dir = flockingDirectionPos - transform.position;
        charcterMoveProcess.OnMove(dir.normalized, dir.sqrMagnitude);
    }

    /// <summary>
    /// ��ü�̵��� ������Ҷ� ����� �Լ�
    /// </summary>
    private void OnFlockingMovingStop() 
    {
        charcterMoveProcess.OnMovingStop();
    }

    /// <summary>
    /// ������ �׾����� ������ �Լ�
    /// </summary>
    private void OnDie() 
    {
        onDie?.Invoke(this);    //�׾��ٰ� ��ȣ������ 
        DataReset();
    }

    /// <summary>
    /// �ʱ�ȭ�� ���� ������
    /// </summary>
    private void DataReset() 
    {
        unitObject.ResetData();     // ���� Ǯ�� ������ 
        unitObject = null;          // �� �ʱ�ȭ 

        charcterMoveProcess = null; //
        unitData.onDie = null;
        unitData = null;

        isLeader = false;
        parentNode = null;
        flockingDirectionPos = Vector3.zero;
        flockingIndex = -1;
    }

    /// <summary>
    /// �ʱ� �����Ͱ����� ���������� �Լ�
    /// </summary>
    public override void ResetData()
    {
        if (unitObject != null) //������ �ִ� �͵��� 
        {
            DataReset();
        }
        base.ResetData();            //�ɹ��� Ǯ�ε�����.
    }
}
