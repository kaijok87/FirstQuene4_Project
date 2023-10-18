using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase<T> : MonoBehaviour where T : PoolObjectBase
{
    /// <summary>
    /// 배열확장시 크기
    /// </summary>
    int expendValue = 2;

    /// <summary>
    /// 현재 배열크기
    /// </summary>
    [SerializeField]
    int capasity = 0;

    /// <summary>
    /// 풀을 생성할 기본 프리팹
    /// </summary>
    [SerializeField]
    T poolPrefab;

    /// <summary>
    /// 생성된 프리팹이 들어갈 배열 
    /// </summary>
    T[] poolArray;
    
    /// <summary>
    /// 순차적으로 사용될수있게 관리할 큐
    /// </summary>
    Queue<T> poolQueue;

    protected virtual void Awake()
    {
        InitDataSetting();
    }

    /// <summary>
    /// 데이터 초기값 셋팅
    /// </summary>
    public virtual void InitDataSetting() 
    {
        poolArray = new T[capasity];
        poolQueue = new Queue<T>(capasity);
        PoolDataSetting();
    }

    /// <summary>
    /// 큐에서 꺼낼것이 있으면 꺼내서 주고 없으면 확장후 꺼내주는 함수
    /// </summary>
    /// <returns>풀에서관리되는 오브젝트 반환</returns>
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
    /// 풀의 내용을 생성해서 넣는 작업
    /// </summary>
    /// <param name="startindex">배열의 시작위치</param>
    protected virtual void PoolDataSetting(int startindex = 0)
    {
        int forSize = poolArray.Length;
        for (int i = startindex; i < forSize; i++)
        {
            T poolObject = Instantiate(poolPrefab,transform);
            poolObject.name = $"{typeof(T).Name}_{i}";
            poolObject.onReset += () => {
                poolQueue.Enqueue(poolObject);
                poolObject.transform.SetParent(transform);
            };
            poolQueue.Enqueue(poolObject);
        }

    }

    /// <summary>
    /// 풀의 배열크기가 부족할경우 늘리는 작업 
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
}
