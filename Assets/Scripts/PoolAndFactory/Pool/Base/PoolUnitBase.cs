using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUnitBase : PoolObjectBase
{
    [Header("Ǯ������Ʈ���� ������ ����  ")]
    [SerializeField]
    int generateCapasity;
    public int Capacity => generateCapasity;


    public virtual void ResetData() 
    {

    }
}
