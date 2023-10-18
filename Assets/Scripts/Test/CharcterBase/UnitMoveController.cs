using System.Collections;
using UnityEngine;

/// <summary>
/// ĳ���� �̵� ���� 
/// </summary>
public class UnitMoveController : MonoBehaviour, IMoveBase
{
    /// <summary>
    /// �̵����� ������ Ʈ������
    /// </summary>
    Transform moveTarget;

    /// <summary>
    /// float �� 0 ������ üũ�ϱ����� ����
    /// </summary>
    float minFloatValue = 0.000001f;

    /// <summary>
    /// ĳ������ �ݶ��̴� ������ ũ��
    /// </summary>
    float colliderSize = 0.0f;

    /// <summary>
    /// ���� �̵���ġ���� ������ �Ÿ��� 
    /// Ÿ����������� Ÿ�� ������ + ĳ���͹����� �� ���°�� 0���μ���
    /// </summary>
    float moveDistanceSubtractiveOperation;

    /// <summary>
    /// ĳ������ �̵��ӵ� 
    /// </summary>
    [SerializeField]
    [Range(0.01f,20.0f)]
    float charcterMoveSpeed = 1.0f;

    /// <summary>
    /// �����ߴ��� üũ�� ������
    /// </summary>
    float checkingInterval = 0.2f;

    /// <summary>
    /// �ش� ĳ������ �̵��� �ڷ�ƾ ������Ƶ� �ݺ��� 
    /// </summary>
    IEnumerator moveCoroutine;

    protected virtual void Awake()
    {
        moveCoroutine = CharcterMoveCoroutine(transform.position, 0.0f); //StopCoroutine Null���۷��� Expection ������
    }

    /// <summary>
    /// �ʱⰪ �����ϴ� �Լ�
    /// </summary>
    public void InitDataSetting() 
    {
        moveTarget = transform.parent;
        charcterRigidbody = moveTarget.GetComponent<Rigidbody>();
        CapsuleCollider collider = moveTarget.GetComponent<CapsuleCollider>();
        colliderSize = collider.radius;
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
        moveTarget.position = endPos;
    }

    /// <summary>
    /// �̵��� �ڷ�ƾ ���� 
    /// </summary>
    /// <param name="direction">�̵��� ����</param>
    /// <param name="distance">�̵��� �Ÿ�</param>
    public void OnMove(Vector3 direction, float distance) 
    {
        OnRigidMove(direction,distance);
        return;
        StopCoroutine(moveCoroutine);
        moveCoroutine = CharcterMoveCoroutine(direction,distance);
        StartCoroutine(moveCoroutine);
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
        Vector3 endPos = moveTarget.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif

        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;                     // �̵��� �Ÿ� ��
        while ((endPos - moveTarget.position).sqrMagnitude > checkValue)
        {
            moveTarget.Translate(Time.deltaTime * charcterMoveSpeed * direction, Space.World);       // Ư���������� �̵��ӵ���ŭ �̵���Ų��.
            yield return null;                                                                      // �����Ӹ���.
        }

        if (moveDistanceSubtractiveOperation < minFloatValue)                                       // Ÿ���� ���°�� 
        {
            moveTarget.position = endPos;                                                            // ��Ȯ�� ��ġ�� �̵��� ��Ų��
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
        StopCoroutine(moveCoroutine);
        moveCoroutine = RigidBodyCharcterMove(direction, distance);
        StartCoroutine(moveCoroutine);
    }


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
        Vector3 endPos = moveTarget.position + (direction * distance);
#if UNITY_EDITOR
        gizmosEndPos = endPos;
#endif
        float checkValue = checkingInterval + moveDistanceSubtractiveOperation;
        //Debug.Log($"start :{(endPos-moveTarget.position).sqrMagnitude} > {checkValue}");
        float sqlMagnitudeValue = (endPos - moveTarget.position).sqrMagnitude;  // �����ߴ��������� ���� �����Ȳ �����Һ���
        float tempValue = 0.0f;     //������ �ٽ���ƾ��Ҷ� ����� ����
        while (sqlMagnitudeValue  > checkValue)
        {
            charcterRigidbody.MovePosition(moveTarget.position + Time.deltaTime * charcterMoveSpeed * direction);
            yield return fixedWait;
            tempValue = (endPos - moveTarget.position).sqrMagnitude;    //�����Ȳ�ӽ÷������ϰ� 
            if (sqlMagnitudeValue  < tempValue) //�̵������� ���������ϴµ�  �Ÿ��� �о����ٴ°��� �������� �����ƴٴ°��̴� 
            {
                
                direction = (endPos - moveTarget.position).normalized; //������ �ٽ���´�
            }
            sqlMagnitudeValue = tempValue;  //üũ�� ������ �����Ѵ�.
        }
        //Debug.Log($"end :{(endPos - moveTarget.position).sqrMagnitude} > {checkValue}");
        charcterRigidbody.MovePosition(endPos);
    }


#if UNITY_EDITOR

    [SerializeField]
    Color gizmosColor = Color.black;

    Vector3 gizmosEndPos = Vector3.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        if(moveTarget != null)
        Gizmos.DrawLine(moveTarget.position , gizmosEndPos);
    }


#endif



}
