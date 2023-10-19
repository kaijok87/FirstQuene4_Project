using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
///  �� 1���� 1���� ���۳�Ʈ�� �����Ѵ�.
///  
/// ��� ����
/// 1. ��ü �������� ���ŵǸ� �ش��ε��� �������� ����� �� +- �� ���ؼ� ������ ���������� ������ ���
///  
/// 2. ��ü �ɹ��� ������ ��ω���� üũ�ϴ� ���������� üũ�� ���� ���ؾ��Ѵ� ���� transform ���� �⺻ �����س���. ��Ʈ�� ���Ҽ������� �غ��������� 
/// </summary>
public class BattleMapTeamManager : MonoBehaviour
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
    int memberCapacity = 20;

    /// <summary>
    /// �ش籺ü�� �������ִ� �ɹ� �迭
    /// ���⿡ ���� �迭 ������ġ�� ������ �����ϴ� ���� 3���� �ִ�
    /// 1. �����ʿ��� �ش� �ɹ��� ������ �׾������ ���� 
    /// 2. �����ʿ��� ������ ������ �׺��ؼ� �Ʊ��������� �����Ҷ� �迭�� ��������� �߰� 
    /// 3. �� ȭ�鿡�� ���ѳ����� ���� (�ش系���� �����̿Ϸ������ �ѹ����Ѵ�)
    /// </summary>
    IControllObject[] memberArray;
    public IControllObject[] MemberArray => memberArray;
    /// <summary>
    /// ��ü�̵���ų ������ ���������� ó���� ����Ʈ
    /// ��Ʋ�ʿ��� ��ü �̵� ����ó�������� ����� ����Ʈ 
    /// 1. ��Ʋ�ʿ��� memberArray ���� �����ɰ�� �ش縮��Ʈ ���뵵 �����̵Ǿ��Ѵ�.
    /// 2. �� ȭ�鿡�� ���ѳ����� �ش縮��Ʈ���� �����ϵ����Ѵ�. (�ش系���� �����̿Ϸ������ �ѹ����Ѵ�)
    ///  2-1.�̽������� ��ü�� ������ ���������� �о icontrollObject ���� ���ݰ��� �����ؼ� �־�д�.
    /// </summary>
    List<IControllObject> livingMemberList;

    /// <summary>
    /// ��ü�� ������ġ�� ������ ���Ͱ�
    /// </summary>
    Vector3[] flockingPosArray;
    




    /// <summary>
    /// ������ ��ü ������� �����϶�� ��ȣ�� ������ ��������Ʈ
    /// </summary>
    public Action<Vector3> onAssemble;

    /// <summary>
    /// ��ü �迭�� �̵���ȣ�� ������.
    /// �̵��� ����� �̵��Ÿ� �� Ÿ���� �����ϸ� Ÿ���� �������� ������.
    /// </summary>
    public Action<Vector3, float, float> onMove;

    /// <summary>
    /// ��ü �ɹ����� �̵��� ���߶�� ��ȣ�� ������.
    /// </summary>
    public Action onStop;

    private void Start()
    {
        InitFlockingMember();
    }

    /// <summary>
    /// ��ü �ɹ� �⺻Ʋ ���� 
    /// </summary>
    private void InitFlockingMember()
    {
        memberArray = new IControllObject[memberCapacity];
        livingMemberList = new(memberCapacity);
        for (int i = 0; i < memberCapacity; i++)
        {
            memberArray[i] = (IControllObject)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingMember, transform);
        }

    }


    /// <summary>
    /// ��ü�� ���� �����ǰ� �����ϱ� 
    /// </summary>
    /// <param name="floackingPos"> ��ü ���� �����ǰ� �� �迭</param>
    public void SetFlockingPosArray(Vector3[] floackingPos = null)
    {
        if (floackingPos == null) //���̾����� �׽�Ʈ �� ���� 
        {
            floackingPos = new Vector3[flockingPosArray.Length];

            floackingPos[0] = new Vector3(0, 0, 0);

            for (int i = 1; i < flockingPosArray.Length; i++)
            {
                floackingPos[i] = new Vector3(UnityEngine.Random.Range(0.5f, 10.0f), 0, UnityEngine.Random.Range(0.5f, 10.0f));
            }
        }
        flockingPosArray = floackingPos;
    }

    /// <summary>
    /// ��ü �ɹ� �����ϴ� �Լ� 
    /// �Ź� ���Ӱ� ����Ʈ�� ���� �ϱ⶧���� ���ֽ����ϸ� ������.
    /// </summary>
    public void InitData()
    {
        PoolObj_Unit unitObject = (PoolObj_Unit)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingUnit, memberArray[0].transform);
        memberArray[0].InitDataSetting(this, unitObject, flockingPosArray[0], 0);
        memberArray[0].onDie += UnitDieAndNextLeaderSetting;
        memberArray[0].IsLeader = true;
        leaderUnit = memberArray[0];
        

        int forSize = memberArray.Length;
        for (int i = 1; i < forSize; i++)
        {
            unitObject = (PoolObj_Unit)BattleMapFactory.Instance.GetObject(BattleMapPoolNames.FlockingUnit, memberArray[i].transform);
            memberArray[i].InitDataSetting(this, unitObject , flockingPosArray[i], i);
            memberArray[i].onDie += UnitDieAndNextLeaderSetting;
        }
        livingMemberList = new List<IControllObject>(memberArray);
        //Debug.Log($"{onAssemble} , {onMove} , {onStop}");
    }

    private void SettingHiddenLeaderAbility(PoolObj_Unit unit) 
    {

    }


    /// <summary>
    /// �ɹ���ü�� ������ �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="unit">�߰��� ����</param>
    /// <returns>�������� ����true  ���� false</returns>
    public bool AppendUnit(PoolObj_Unit unit) 
    {
        int oldIndex = 0;
        int nextIndex = 0;
        foreach (IControllObject member in livingMemberList) //���������� ���Ƽ� 
        {
            nextIndex = member.ArrayIndex;  //�迭�ε������� �����ͼ� 
            if (oldIndex < nextIndex) //0������ �񱳸� �ϴµ� �⺻������ �����ִ°�� �������̶� �ش����ǿ� �Ȱɷ����Ѵ�.
            {
                //next�� ũ�ٴ°��� �迭���� ���� �����Ѵٴ� ���̴� ������ ����
                unit.transform.SetParent(memberArray[oldIndex].transform);                                  // �θ� �����ϰ� 
                memberArray[oldIndex].InitDataSetting(this, unit, flockingPosArray[oldIndex], oldIndex);    // ������ �ʱ�ȭ 
                memberArray[oldIndex].onDie += UnitDieAndNextLeaderSetting;                                 // �״��Լ� ����
                livingMemberList.Insert(oldIndex, memberArray[oldIndex]); //��ũ�� ����Ʈ�� �ٲܰ�... ArrayList�����̶� �迭������ ��°Ŷ� ������ ��������ȴٴµ�..
                return true;
            }
            oldIndex ++;    //0������ ���������� �ε��� ���ϱ����� ����
        }
        return false;
    }


    /// <summary>
    /// ���� ��ġ�� �ɹ� ��ü��ġ�� �ٽü����ϴ� �Լ�
    /// </summary>
    public void SettingFlockingPos() 
    {
        foreach (IControllObject liveMember in livingMemberList)
        {
            flockingPosArray[liveMember.ArrayIndex] = liveMember.SetFlockingDirectionPos();
        }

    }

    /// <summary>
    /// Ư���������� ��ü ��ü�� �̵��ϴ� ����
    /// </summary>
    /// <param name="movePos">�̵��� ��ġ</param>
    public void OnFlockingMove(Vector3 movePos) 
    {
        leaderUnit?.OnAssemble(movePos);
        onAssemble?.Invoke(movePos);
    }

    /// <summary>
    /// ������������ ���̶�� ȣ���ϱ�
    /// </summary>
    public void OnAssemble() 
    {
        onAssemble?.Invoke(leaderUnit.transform.position);
    }

    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    public void ResetData() 
    {
        foreach (IControllObject member in memberArray)
        {
            member.ResetData();
        }
        leaderUnit = null;
        livingMemberList.Clear();
        //Debug.Log($"{onAssemble} , {onMove} , {onStop}");
    }

    /// <summary>
    /// ������ ������ ó�� �ϱ����� �Լ� 
    /// �迭�� ����Ʈ���� ���� �� ���� ���׾����� ��������
    /// ��� ������ �׾����� ó���ҷ��� 
    /// </summary>
    /// <param name="member">���� ������ �ɹ�</param>
    private void UnitDieAndNextLeaderSetting(IControllObject member)
    {
        livingMemberList.Remove(member);
        memberArray[member.ArrayIndex] = null;

        if (member.IsLeader)  //���� ������ ������ 
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


}

