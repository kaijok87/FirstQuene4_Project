using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObj_Unit : PoolObjectBase
{
    public override void ResetData()
    {
        base.ResetData();
        transform.position = Vector3.zero;
    }
}
