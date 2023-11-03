using System;
using UnityEngine;

/// <summary>
/// ���� ������ ��Ƶ� ��嵥����
/// </summary>
public class UnitBaseNode : MonoBehaviour, IUnitDefaultBase
{
    [Header("������ ���� ")]
    /// <summary>
    /// ���� ���� 
    /// </summary>
    UnitData unitData;
    public UnitData UnitData => unitData;

    /// <summary>
    /// ��Ʋ �ʿ��� �ǽð����� ������ ������
    /// </summary>
    protected UnitRealTimeState unitRealTimeState;
    public UnitRealTimeState UnitRealTimeState => unitRealTimeState;

    /// <summary>
    /// ���� ��Ʈ�� �� ���۳�Ʈ �����صα� 
    /// </summary>
    UnitMoveController unitMoveController;
    public UnitMoveController UnitMoveController => unitMoveController;

    /// <summary>
    /// �ڽ��� ���� �� 
    /// </summary>
    ITeamUnit teamUnit;
    public ITeamUnit CurrentTeam
    {
        get => teamUnit;
        set => teamUnit = value;
    }

    /// <summary>
    /// ������ġ�� ������ �⺻��ġ�� ������� ������(����)���� �Ÿ���ǥ�� ��ȯ�ϴ� ������Ƽ
    /// </summary>
    public Vector3 GetFlockingPos => teamUnit.LeaderUnit.transform.position - transform.position;

    /// <summary>
    /// ������ ������ ������ ��������Ʈ
    /// </summary>
    public Action<IUnitDefaultBase> onDie { get; set; }

    /// <summary>
    /// �ڽ��� ���� ť�� �������ȣ�� ���� ��������Ʈ
    /// </summary>
    public Action onResetData;


    protected virtual void Awake()
    {
        unitRealTimeState = new UnitRealTimeState();    //Ʋ ��Ƶΰ� 
        unitMoveController = GetComponent<UnitMoveController>();
    }

    /// <summary>
    /// ���� �����ͷ� �ʱⰪ ���ÿ� �Լ� 
    /// �ε�ÿ��� ��� 
    /// </summary>
    /// <param name="unitData">���� ������</param>
    public virtual void InitDataSetting(UnitData unitData)
    {
        this.unitData = unitData;
        SetUnitDataSetting();
    }
  
    /// <summary>
    /// �⺻�� ������ �ǽð� ���� ������ ���ÿ� �Լ� 
    /// ��ü������ ������ �뵵 
    /// </summary>
    protected virtual void SetUnitDataSetting()
    {
        unitMoveController = transform.GetComponentInChildren<UnitMoveController>();
        //������ ���� 
        AppendActionDelegate();
    }

    /// <summary>
    /// �ʿ��� ��������Ʈ �����ϴ��Լ� 
    /// </summary>
    protected virtual void AppendActionDelegate()
    {
        unitRealTimeState.onDie += OnDie;
    }

    /// <summary>
    /// ����� ��������Ʈ ���� �ϴ��Լ� 
    /// </summary>
    protected virtual void RemoveActionDelegate()
    {
        unitRealTimeState.onDie -= OnDie;
    }

    /// <summary>
    /// ��� �ٲ� �κ������� ������ �뵵 
    /// </summary>
    protected virtual void SetEquipChange() 
    {

    }

    /// <summary>
    /// ������ �װų� �ε�� �ٸ�ĳ���� ������ �Էµɽ� 
    /// ������ �Է��ϱ� ���� ���·� ������ �Լ� 
    /// </summary>
    public virtual void ResetData() 
    {
        unitData = null;
        unitRealTimeState.ResetData();
        onDie = null;
        RemoveActionDelegate();
        gameObject.SetActive(false);
        onResetData?.Invoke();
    }

    /// <summary>
    /// ĳ���� ������ ó���� �Լ�
    /// </summary>
    protected virtual void OnDie() 
    {
        ResetData();
        onDie?.Invoke(this);    
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

    /// <summary>
    ///  �ɹ��� ���õ� �̵� ������ ��ȯ
    /// </summary>
    /// <returns>�̵����� ��</returns>
    public float GetUnitMoveRange()
    {
        return unitRealTimeState.MoveRange;
    }

    /// <summary>
    /// �ɹ��� ���õ� ��ȣ�� �������� �Լ�
    /// </summary>
    /// <returns>�ɹ� �ε�����</returns>
    public int GetMemberIndex()
    {
        return unitRealTimeState.MemberIndex;
    }

    /// <summary>
    /// �ɹ������� �̵��ӵ� ��ȯ 
    /// </summary>
    /// <returns></returns>
    public float GetUnitMoveSpeed()
    {
        return unitRealTimeState.MoveSpeed;
    }
}
