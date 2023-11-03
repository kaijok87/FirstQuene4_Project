using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ĳ����(����) �� �������ɷ�ġ 
/// 1. ���� ĳ���� �� ���� �ӵ� ���� 
/// 2. ���� ĳ���� �� ���� �Ƿε� ��ġ���� 
/// 3. 
/// </summary>
public struct HiddenLeaderAbility 
{
    /// <summary>
    /// �̵��ӵ� ����ġ
    /// </summary>
    float moveSpeed;

    /// <summary>
    /// �Ƿε� ����ġ
    /// </summary>
    float fatigueValue;

    public HiddenLeaderAbility(float moveSpeed, float fatigueValue) 
    {
        this.moveSpeed = moveSpeed;
        this.fatigueValue = fatigueValue;
    }
    public void ResetData() 
    {
        moveSpeed = 0f;
        fatigueValue = 0f;
    }
}

/// <summary>
/// ���� �����͸� ������ ������Ʈ 
/// </summary>
public class TeamBaseNode : PoolObjectBase , ITeamUnit
{
    /// <summary>
    /// �������� ���� 
    /// </summary>
    TeamType teamType;
    public TeamType Type => teamType;

    /// <summary>
    /// ���� �ִ� �ο��� ������ ��
    /// </summary>
    [SerializeField]
    int unitMaxCapacity = 20;
    public int UnitMaxCapacity => unitMaxCapacity;

    /// <summary>
    /// �ش籺ü�� �������ִ� �ɹ� �迭
    /// ���⿡ ���� �迭 ������ġ�� ������ �����ϴ� ���� 3���� �ִ�
    /// 1. �����ʿ��� �ش� �ɹ��� ������ �׾������ ���� v
    /// 2. �����ʿ��� ������ ������ �׺��ؼ� �Ʊ��������� �����Ҷ� �迭�� ��������� �߰�  v
    /// 3. �� ȭ�鿡�� ���ѳ����� ���� (�ش系���� �����̿Ϸ������ �ѹ����Ѵ�) 
    /// </summary>
    IUnitDefaultBase[] units;
    public IUnitDefaultBase[] Units => units;

    /// <summary>
    /// ���� ���� �⺻������ 0���� �����̴�.
    /// ��ü�� �߽ɿ� �ִ� �������̵� ������ ��ġ 
    /// </summary>
    IUnitDefaultBase leaderUnit;
    public IUnitDefaultBase LeaderUnit => leaderUnit;


    /// <summary>
    /// ���� �Ӽ� ����
    /// </summary>
    HiddenLeaderAbility mainUnitHiddenAbility;
    public HiddenLeaderAbility MainUnitHiddenAbility => mainUnitHiddenAbility;


    /// <summary>
    /// ���� �ǽð� ���� ��Ʈ�� ������ ����� ����Ʈ 
    /// </summary>
    List<IUnitDefaultBase> livingMemberList;
    public List<IUnitDefaultBase> LivingMemberList => livingMemberList;



    /// <summary>
    /// ��ü�� ������ġ�� ������ ���Ͱ�
    /// </summary>
    Vector3[] flockingPosArray;
    public Vector3[] FlockingPosArray => flockingPosArray;

    /// <summary>
    /// �̵����� �����ص� ����
    /// </summary>
    TeamMoveController moveController;
    public TeamMoveController MoveController => moveController;
    

    protected virtual void Awake()
    {
        units = new IUnitDefaultBase[unitMaxCapacity];
        livingMemberList = new List<IUnitDefaultBase>(unitMaxCapacity);
        moveController = GetComponent<TeamMoveController>();
    }

    /// <summary>
    /// �ܺο��� ������ �ʱ�ȭ �ϱ����� �Լ�
    /// </summary>
    /// <param name="type">���� Ÿ�� (�Ʊ� ,����, �߸�, ���)</param>
    /// <param name="units">���� ������ </param>
    /// <param name="initPos">in�� ����� ���縦 ���� ���� ó�� ��ġ��</param>
    public void InitDataSetting(TeamType type, IUnitDefaultBase[] units, in Vector3 initPos)
    {
        this.teamType = type;
        InitData(units,initPos);
    }

    /// <summary>
    /// ��ü�� ���� �����ǰ� �����ϱ� 
    /// </summary>
    /// <param name="floackingPos"> ��ü ���� �����ǰ� �� �迭</param>
    public void SetFlockingPosArray(Vector3[] floackingPos)
    {
        flockingPosArray = floackingPos;
    }

    /// <summary>
    /// ��ü �ɹ� �����ϴ� �Լ� 
    /// �Ź� ���Ӱ� ����Ʈ�� ���� �ϱ⶧���� ���ֽ����ϸ� ������.
    /// <param name="appendUnits"> �ɹ��� �߰��� ���ֵ�</param>
    /// <param name="initPos">in�� ����� ���縦 ���� ���� ó�� ��ġ��</param>
    /// </summary>
    public void InitData(IUnitDefaultBase[] appendUnits ,in Vector3 initPos)
    {
        if (appendUnits != null) 
        {
            int forSize = appendUnits.Length;
            if (units.Length >= forSize) //���� ���� ���� �ִ� ���� ������ ���ų� �������� ����  
            {
                IUnitDefaultBase appendUnit;
                for (int i = 0; i < forSize; i++)
                {
#if UNITY_EDITOR
                    if (GameManager.Instance.IsDebugCheck) 
                    {
                        Debug.Log($"TeamObject �ʱ�ȭ {i}��° : {appendUnits[i]}");
                    }
#endif
                    appendUnit = appendUnits[i];
                    if (appendUnit != null) //�迭 �߰��߰� ����ִ� ���� ���������� ���� �ִ°�츸 üũ 
                    {
                        if (leaderUnit == null) //ù��° ���ֿ� ������ ���� 
                        {
                            leaderUnit = appendUnit;
                            SettingHiddenLeaderAbility();
                            leaderUnit.transform.position = initPos;
                        }
                        else //������ �������̱⶧���� ��������� ���� ������ ���� 
                        {
                            appendUnit.UnitData.SetFlockingPos(flockingPosArray[i]);
                            appendUnit.transform.position = initPos+ flockingPosArray[i];
                        }
                        AddUnit(i, appendUnit);
                    }
                }
                return;
            }
            Debug.LogWarning($"�ʱ�ȭ�� �����Ͱ� �����ϴ� �߰��� ũ�� :{appendUnits.Length}  �ִ� ũ�� : {units.Length}");

        }
        else
        {
            Debug.LogWarning($"�ʱ�ȭ�� �����Ͱ� �������� �ʽ��ϴ� data : {appendUnits}");
        }
    }

    /// <summary>
    /// ������ ������ �Ӽ��� ã�Ƽ� ���� �Ӽ��� �����ϴ��Լ� 
    /// </summary>
    private void SettingHiddenLeaderAbility() 
    {
        //������ �������� ������ �Ӽ��� ã�Ƽ� �����ϴ� ���� �ۼ� 
    }

    /// <summary>
    /// ���� �������� �߰� ó���ϴ� �Լ� 
    /// </summary>
    /// <param name="index">�Է��� �迭��ġ</param>
    /// <param name="unit">�Է��� ������ </param>
    private void AddUnit(int index, IUnitDefaultBase unit) 
    {
        //unit.UnitRealTimeState.MemberIndex = index;
        unit.CurrentTeam = this;
        units[index] = unit;
        livingMemberList.Add(unit);
        OnAppendAction(unit);
    }


    /// <summary>
    /// �ɹ���ü�� ������ �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="appendUnit">�߰��� ����</param>
    /// <returns>�������� ����true  ���� false</returns>
    public bool AppendUnit(IUnitDefaultBase appendUnit) 
    {
        int forSize = units.Length;
        IUnitDefaultBase unit;
        for (int i = 0; i < forSize; i++)
        {
            unit = units[i];
            if (unit == null) //������ ���������������� �߰� 
            {
                //unit.UnitRealTimeState.ColonyMemberPosition = flockingPosArray[i];
                AddUnit(i,unit);
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// �׺��� ������ ���� ���� �������� �̵� ��ų���ִ��� üũ�ؼ� �̵���Ű�� �Լ� 
    /// </summary>
    /// <param name="surrenderUnit">�׺��� �ɹ�</param>
    /// <param name="surrenderTeam">�׺��� ������ ��</param>
    /// <returns>�׺� ������ true ���н� false </returns>
    public bool SurrenderUnit(ITeamUnit surrenderTeam, IUnitDefaultBase surrenderUnit) 
    {
        if (AppendUnit(surrenderUnit)) //���� �߰� �õ�
        { 
            surrenderTeam.RemoveUnit(surrenderUnit);    //�׺��� �������� ���� ����� 
            return true;
        }
        return false;
    }


    /// <summary>
    /// ���� ��ġ�� �ɹ� ��ü��ġ�� �ٽü����ϴ� �Լ�
    /// </summary>
    public void SettingFlockingPos() 
    {
        foreach (IUnitDefaultBase liveMember in livingMemberList)
        {
            flockingPosArray[liveMember.GetMemberIndex()] = liveMember.GetFlockingPos;
        }

    }

    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    public override void ResetData()
    {
        leaderUnit = null;

        int forSize = units.Length;
        IUnitDefaultBase unit;
        for (int i = 0; i < forSize; i++) //������ ��������� �����ʹ� �ٸ������� ó�� 
        {
            unit = units[i];
            if (unit != null) 
            {
                unit.ResetData();
                OnRemoveAction(unit);
            }
            units[i] = null;
        }

        livingMemberList.Clear();
        
        mainUnitHiddenAbility.ResetData();
        
        base.ResetData();
    }

    /// <summary>
    /// ������ ������ ó�� �ϱ����� �Լ� 
    /// �迭�� ����Ʈ���� ���� �� ���� ���׾����� ��������
    /// ��� ������ �׾����� ó���ҷ��� 
    /// </summary>
    /// <param name="removeUnit">������ ����</param>
    public void RemoveUnit(IUnitDefaultBase removeUnit)
    {
        livingMemberList.Remove(removeUnit);
        int index = removeUnit.GetMemberIndex();
        
        OnRemoveAction(removeUnit);
        units[index] = null;

        if (livingMemberList.Count > 0) //����ִ� ������ ������  
        {
            if (removeUnit == leaderUnit)  //���� ������ ������ 
            {
                leaderUnit = livingMemberList[0]; //��ó�� ������ ������ ���� 
                SettingHiddenLeaderAbility();
            }
        }
        else // ����ִ� ������ ���°�� ó�� 
        {
            //�ش籺ü�� ���������� ó���� ���� �ۼ� 
        }
    }
    
    /// <summary>
    /// ���������� ������ �ʿ��� �۾��� �ִ°�� ���⿡ ���� �ۼ��Ѵ� .
    /// </summary>
    /// <param name="index">�߰��� �� �ɹ��� �ε���</param>
    private void OnAppendAction(IUnitDefaultBase unit)
    {
        unit.onDie += RemoveUnit;
    }

    /// <summary>
    /// ���������� ������ ������ �۾��� �ִ°�� ���⿡ ���� �ۼ��Ѵ� .
    /// </summary>
    /// <param name="index">������ �� �ɹ��� �ε���</param>
    private void OnRemoveAction(IUnitDefaultBase unit)
    {
        unit.onDie -= RemoveUnit;
    }


}

