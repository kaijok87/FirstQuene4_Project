using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CorpInfoTypeName : MonoBehaviour, IBaseCorpsInfoNode
{
    CorpsInfoNodeType corpsInfoNodeType;
    public CorpsInfoNodeType NodeType 
    {
        get => corpsInfoNodeType;
        set 
        {
            corpsInfoNodeType = value; 
        }
    }
    string nodeTypeName = string.Empty;
    string NodeTypeName 
    {
        get => nodeTypeName;
        set
        {
            nodeTypeName = value;
            
        }
    }
    TextMeshProUGUI text;

    RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    public void SetValue()
    {
        text.text = nodeTypeName;
    }
}
