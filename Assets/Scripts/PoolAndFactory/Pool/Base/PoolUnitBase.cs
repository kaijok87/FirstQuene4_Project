using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUnitBase : PoolObjectBase
{
    [Header("풀오브젝트전용 데이터 셋팅  ")]
    [SerializeField]
    int generateCapasity;
    public int Capacity => generateCapasity;


    public virtual void ResetData() 
    {

    }
}
