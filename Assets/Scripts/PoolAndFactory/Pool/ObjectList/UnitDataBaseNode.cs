using System;
using UnityEngine;

/// <summary>
/// ĳ���� �⺻ �ɷ�ġ ���� Ŭ����
/// </summary>
public class UnitDataBaseNode : PoolObjectBase, IUnitStateTable
{
    /// <summary>
    /// ��ũ���ͺ�μ��õ� �⺻ ������ �޾ƿ� ��ü 
    /// </summary>
    [SerializeField]
    Scriptable_UnitType_DataBase scriptableObject;

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
    /// ����ϰ��ִ� ���Ŭ���� 
    /// </summary>
    UnitEquipBase UnitEquipBase { get; set; }

    /// <summary>
    /// ������ ���ÿ� ������ ������ �ʿ��ҽ� ������ ��������Ʈ
    /// </summary>
    public Action onDie { get; set; }


    protected virtual void Awake()
    {
        unitRealTimeState = new UnitRealTimeState();    //Ʋ ��Ƶΰ� 
        unitData = new UnitData();
    }

    /// <summary>
    /// �ʱⰪ ���ÿ� 
    /// </summary>
    /// <param name="index">���� ������ �ε�����</param>
    /// <param name="unitStateData">�ʱⵥ����</param>
    public virtual void InitDataSetting(int index, UnitStateData unitStateData)
    {
        unitData.OnDataChange(index,scriptableObject.UnitDataBase, unitStateData);
        SetUnitDataSetting();
    }

    /// <summary>
    /// �������Ͽ��� �о�ͼ� �Ľ̿뵵�� ����� �Լ�
    /// </summary>
    /// <param name="index">���� ������ �ε�����</param>
    /// <param name="unitDataBase">������ �⺻�ɷ�ġ��</param>
    /// <param name="unitStateData">�ʱⵥ����</param>
    public virtual void LoadDataParsing(int index, UnitDataBase unitDataBase, UnitStateData unitStateData) 
    {
        unitData.OnDataChange(index, unitDataBase, unitStateData);
        SetUnitDataSetting();
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
    /// ��� Ŭ�������� �����͸� �������� �Լ�
    /// </summary>
    /// <param name="equipData">��� Ŭ����</param>
    /// <returns>��� ���� �߰��� �ɷ�ġ</returns>
    protected virtual int UnitEquipData(UnitEquipBase equipData, EquipStateType stateType) 
    {
        return int.MinValue; //�������̵� �ؼ� ����ؾ��Ѵ�. 
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

    public void InitDataSetting(UnitStateData unitData)
    {
        throw new NotImplementedException();
    }
}
