using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectBase : MonoBehaviour
{
    /// <summary>
    /// Ǯ�� ť�� �����͸� ���������� ��������Ʈ
    /// </summary>
    public Action onReset;

    /// <summary>
    /// �ʱ� ������ ������ �Լ�
    /// </summary>
    public virtual void InitDataSetting() 
    {
    }

    /// <summary>
    /// ������ ������ ������ �Լ�
    /// </summary>
    public virtual void ResetData() 
    {
        onReset?.Invoke();
    }
}
