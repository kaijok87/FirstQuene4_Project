using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemCode { }
public enum EventFlag { }

/// <summary>
/// 아이템 기본 데이터 
/// 스크립터블로 만드는 클래스
/// </summary>
[CreateAssetMenu(fileName ="New Item Data",menuName ="Scriptable Object/Item Data",order =1)]
public class ItemBase : ScriptableObject
{
    [Header("아이템 기본 데이터")]
    
    /// <summary>
    /// 아이템 종류 
    /// </summary>
    [SerializeField]
    ItemCode code;
    public ItemCode Code => code;

    /// <summary>
    /// 아이템 이름
    /// </summary>
    [SerializeField]
    string itemName = "아이템 이름";
    public string ItemName => itemName;

    /// <summary>
    /// 아이템 프리팹
    /// </summary>
    [SerializeField]
    GameObject itemPrefab;
    public GameObject ItemPrefab => itemPrefab;

    /// <summary>
    /// 아이템 아이콘
    /// </summary>
    [SerializeField]
    Sprite itemIcon;
    public Sprite ItemIcon => itemIcon;

    /// <summary>
    /// 아이템 금액
    /// </summary>
    [SerializeField]
    uint price = 0;
    public uint Price => price;

    /// <summary>
    /// 아이템 최대 중복 갯수
    /// </summary>
    [SerializeField]
    uint maxStackCount = 1;
    public uint MaxStackCount => maxStackCount;

    /// <summary>
    /// 아이템에대한 설명
    /// </summary>
    [SerializeField]
    string itemDescription = "설명";
    public string ItemDescription => itemDescription;

    /// <summary>
    /// 아이템이 어떤 방식으로 사용되는지에대한 타입
    /// 기능연결에 필요한 종류
    /// </summary>
    [SerializeField]
    ConsumeType consumeType;
    public ConsumeType ConsumeType => consumeType;
}
