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

    public void InitDataSetting() 
    {
        rotateCoroutine = CharcterRotateCoroutine(Vector3.zero);
    }

    /// <summary>
    /// ���������� ȸ�� ó���� �Լ�
    /// </summary>
    public void OnRotateRealTime(Vector3 dir) 
    {
        StopCoroutine(rotateCoroutine);
        rotateCoroutine = CharcterRotateCoroutine(dir);
        StartCoroutine(rotateCoroutine);
    }

   
    /// <summary>
    /// ������͸� �޾Ƽ� �ٷ� ȸ�����ѹ����� �Լ�
    /// </summary>
    /// <param name="dir">�������</param>
    public void OnRotate(Vector3 dir) 
    {
        transform.Rotate(dir);
    }

    /// <summary>
    /// ���������� ȸ�� ������ �ڷ�ƾ
    /// </summary>
    IEnumerator CharcterRotateCoroutine(Vector3 dir) 
    {
        float timeElaspad = 0.0f;
        dir.y = 0.0f;
        Quaternion lookDir = Quaternion.LookRotation(dir);

        //Debug.Log($"����{lookDir}");
        while (timeElaspad < 1.0f) 
        {
            timeElaspad += Time.fixedDeltaTime * charcterRotateSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation,lookDir,Time.fixedDeltaTime * charcterRotateSpeed);
            yield return null;
        }
        transform.rotation = lookDir;
        //Debug.Log("ȸ����");
    }
}
