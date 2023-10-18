using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectBase : MonoBehaviour
{
    /// <summary>
    /// 풀의 큐로 데이터를 돌리기위한 델리게이트
    /// </summary>
    public Action onReset;

    /// <summary>
    /// 초기 데이터 셋팅할 함수
    /// </summary>
    public virtual void InitDataSetting() 
    {
    }

    /// <summary>
    /// 리셋할 데이터 셋팅할 함수
    /// </summary>
    public virtual void ResetData() 
    {
        onReset?.Invoke();
    }
}
