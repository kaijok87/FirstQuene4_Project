using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemCode { }
public enum EventFlag { }

/// <summary>
/// ������ �⺻ ������ 
/// ��ũ���ͺ�� ����� Ŭ����
/// </summary>
[CreateAssetMenu(fileName ="New Item Data",menuName ="Scriptable Object/Item Data",order =1)]
public class ItemBase : ScriptableObject
{
    [Header("������ �⺻ ������")]
    
    /// <summary>
    /// ������ ���� 
    /// </summary>
    [SerializeField]
    ItemCode code;
    public ItemCode Code => code;

    /// <summary>
    /// ������ �̸�
    /// </summary>
    [SerializeField]
    string itemName = "������ �̸�";
    public string ItemName => itemName;

    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField]
    GameObject itemPrefab;
    public GameObject ItemPrefab => itemPrefab;

    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField]
    Sprite itemIcon;
    public Sprite ItemIcon => itemIcon;

    /// <summary>
    /// ������ �ݾ�
    /// </summary>
    [SerializeField]
    uint price = 0;
    public uint Price => price;

    /// <summary>
    /// ������ �ִ� �ߺ� ����
    /// </summary>
    [SerializeField]
    uint maxStackCount = 1;
    public uint MaxStackCount => maxStackCount;

    /// <summary>
    /// �����ۿ����� ����
    /// </summary>
    [SerializeField]
    string itemDescription = "����";
    public string ItemDescription => itemDescription;

    /// <summary>
    /// �������� � ������� ���Ǵ��������� Ÿ��
    /// ��ɿ��ῡ �ʿ��� ����
    /// </summary>
    [SerializeField]
    ConsumeType consumeType;
    public ConsumeType ConsumeType => consumeType;
}
