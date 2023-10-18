using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpInfoGap : MonoBehaviour, IBaseCorpsInfoNode
{
    public CorpsInfoNodeType NodeType 
    { 
        get => CorpsInfoNodeType.Gap; 
        set 
        { } 
    }
    RectTransform rt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    public void SetValue()
    {
        //창크기조절
    }
}
