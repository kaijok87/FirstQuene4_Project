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
/// ���� ���۰��� �⺻ ���۳�Ʈ 
/// 
/// �Ⱦ��� ����� ���븸 �ű���
/// </summary>
public class UnitAction_BaseProcess : MonoBehaviour
{
    /// <summary>
    /// ĳ������ �̵��� ������ ������Ʈ
    /// </summary>
    [SerializeField]
    IMoveBase charcterMoveProcess;
    public IMoveBase CharcterMoveProcess => charcterMoveProcess;

    Collider charcterCollider;


    private void Awake()
    {
        charcterMoveProcess = GetComponentInChildren<IMoveBase>();
        charcterMoveProcess.InitDataSetting(0.0f);
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
    /// ��ü�� ���ս�ȣ�� �޾Ƽ� ������ġ�� �̵���Ű�� ����
    /// <param name="originPos">�̵���ų ������</param>
    /// </summary>
    public void OnAssemble(Vector3 originPos)
    {
        //charcterMoveProcess.SetMoveDistanceSubtractiveOperation(0.0f);
        //Vector3 dir = originPos + flockingDirectionPos - transform.position;
        //charcterMoveProcess.OnMove(dir.normalized, dir.magnitude);
    }

    /// <summary>
    /// ��ü�̵��� ������Ҷ� ����� �Լ�
    /// </summary>
    private void OnFlockingMovingStop()
    {
        charcterMoveProcess.OnMovingStop();
    }
  
    /// <summary>
    /// ���� �̺�Ʈ�Լ��� �̿��ؼ� �������� �ͳθ� ������ ��������� �������� ������ 
    /// 1. ó���������� �浹��ġ�� �ȵǰ�
    /// 2. �浹 ���߿� ���ʿ���  �ٸ�������Ʈ�� �浹�Ǽ� �з��� �ȵȴ�.
    /// </summary>
    [SerializeField]
    float testValue = 0.05f;

    /// <summary>
    /// �浹������ġ
    /// </summary>
    [SerializeField]
    Vector3 collisionStartPos;

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
