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

    private void Awake()
    {
        rotateCoroutine = CharcterRotateCoroutine();
    }

    /// <summary>
    /// 순차적으로 회전 처리할 함수
    /// </summary>
    public void OnRotateRealTime() 
    {
        StopCoroutine(rotateCoroutine);
        rotateCoroutine = CharcterRotateCoroutine();
        StartCoroutine(rotateCoroutine);
    }

    /// <summary>
    /// 한번에 회전 처리할 함수
    /// </summary>
    public void OnRotate(Vector3 eulers) 
    {
        transform.Rotate(eulers);
    }

    /// <summary>
    /// 순차적으로 회전 적용할 코루틴
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
