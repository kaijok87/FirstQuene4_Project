using System;
using UnityEngine;

/// <summary>
/// ���� ������ ��Ƶ� ��嵥����
/// </summary>
public class UnitBaseNode : PoolUnitBase, IUnitDefaultBase
{
    [Header("������ ���� ")]
    /// <summary>
    /// �������� ����
    /// ���� ��ġ�� �����ִ� ����
    /// </summary>
    [SerializeField]
    int memberIndex = -1;
    public int MemberIndex
    {
        get => memberIndex;
        set => memberIndex = value;
    }

    /// <summary>
    /// ���� ���� ��Ƶ� ��ü 
    /// </summary>
    UnitData unitData;
    public UnitData UnitData => unitData;
    
    /// <summary>
    /// ��Ʋ �ʿ��� ����� �����͸� ������ ����ü 
    /// </summary>
    protected UnitRealTimeState unitRealTimeState;
    public UnitRealTimeState UnitRealTimeState => unitRealTimeState;

    /// <summary>
    /// ������ ���ÿ� ������ ������ �ʿ��ҽ� ������ ��������Ʈ
    /// </summary>
    public Action onDie { get; set; }

    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField]
    GameObject unitPrefab;
    public GameObject UnitPrefab => unitPrefab;

    protected virtual void Awake()
    {
        unitRealTimeState = new UnitRealTimeState();    //Ʋ ��Ƶΰ� 
    }

    /// <summary>
    /// ���� �����ͷ� �ʱⰪ ���ÿ� �Լ� 
    /// �ε�ÿ��� ��� 
    /// </summary>
    /// <param name="unitData">���� ������</param>
    /// <param name="unitPrefab">���� ��</param>
    public virtual void InitDataSetting(UnitData unitData, GameObject unitPrefab)
    {
        this.unitPrefab = unitPrefab;
        InitDataSetting(unitData);
        SetUnitDataSetting();
    }
    public void InitDataSetting(UnitData unitData)
    {
        this.unitData = unitData;
    }

    /// <summary>
    /// �⺻�� ������ �ǽð� ���� ������ ���ÿ� �Լ� 
    /// ��ü������ ������ �뵵 
    /// </summary>
    protected virtual void SetUnitDataSetting() 
    {
    }

    /// <summary>
    /// ��� �ٲ� �κ������� ������ �뵵 
    /// </summary>
    protected virtual void SetEquipChange() 
    {

    }



    /// <summary>
    /// ĳ���� ������ ó���� �Լ�
    /// </summary>
    protected virtual void OnDie() 
    {
        onDie?.Invoke();    
    }

    /// <summary>
    /// ĳ���� �̸� ���� �Լ�
    /// ���ӳ��ο��� ĳ���� �̸������ ����� �Լ�
    /// </summary>
    /// <param name="newName">����� �̸�</param>
    protected virtual void UnitNameChange(string newName) 
    {
        //unitData.UnitStateData.UnitName = newName;
    }

 
}
