using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

/// <summary>
/// ���� ������Ʈ Ǯ 
/// </summary>
/// <typeparam name="T">������� �������̽� </typeparam>
public class Pool_Unit_Base<T> : MonoBehaviour where T : UnitBaseNode
{
    /// <summary>
    /// �迭Ȯ��� ũ��
    /// </summary>
    int expendValue = 2;

    /// <summary>
    /// ���� �迭ũ��
    /// </summary>
    [SerializeField]
    protected int capasity = 0;

    /// <summary>
    /// Ǯ�� ������ �⺻ ������
    /// </summary>
    [SerializeField]
    protected T poolPrefab;

    /// <summary>
    /// ������ �������� �� �迭 
    /// </summary>
    T[] poolArray;
    
    /// <summary>
    /// ���������� ���ɼ��ְ� ������ ť
    /// </summary>
    Queue<T> poolQueue;
    public void InitDataSetting() 
    {
        poolArray = new T[capasity];
        poolQueue = new Queue<T>(capasity);
        PoolDataSetting();
    }
    /// <summary>
    /// ť���� �������� ������ ������ �ְ� ������ Ȯ���� �����ִ� �Լ�
    /// </summary>
    /// <returns>Ǯ���������Ǵ� ������Ʈ ��ȯ</returns>
    public T GetPoolObject()
    {
        T resultObjcect;
        if (poolQueue.Count > 0)
        {
            resultObjcect = poolQueue.Dequeue();
        }
        else
        {
            ExpendPool();
            resultObjcect = GetPoolObject();
        }
        return resultObjcect;
    }

    /// <summary>
    /// Ǯ�� ������ �����ؼ� �ִ� �۾�
    /// </summary>
    /// <param name="startindex">�迭�� ������ġ</param>
    protected virtual void PoolDataSetting(int startindex = 0)
    {
        int forSize = poolArray.Length;
        for (int i = startindex; i < forSize; i++)
        {
            T poolObject = Instantiate(poolPrefab, transform);
            poolObject.name = $"{typeof(T).Name}_{i}";
            poolObject.onResetData += () => {
                poolQueue.Enqueue(poolObject);
                poolObject.transform.SetParent(transform);
            };
            poolQueue.Enqueue(poolObject);
            poolObject.gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// Ǯ�� �迭ũ�Ⱑ �����Ұ�� �ø��� �۾� 
    /// </summary>
    protected virtual void ExpendPool() 
    {
        int newCapasity = capasity * expendValue;

        T[] newPoolArray = new T[newCapasity];
        
        for (int i = 0; i < capasity; i++)
        {
            newPoolArray[i] = poolArray[i]; 
        }
        
        poolArray = newPoolArray;

        PoolDataSetting(capasity);
        
        capasity = newCapasity;
    }

    /// <summary>
    /// ��ũ���ͺ� ����� ������ ���ÿ� 
    /// </summary>
    /// <param name="prefab">��ũ���ͺ� ����� ������</param>
    /// <param name="capasity">���� ���� </param>
    public virtual void SetPrefab(T prefab, int capasity) 
    {
        poolPrefab = prefab;
        this.capasity = capasity; 
    }
}
