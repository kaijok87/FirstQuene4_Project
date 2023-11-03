using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// ĳ���� �̵� ���� 
/// ��ǥ�� �ϰ��ִµ� 
/// ���� Ÿ����ǥ�� �̵������������ؾߵȴ�.
/// </summary>
public class UnitMoveController : MonoBehaviour, IMoveBase
{
    /// <summary>
    /// float �� 0 ������ üũ�ϱ����� ����
    /// </summary>
    float minFloatValue = 0.000001f;

    /// <summary>
    /// ĳ������ �ݶ��̴� ������ ũ��
    /// </summary>
    float colliderSize = 0.0f;

    /// <summary>
    /// �浹�� ��ֹ��� �ݶ��̴� ������ ũ��
    /// </summary>
    //float propCollisionSize = 0.0f;

    /// <summary>
    /// ���� �̵���ġ���� ������ �Ÿ��� 
    /// Ÿ����������� Ÿ�� ������ + ĳ���͹����� �� ���°�� 0���μ���
    /// </summary>
    float moveDistanceSubtractiveOperation;

    /// <summary>
    /// �����ߴ��� üũ�� ������
    /// </summary>
    float checkingInterval = 0.2f;

    /// <summary>
    /// �ش� ĳ������ �̵��� �ڷ�ƾ ������Ƶ� �ݺ��� 
    /// </summary>
    IEnumerator moveCoroutine;

    IUnitDefaultBase unitData;

    protected virtual void Awake()
    {
        moveCoroutine = RigidBodyCharcterMove(transform.position, 0.0f);
        charcterRigidbody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// �ʱⰪ �����ϴ� �Լ�
    /// <param name="colliderRadius">���ڽ��� �ݶ��̴� ������ũ��</param>
    /// </summary>
    public void InitDataSetting(float colliderRadius) 
    {
        
        colliderSize = colliderRadius;
    }



    /// <summary>
    /// ��ġ�� �ʰ� ���� �����ϱ�
    /// Ÿ���� ���������� �����ͼ� �̵��Ÿ�üũ������ ����
    /// ���̸� Ÿ���̾������� �ش���ġ�� �̵��Ҽ��ְ� ������
    /// </summary>
    /// <param name="targetRadiusValue">Ÿ���� �ݶ��̴� ������ũ��</param>
    public void SetMoveDistanceSubtractiveOperation(float targetRadiusValue = 0.0f) 
    {
        if (targetRadiusValue > minFloatValue)  //���� �ִ��� üũ
        {
            moveDistanceSubtractiveOperation = colliderSize + targetRadiusValue;    //������
        }
        else   
        {
            moveDistanceSubtractiveOperation = targetRadiusValue; //�󰪼���  
        }
        
    }

    /// <summary>
    /// �ش���ġ�� �����̵���Ű�� �Լ�
    /// </summary>
    /// <param name="endPos">�̵��� ��ġ</param>
    public void Teleportation(Vector3 endPos) 
    {
        transform.position = endPos;
    }

    /// <summary>
    /// �̵��� �ڷ�ƾ ���� 
    /// </summary>
    /// <param name="direction">�̵��� ����</param>
    /// <param name="distance">�̵��� �Ÿ�</param>
    public void OnMove(Vector3 direction, float distance) 
    {
        OnRigidMove(direction,distance);
        //return;
        //StopCoroutine(moveCoroutine);
        //moveCoroutine = CharcterMoveCoroutine(direction, distance);
        //StartCoroutine(moveCoroutine);
    }

    /// <summary>
    /// ĳ���� �̵��� ���ߴ� �Լ�
    /// </summary>
    public void OnMovingStop()
    {
        StopCoroutine(moveCoroutine);
    }

    /// <summary>
    /// ĳ���� �̵��ӵ� �� ����Ͽ� 
    /// �̵��� ����� �Ÿ���ŭ �̵���Ű�� �ڷ�ƾ
    /// </summary>
    /// <param name="direction">�̵��� ����</param>
    /// <param name="distance">�̵��� �Ÿ�</param>
    /// <returns></returns>
    protected IEnumerator CharcterMoveCoroutine(Vector3 direction, float distance)
    {
        Vector3 endPos = transform.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif

        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;                     // �̵��� �Ÿ� ��
        float timeDeltaValue = 0.0f;
        float dirMag = (endPos - transform.position).sqrMagnitude;
        float tempValue = 0.0f;
        while (dirMag > checkValue)
        {
            timeDeltaValue = Time.deltaTime;
            transform.Translate(timeDeltaValue * unitData.GetUnitMoveRange() * direction, Space.World);     // Ư���������� �̵��ӵ���ŭ �̵���Ų��.
            
            tempValue = (endPos - transform.position).sqrMagnitude;    //�����Ȳ�ӽ÷������ϰ� 

            if (dirMag < tempValue) //�̵������� ���������ϴµ�  �Ÿ��� �о����ٴ°��� �������� �����ƴٴ°��̴� 
            {
                direction = (endPos - transform.position).normalized; //������ �ٽ���´�
            }
            dirMag = tempValue;  //üũ�� ������ �����Ѵ�.
            transform.localPosition = Vector3.zero;
            yield return null;                                                                      // �����Ӹ���.
        }

        if (moveDistanceSubtractiveOperation < minFloatValue)                                       // Ÿ���� ���°�� 
        {
            transform.position = endPos;                                                            // ��Ȯ�� ��ġ�� �̵��� ��Ų��
        }
    }



    ///====================================== ������ �ٵ� �׽�Ʈ �ڵ�======================================
     
    /// <summary>
    /// ĳ������ ��������ó���� ������ٵ�
    /// </summary>
    Rigidbody charcterRigidbody;

    WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();

    /*
        Root Object �� �̵���Ű�� 
        Child Object �� RigidBody �� �־���� �����̸� 
        ������ٵ��� MovePosition �� ����Ҽ�������.
        �θ�� �̵�ó���ϴµ� �ڽĸ� ���� ��� ��찡 �����.
    */

    /// <summary>
    /// �̵��� ����� �Ÿ���ŭ �̵���Ű�� �Լ�
    /// ������ �ٵ� �̿��Ѵ�.
    /// </summary>
    /// <param name="direction">�̵��� ����</param>
    /// <param name="distance">�̵��� �Ÿ�</param>
    public virtual void OnRigidMove(Vector3 direction, float distance)
    {
        //isRigid = true;
        //direction_Value = direction ;
        //endPos = transform.position + (direction * distance);
        //tempValue = (endPos - transform.position).sqrMagnitude;
        //a = tempValue;
        StopCoroutine(moveCoroutine);
        moveCoroutine = RigidBodyCharcterMove(direction, distance);
        StartCoroutine(moveCoroutine);
    }

    //bool isRigid = false;
    //Vector3 direction_Value;
    //Vector3 endPos;
    //float tempValue;
    //float a;
    //private void FixedUpdate()
    //{
    //    if (isRigid) 
    //    {
    //        charcterRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * charcterMoveSpeed * direction_Value);
    //        tempValue = (endPos - transform.position).sqrMagnitude;
    //        if (tempValue < a) 
    //        {
    //            direction_Value = (endPos - transform.position).normalized;
    //        }
    //        a = tempValue;
    //    }
    //}

    /// <summary>
    /// ĳ���� �̵��ӵ� �� ����Ͽ� 
    /// �̵��� ����� �Ÿ���ŭ �̵���Ű�� �ڷ�ƾ
    /// ������ �ٵ� �� �̿��Ѵ�.
    /// ���� ���� ��� �ȵǳ�?? �ڿ��� �и� ��������..
    /// </summary>
    /// <param name="direction">�̵��� ����</param>
    /// <param name="distance">�̵��� �Ÿ�</param>
    protected virtual IEnumerator RigidBodyCharcterMove(Vector3 direction, float distance)
    {
        Vector3 endPos = transform.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif
        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;
        //Debug.Log($"start :{(endPos-transform.position).sqrMagnitude} > {checkValue}");
        float sqlMagnitudeValue = (endPos - transform.position).sqrMagnitude;  // �����ߴ��������� ���� �����Ȳ �����Һ���
        float tempValue = 0.0f;     //������ �ٽ���ƾ��Ҷ� ����� ����
        while (sqlMagnitudeValue  > checkValue)
        {
            //charcterRigidbody.MovePosition(transform.position + tempValue * propCollisionSize * direction);      // �浹�� ��ֹ� ������ ��ŭ �и����� �����.
            charcterRigidbody.MovePosition(transform.position + Time.fixedDeltaTime * unitData.GetUnitMoveSpeed() * direction);
            yield return fixedWait;
            Debug.Log(charcterRigidbody.velocity);
            tempValue = (endPos - transform.position).sqrMagnitude;    //�����Ȳ�ӽ÷������ϰ� 
            if (sqlMagnitudeValue  < tempValue) //�̵������� ���������ϴµ�  �Ÿ��� �о����ٴ°��� �������� �����ƴٴ°��̴� 
            {
                direction = (endPos - transform.position).normalized; //������ �ٽ���´�
            }
            sqlMagnitudeValue = tempValue;  //üũ�� ������ �����Ѵ�.
            transform.localPosition = Vector3.zero;
        }
        //Debug.Log($"end :{(endPos - transform.position).sqrMagnitude} > {checkValue}");
        charcterRigidbody.MovePosition(endPos);
    }




  

#if UNITY_EDITOR

    [SerializeField]
    Color gizmosColor = Color.black;

    [SerializeField]
    float moveRange = 5.0f;
    [SerializeField]
    float attackRange = 8.0f;
    Vector3 gizmosEndPos = Vector3.zero;


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;



        if (transform != null)
        {
            Gizmos.DrawLine(transform.position, gizmosEndPos);
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, transform.up, moveRange);

            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, attackRange);
        }
        else 
        {
            Gizmos.color= Color.clear;
        }

    }




#endif



}
