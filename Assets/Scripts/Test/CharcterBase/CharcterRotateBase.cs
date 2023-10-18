using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterRotateBase : MonoBehaviour
{
    /// <summary>
    /// ȸ���ӵ��� ���� ����
    /// </summary>
    [SerializeField]
    [Range(0.01f,5.0f)]
    float charcterRotateSpeed;

    /// <summary>
    /// ȸ�� �ڷ�ƾ�� ��Ƶ� �ݺ���
    /// </summary>
    IEnumerator rotateCoroutine;

    private void Awake()
    {
        rotateCoroutine = CharcterRotateCoroutine();
    }

    /// <summary>
    /// ���������� ȸ�� ó���� �Լ�
    /// </summary>
    public void OnRotateRealTime() 
    {
        StopCoroutine(rotateCoroutine);
        rotateCoroutine = CharcterRotateCoroutine();
        StartCoroutine(rotateCoroutine);
    }

    /// <summary>
    /// �ѹ��� ȸ�� ó���� �Լ�
    /// </summary>
    public void OnRotate(Vector3 eulers) 
    {
        transform.Rotate(eulers);
    }

    /// <summary>
    /// ���������� ȸ�� ������ �ڷ�ƾ
    /// </summary>
    IEnumerator CharcterRotateCoroutine() 
    {
        float timeElaspad = 0.0f;
        while (timeElaspad < 1.0f) 
        {
            timeElaspad += Time.deltaTime * charcterRotateSpeed;



            yield return null;
        }
    }
}
