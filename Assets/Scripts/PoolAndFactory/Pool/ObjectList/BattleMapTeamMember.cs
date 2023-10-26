using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ü �ɹ� ������
/// </summary>
public class BattleMapTeamMember : PoolObjectBase, IControllObject
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
    UnitDataBaseNode unitObject;
    public UnitDataBaseNode UnitObject => unitObject;

    /// <summary>
    /// ĳ������ �̵��� ������ ������Ʈ
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;
    public IMoveBase CharcterMoveProcess => charcterMoveProcess;
  
    /// <summary>
    /// ĳ���� ȸ���� ������ ������Ʈ
    /// </summary>
    CharcterRotateBase charcterRotateProcess;



    /// <summary>
    /// ��ü�� ���������� �󸶳����������������� ��ǥ��
    /// ���������� �ش� �ɹ��� ��ġ�� ������� ���갪
    /// </summary>
    Vector3 flockingDirectionPos;
    public Vector3 FlockingDirectionPos
    {
        get => flockingDirectionPos;
        set => flockingDirectionPos = value;
    }
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
    IUnitStateTable unitData;
    public IUnitStateTable UnitData => unitData;

    /// <summary>
    /// ������ �׾����� ��ȣ���� ��������Ʈ
    /// </summary>
    public Action<IControllObject> onDie { get; set; }

    /// <summary>
    /// �ɹ������� �⺻ �ݶ��̴� 
    /// </summary>
    CapsuleCollider capCollider;
    float colliderRadius = 0.0f;

    CharacterController cc;

    Collider[] thisColliders;

    private void Awake()
    {
        capCollider = GetComponent<CapsuleCollider>();
        thisColliders = GetComponentsInChildren<Collider>();
        //stateList = new(10000);
        //cc = GetComponent<CharacterController>();
        //if (cc != null)
        //{
        //    colliderRadius = cc.radius;
        //}
        //else if () {
        //{
        //    colliderRadius = capCollider.radius;
        //}
    }

    /// <summary>
    /// �ش� ��ü �ɹ������͹� ���������͸� �ʱ�ȭ�� �Լ�
    /// </summary>
    /// <param name="parentNode">��ü�� �߽ɺ�</param>
    /// <param name="unitObject">�ɹ������ ���ֿ�����Ʈ</param>
    /// <param name="flockingPos">��ü���� �ڽ��� ��� ��ġ��</param>
    /// <param name="index">��ü������ �ε�����</param>
    public void InitDataSetting(BattleMapTeamManager parentNode, UnitDataBaseNode unitObject, int index)
    {
        
        flockingIndex = index;
        this.parentNode = parentNode;
        this.unitObject = unitObject;

        charcterRotateProcess = GetComponentInChildren<CharcterRotateBase>();
        charcterRotateProcess.InitDataSetting();

        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting(colliderRadius);

        unitData = GetComponentInChildren<IUnitStateTable>();
        unitData.InitDataSetting();
        unitData.onDie = OnDie;

        parentNode.onAssemble += OnAssemble;
        parentNode.onMove += CharcterMove;
        parentNode.onRotate += CharcterRotate;
        parentNode.onStop += OnFlockingMovingStop;

        unitObject.transform.position = Vector3.zero;
    }
  
    /// <summary>
    /// ĳ���� �̵������� �۾��� �����Ѵ�.
    /// </summary>
    /// <param name="endPos">��������</param>
    /// <param name="radius">Ÿ�����ִ°�� Ÿ���� ��������</param>
    private void CharcterMove(Vector3 direction, float distance, float radius = 0.0f)
    {
        charcterMoveProcess.SetMoveDistanceSubtractiveOperation(radius);
        charcterMoveProcess.OnMove(direction, distance);
    }


    /// <summary>
    /// ĳ���� ȸ�� ��Ű��
    /// </summary>
    /// <param name="endPos">�������� </param>
    private void CharcterRotate(Vector3 endPos) 
    {
        charcterRotateProcess.OnRotateRealTime(endPos-transform.position);
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
    /// ���ó ���� �׾������� ���� �ε�� Ÿ��Ʋ �̵������� ��ü������ �ʱ�ȭ�� ��� 
    /// </summary>
    public override void ResetData()
    {
        if (unitObject != null)
        {
            unitObject.ResetData();     // ���� Ǯ�� ������ 
            UnitDataReset();            // ������ ���� 
        }
        MemberDataReset();
        //OffCollider();
    }

    /// <summary>
    /// �ɹ� ���� ������ �����Լ�
    /// </summary>
    private void MemberDataReset()
    {
        isLeader = false;
        flockingDirectionPos = Vector3.zero;
        flockingIndex = -1;
        transform.position = Vector3.zero;
    }

    /// <summary>
    /// �ɹ��� ����� ���ֿ����� ������ �����ϴ� �Լ�
    /// </summary>
    private void UnitDataReset() 
    {
        unitObject = null;          // �� �ʱ�ȭ 
        unitData.onDie = null;
        unitData = null;
        parentNode.onAssemble -= OnAssemble;
        parentNode.onMove -= CharcterMove;
        parentNode.onStop -= OnFlockingMovingStop;
        parentNode.onRotate -= CharcterRotate;
        parentNode = null;
        charcterMoveProcess.OnMovingStop();
        charcterMoveProcess = null;
    } 
    

    /// <summary>
    /// �׺��� ������ ������ �����Լ�
    /// </summary>
    public void SurrenderDataReset()
    {
        UnitDataReset();
        MemberDataReset();
    }

    /// <summary>
    /// �浹������ġ
    /// </summary>
    [SerializeField]
    Vector3 collisionStartPos;
  
    enum CollisionState 
    {
        None = 0,
        Te,
        Ts,
        Tx,
        Ce,
        Cs,
        Cx
    }
    List<CollisionState> stateList;
    [SerializeField]
    CollisionState[] collisionStateArray;
    public void SeTList() 
    {
        collisionStateArray = stateList.ToArray();
    }
    public void ResetList()
    {
        stateList.Clear();
        collisionStateArray = null;
    }
  
    /*
     
     */

    /// <summary>
    /// ���� �̺�Ʈ�Լ��� �̿��ؼ� �������� �ͳθ� ������ ��������� �������� ������ 
    /// 1. ó���������� �浹��ġ�� �ȵǰ�
    /// 2. �浹 ���߿� ���ʿ���  �ٸ�������Ʈ�� �浹�Ǽ� �з��� �ȵȴ�.
    /// </summary>
    [SerializeField]
    float testValue = 0.05f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            collisionStartPos = transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            if (collisionStartPos != transform.position)
            {
                Vector3 tempPos = (collision.transform.position - transform.position).normalized;   // ��������� �븻�� ���ϰ�
                tempPos *= -testValue;                                                  // �븻���� ������ũ�Ⱚ�� ���ؼ� �ݶ��̴��� �浹������ġ���������ϰ�
                                                                                        //tempPos =  other.transform.position - transform.position + tempPos;             // �ش繰ü�� �浹�� ������ �����صд� 
                tempPos.y = 0.0f;
                transform.position = collisionStartPos + tempPos;
            }
            collisionStartPos = transform.position;
        }
    }

}
