using System;
using System.Collections;
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
}

/// <summary>
/// ��� ����
/// 1. ��ü �������� ���ŵǸ� �ش��ε��� �������� ����� �� +- �� ���ؼ� ������ ���������� ������ ���
///  
/// 2. ��ü �ɹ��� ������ ��ω���� üũ�ϴ� ���������� üũ�� ���� ���ؾ��Ѵ� ���� transform ���� �⺻ �����س���. ��Ʈ�� ���Ҽ������� �غ��������� 
/// </summary>
public class FlockingManager : MonoBehaviour
{
    /// <summary>
    /// ���� �Ӽ� ����
    /// </summary>
    HiddenLeaderAbility mainUnitHiddenAbility;
    public HiddenLeaderAbility MainUnitHiddenAbility => mainUnitHiddenAbility;

    /// <summary>
    /// ���� ���� �⺻������ 0���� �����̴�.
    /// </summary>
    IControllObject leaderUnit;

    /// <summary>
    /// ��ü�� �߽ɿ� �ִ� �������̵� ������ ��ġ 
    /// </summary>
    public IControllObject FlockingCenterPos => leaderUnit;

    [SerializeField]
    int memberCapacity = 18;

    /// <summary>
    /// �ش籺ü�� �������ִ� �ɹ� �迭
    /// ���⿡ ���� �迭 ������ġ�� ������ �����ϴ� ���� 3���� �ִ�
    /// 1. �����ʿ��� �ش� �ɹ��� ������ �׾������ ���� 
    /// 2. �����ʿ��� ������ ������ �׺��ؼ� �Ʊ��������� �����Ҷ� �迭�� ��������� �߰� 
    /// 3. �� ȭ�鿡�� ���ѳ����� ���� (�ش系���� �����̿Ϸ������ �ѹ����Ѵ�)
    /// </summary>
    IControllObject[] memberArray;

    /// <summary>
    /// ��ü�̵���ų ������ ���������� ó���� ����Ʈ
    /// ��Ʋ�ʿ��� ��ü �̵� ����ó�������� ����� ����Ʈ 
    /// 1. ��Ʋ�ʿ��� memberArray ���� �����ɰ�� �ش縮��Ʈ ���뵵 �����̵Ǿ��Ѵ�.
    /// 2. �� ȭ�鿡�� ���ѳ����� �ش縮��Ʈ���� �����ϵ����Ѵ�. (�ش系���� �����̿Ϸ������ �ѹ����Ѵ�)
    ///  2-1.�̽������� ��ü�� ������ ���������� �о icontrollObject ���� ���ݰ��� �����ؼ� �־�д�.
    ///  
    /// </summary>
    List<IControllObject> livingMemberList;






    /// <summary>
    /// ������ ��ü ������� �����϶�� ��ȣ�� ������ ��������Ʈ
    /// </summary>
    public Action onAssemble;

    /// <summary>
    /// ��ü �迭�� �̵���ȣ�� ������.
    /// �̵��� ����� �̵��Ÿ� �� Ÿ���� �����ϸ� Ÿ���� �������� ������.
    /// </summary>
    public Action<Vector3, float, float> onMove;

    /// <summary>
    /// ��ü �ɹ����� �̵��� ���߶�� ��ȣ�� ������.
    /// </summary>
    public Action onStop;

    /// <summary>
    /// ��ü �ɹ� �����ϴ� �Լ� 
    /// �Ź� ���Ӱ� �ֱ⶧���� ���ֽ����ϸ� ������.
    /// </summary>
    public void InitData()
    {

        memberArray = new IControllObject[memberCapacity];
        int forSize = memberArray.Length;
        PoolObj_Unit unitObject;
        for (int i = 0; i < forSize; i++)
        {
            memberArray[i] = (IControllObject)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingMember,transform);
            unitObject = (PoolObj_Unit)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingMember, memberArray[i].transform);
            memberArray[i].InitDataSetting(this, unitObject , i);
            AddAction(memberArray[i]);
        }
        livingMemberList = new List<IControllObject>(memberArray);
        leaderUnit = memberArray[0]; //��ü�� ��Ʈ���� �������̵� ���� ����
        memberArray[0].IsLeader = true; //��������
    }
    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    public void ResetData() 
    {
        foreach (IControllObject member in livingMemberList)
        {
            member.onDie = null;
            member.ResetData();
        }
        onAssemble = null;
        onMove = null;
        onStop = null;
        leaderUnit = null;
        memberArray = null;
        livingMemberList.Clear();
    }
    /// <summary>
    /// ������ ������ ó�� �ϱ����� �Լ� 
    /// �迭�� ����Ʈ���� ���� �� ���� ���׾����� ��������
    /// ��� ������ �׾����� ó���ҷ��� 
    /// </summary>
    /// <param name="unit">���� ����</param>
    private void UnitDieAndNextLeaderSetting(IControllObject unit)
    {
        livingMemberList.Remove(unit);
        memberArray[unit.ArrayIndex] = null;

        DeleteAction(unit);
        
        if (unit.IsLeader)  //���� ������ ������ 
        {
            if (livingMemberList.Count > 0) //����ִ� ������ ������  
            {
                livingMemberList[0].IsLeader = true; //��ó�� ������ ������ ���� 
            }
        }
        if (livingMemberList.Count < 1) // ����ִ� ������ ���°�� ó�� 
        {
            //�ش籺ü�� ���������� ó���� ���� �ۼ� 
        }
    }


    /// <summary>
    /// �׼� �����ϴ� �Լ�
    /// </summary>
    /// <param name="unit">������ ����</param>
    private void AddAction(IControllObject unit)
    {
        unit.onDie  += UnitDieAndNextLeaderSetting;
        onAssemble  += unit.OnAssemble;
        onMove      += unit.CharcterMove;
        onStop      += unit.OnFlockingMovingStop;
    }

    /// <summary>
    /// �׼� ������� �Լ�
    /// </summary>
    /// <param name="unit">������� ����</param>
    private void DeleteAction(IControllObject unit)
    {
        unit.onDie  -= UnitDieAndNextLeaderSetting;
        onAssemble  -= unit.OnAssemble;
        onMove      -= unit.CharcterMove;
        onStop      -= unit.OnFlockingMovingStop;
    }
}

