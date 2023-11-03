using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharcterRotateBase : MonoBehaviour
{
    /// <summary>
    /// 회전속도를 정할 변수
    /// </summary>
    [SerializeField]
    [Range(0.01f,5.0f)]
    float charcterRotateSpeed;

    /// <summary>
    /// 회전 코루틴을 담아둘 반복자
    /// </summary>
    IEnumerator rotateCoroutine;

    public void InitDataSetting() 
    {
        rotateCoroutine = CharcterRotateCoroutine(Vector3.zero);
    }

    /// <summary>
    /// 순차적으로 회전 처리할 함수
    /// </summary>
    public void OnRotateRealTime(Vector3 dir) 
    {
        StopCoroutine(rotateCoroutine);
        rotateCoroutine = CharcterRotateCoroutine(dir);
        StartCoroutine(rotateCoroutine);
    }

   
    /// <summary>
    /// 방향백터를 받아서 바로 회전시켜버리는 함수
    /// </summary>
    /// <param name="dir">방향백터</param>
    public void OnRotate(Vector3 dir) 
    {
        transform.Rotate(dir);
    }

    /// <summary>
    /// 순차적으로 회전 적용할 코루틴
    /// </summary>
    IEnumerator CharcterRotateCoroutine(Vector3 dir) 
    {
        float timeElaspad = 0.0f;
        dir.y = 0.0f;
        Quaternion lookDir = Quaternion.LookRotation(dir);

        //Debug.Log($"시작{lookDir}");
        while (timeElaspad < 1.0f) 
        {
            timeElaspad += Time.fixedDeltaTime * charcterRotateSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation,lookDir,Time.fixedDeltaTime * charcterRotateSpeed);
            yield return null;
        }
        transform.rotation = lookDir;
        //Debug.Log("회전끝");
    }
}
