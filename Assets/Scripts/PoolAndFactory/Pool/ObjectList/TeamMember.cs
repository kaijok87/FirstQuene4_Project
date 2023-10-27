using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

/*
 1. �ɹ� �� �Ŵ����� �� ����(����, �Ʊ�) �� �ɹ��� �����ϱ����� ���۳�Ʈ 
    1 - 1. �ɹ� �� �� �Ŵ������� �߰����Ǿ�� �ϰ� �߰��ɶ� �ʱ�ȭ ������ ����Ǿ��Ѵ�.
    1 - 2. �Ʊ� �ɹ� �� �׾����� ���Ŵ��� ������ �������� ���Ű��Ǿ���ϰ� Dead ���� �����Ϳ� ����Ǿ���Ѵ�.
    1 - 3. ���� �ɹ� �� �׺������� �Ʊ� ���Ŵ��� ������ ������ �߰��Ǿ���ϰ� �Ʊ��ɹ��� ��ȯ�Ǿ���Ѵ�.
    1 - 4. �ɹ��� �̵������� ���� �����ǰ�  �̵����(�����̵�, ��ü�̵�)�� �����ų���ֵ��� �Ǿ��Ѵ�. �ش� ������� ��� �̵��� ó���Ѵ�.
    1 - 5. �ɹ��� ȸ�������� ���� �����ǰ�  ȸ������� �����ų�� �ֵ��� �ؾ��Ѵ� .
    1 - 6. ���Ŵ��� ������Ʈ ������ �ɹ� ������Ʈ ���� ���־���Ѵ�.
    1 - 7. �ɹ� ������Ʈ���� �� ������Ʈ, ������ ������Ʈ ,  ��Ÿ ������Ʈ �� �����Ѵ�.
      1 - 7 - 1. �ɹ� ������Ʈ�� �� ������Ʈ�� ������ ������Ʈ�� �ɹ� ������ �ٲ𶧸��� ��ü �Ǿ�� �Ѵ�. 
      1 - 7 - 2. �ɹ� ������Ʈ�� �� �ΰ��� ������Ʈ�� �����ϰ� �ٸ� ������Ʈ���� �������� ���ɼ��ֵ��� �������Ѵ�.
     
 */
/*
 1- 7 
 
 */

/// <summary>
/// ��ü �ɹ� ������
/// </summary>
public class TeamMember : PoolObjectBase, IControllObject
{
    /// <summary>
    /// ��ü�� �ε��� ��ȣ
    /// </summary>
    [SerializeField]
    int flockingIndex = -1;

    /// <summary>
    /// ��ü�� �ε����� ���� 
    /// </summary>
    public int ArrayIndex => flockingIndex;
    
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
    TeamObject parentNode;

    /// <summary>
    /// ĳ������ �̵��� ������ ������Ʈ
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;
    public IMoveBase CharcterMoveProcess => charcterMoveProcess;


    /// <summary>
    /// ĳ���� ȸ���� ������ ������Ʈ
    /// </summary>
    [SerializeField]
    IRotateBase charcterRotateProcess;
    IRotateBase CharcterRotateProcess => charcterRotateProcess;



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
    /// ������ ������ �� ������ �������̽�
    /// </summary>
    IUnitDefaultBase unitData;
    public IUnitDefaultBase UnitData => unitData;

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
        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting(colliderRadius);

        charcterRotateProcess = GetComponentInChildren<IRotateBase>();
        charcterRotateProcess.InitDataSetting();
        //capCollider = GetComponent<CapsuleCollider>();
        //thisColliders = GetComponentsInChildren<Collider>();
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
    /// <param name="unitObject">������ ������</param>
    /// <param name="index">��ü������ �ε�����</param>
    public void InitDataSetting(TeamObject parentNode, IUnitDefaultBase unit, int index)
    {
        
        flockingIndex = index;
        this.parentNode = parentNode;

        /// ���� ������ ���� 
        unitData = unit;
        unitData.onDie = OnDie;

        /// �ɹ����� ������ �׼� ����
        parentNode.onAssemble += OnAssemble;
        parentNode.onMove += CharcterMove;
        parentNode.onRotate += CharcterRotate;
        parentNode.onStop += OnFlockingMovingStop;

        /// �θ� Ʈ������ ��ġ�� ���߱����� �ڽ� ������Ʈ ��ġ �ʱ�ȭ 
        //unit.transform.position = Vector3.zero;
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
        if (unitData != null)
        {
            unitData.ResetData();     // ���� Ǯ�� ������ 
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
