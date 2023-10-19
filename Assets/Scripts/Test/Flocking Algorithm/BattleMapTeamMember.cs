using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

/// <summary>
/// ��ü �ɹ� ������
/// </summary>
public class BattleMapTeamMember : PoolObjectBase , IControllObject
{
    [SerializeField]
    /// <summary>
    /// ���� �ɹ��� �������� üũ�� ����
    /// </summary>
    bool isLeader = false;

    /// <summary>
    /// ���� ���� �����ϴ� ������Ƽ
    /// </summary>
    public bool IsLeader
    {
        get => isLeader;
        set 
        {
            if (value) 
            {
                parentNode.onAssemble -= OnAssemble; //�����ΰ�� ȣ�� ������ ����ȵǾ������� �׼ǿ��� ����
                isLeader = value;
            }
        }
    }
    /// <summary>
    /// ��ü�� ������ �޴���
    /// </summary>
    BattleMapTeamManager parentNode;
    
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
    /// ������ �׾����� ��ȣ���� ��������Ʈ
    /// </summary>
    public Action<IControllObject> onDie { get ; set ; }


    /// <summary>
    /// �ش� ��ü �ɹ������͹� ���������͸� �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="parentNode">��ü�� �߽ɺ�</param>
    /// <param name="unitObject">�ɹ������ ���ֿ�����Ʈ</param>
    /// <param name="flockingPos">��ü���� �ڽ��� ��� ��ġ��</param>
    /// <param name="index">��ü������ �ε�����</param>
    public void InitDataSetting(BattleMapTeamManager parentNode, PoolObj_Unit unitObject, Vector3 flockingPos, int index) 
    {
        this.parentNode = parentNode;
        this.flockingIndex = index;
        this.unitObject = unitObject;
        this.flockingDirectionPos = flockingPos;

        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting();
        
        unitData = GetComponentInChildren<IUnitDataBase>();
        unitData.InitDataSetting();
        unitData.onDie = OnDie;

        parentNode.onAssemble += OnAssemble;
        parentNode.onMove += CharcterMove;
        parentNode.onStop += OnFlockingMovingStop;
        
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
    /// <param name="originPos">�̵���ų ������</param>
    /// </summary>
    public void OnAssemble(Vector3 originPos) 
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(0.0f);
        Vector3 dir = originPos + flockingDirectionPos - transform.position;
        charcterMoveProcess.OnMove(dir.normalized, dir.magnitude);
    }

    /// <summary>
    /// ��ü�̵��� ������Ҷ� ����� �Լ�
    /// </summary>
    private void OnFlockingMovingStop() 
    {
        charcterMoveProcess.OnMovingStop();
    }

    /// <summary>
    /// ��ü ���ı������� �ڽ�����ġ�� �����ϴ� �Լ�
    /// </summary>
    public Vector3 SetFlockingDirectionPos()
    {
        return flockingDirectionPos = transform.position;
    }

    /// <summary>
    /// ������ �׾����� ������ �Լ�
    /// </summary>
    private void OnDie() 
    {
        onDie?.Invoke(this);    //�׾��ٰ� ��ȣ������ 
        ResetData();
    }


    /// <summary>
    /// �ʱ� �����Ͱ����� ���������� �Լ�
    /// �׻���ٴҰ��̱⶧���� Ǯ�� ������ �۾��� ���ܽ��״�.
    /// </summary>
    public override void ResetData()
    {
        if (unitObject != null) 
        {
            UnitReset();
        }
        isLeader = false;
        flockingDirectionPos = Vector3.zero;
        flockingIndex = -1;
        transform.position = Vector3.zero;
    }

    /// <summary>
    /// ������ ���� ���ִ°�� ������ �ʱ�ȭ�ϱ����� �Լ�
    /// </summary>
    private void UnitReset()
    {
        unitObject.ResetData();     // ���� Ǯ�� ������ 
        unitObject = null;          // �� �ʱ�ȭ 
        unitData.onDie = null;
        unitData = null;
        parentNode.onAssemble -= OnAssemble;
        parentNode.onMove -= CharcterMove;
        parentNode.onStop -= OnFlockingMovingStop;
        parentNode = null;
        charcterMoveProcess.OnMovingStop();
        charcterMoveProcess = null;
    }

   
}
