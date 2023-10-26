using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSearchController : MonoBehaviour
{
    [SerializeField]
    float searchRange = 1.0f;
    [SerializeField]
    LayerMask searchLayer;

    [SerializeField]
    LayerMask propLayer;

    /// <summary>
    /// �ֺ� �˻��ؼ� ����� Ÿ���� ��ġ ��ǥ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns>Ÿ���� ��ǥ��</returns>
    public Vector3 GetSearchTarget()
    {
        Collider[] checkingTarget = Physics.OverlapSphere(transform.position, searchRange, searchLayer); //�ֺ� �˻��ؼ� ������ ã�´�.
        Vector3 resultVector = transform.position; //�⺻��ȯ���� �ڱ��ڽ���ġ
        float targetRange = float.MaxValue;
        foreach (Collider target in checkingTarget)
        {
            if (targetRange > (target.transform.position - transform.position).sqrMagnitude)    //�Ÿ� ���ϰ� 
            {
                //������ ��ġ ����
                if (!IsStraightProp(target.transform.position)) //������ ��ֹ��� ������ 
                {
                    resultVector = target.transform.position;   //��ġ ����
                }
            }
        }
        return resultVector;
    }

    /// <summary>
    /// ������ ��ֹ��� �ִ��� üũ�ϴ� �Լ�
    /// </summary>
    /// <returns>��ֹ� ������ true ������ false</returns>
    public bool IsStraightProp(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        return Physics.Raycast(transform.position,dir,searchRange, propLayer);
    }
}
