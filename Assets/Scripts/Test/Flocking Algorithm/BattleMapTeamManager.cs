using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public IControllObject LeaderUnit => leaderUnit;

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
    /// ��ü�̵�ó�� �� ���� ȸ��ó���ϱ����� ��������Ʈ ȸ�� ���� �Ҽ��������Ű��Ƽ� ����.
    /// </summary>
    public Action<Vector3> onRotate;

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
    public void InitData(UnitData[] units)
    {
        if (units != null && units.Length < memberArray.Length) //���� ���� ���� 
        {
            //livingMemberList.Clear();     // Ȥ�� ������ ������������ �ʱ�ȭ ��Ű�� 
            //leaderUnit = null;            // Ȥ�� �ʱ�ȭ �ȵ� ���� ���� �ʱ�ȭ
            int forSize = units.Length;
            for (int i = 0; i < forSize; i++)
            {
                if (units[i] != null) //�迭 �߰��߰� ����ִ� ���� ���������� 
                {
                    memberArray[i].InitDataSetting(
                            this,
                            (UnitDataBaseNode)BattleMapFactory.Instance.GetUnit(units[i].UnitDataBase.UnitType, memberArray[i].transform), //���丮 ������ ������
                            i
                            );
                    if (leaderUnit == null) //ù��° ���ֿ� ������ ���� 
                    {
                        leaderUnit = memberArray[i];
                        leaderUnit.IsLeader = true;
                    }
                    memberArray[i].FlockingDirectionPos = flockingPosArray[i] - flockingPosArray[leaderUnit.ArrayIndex];
                    OnAppendAction(memberArray[i]);
                    livingMemberList.Add(memberArray[i]);
                    memberArray[i].transform.position = leaderUnit.transform.position + memberArray[i].FlockingDirectionPos;
                }
            }

        }
        else 
        {
            Debug.LogWarning($"�ʱ�ȭ�� �����Ͱ� �������� �ʽ��ϴ� data : {units}");
        }
    }

    /// <summary>
    /// ������ ������ �Ӽ��� ã�Ƽ� ���� �Ӽ��� �����ϴ��Լ� 
    /// </summary>
    /// <param name="unit">���� ����</param>
    private void SettingHiddenLeaderAbility(UnitDataBaseNode unit) 
    {

    }


    /// <summary>
    /// �ɹ���ü�� ������ �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="surrenderUnit">�߰��� �ɹ�</param>
    /// <returns>�������� ����true  ���� false</returns>
    public bool AppendUnit(UnitDataBaseNode surrenderUnit) 
    {
        int oldIndex = 0;
        int nextIndex = 0;
        foreach (IControllObject member in livingMemberList) //���������� ���Ƽ� 
        {
            nextIndex = member.ArrayIndex;  //�迭�ε������� �����ͼ� 
            if (oldIndex < nextIndex) //0������ �񱳸� �ϴµ� �⺻������ �����ִ°�� �������̶� �ش����ǿ� �Ȱɷ����Ѵ�. ����Ʈ ������ ���⶧���� 
            {
                //next�� ũ�ٴ°��� �迭���� ���� �����Ѵٴ� ���̴� ������ ����
                surrenderUnit.transform.SetParent(memberArray[oldIndex].transform);                                     // �߰��� ������ �θ� �����ϰ� 
                memberArray[oldIndex].InitDataSetting(this, surrenderUnit, oldIndex);       // ������ �ʱ�ȭ 
                memberArray[oldIndex].FlockingDirectionPos = flockingPosArray[oldIndex] - flockingPosArray[leaderUnit.ArrayIndex];
                OnAppendAction(memberArray[oldIndex]);
                livingMemberList.Insert(oldIndex, memberArray[oldIndex]); //��ũ�� ����Ʈ�� �ٲܰ�... ArrayList�����̶� �迭������ ��°Ŷ� ������ ��������ȴٴµ�..
                return true;
            }
            oldIndex ++;    //0������ ���������� �ε��� ���ϱ����� ����
        }
        return false;
    }

    /// <summary>
    /// �ɹ��� �߰��� ������ �޾Ƽ� ó���ϴ��Լ�
    /// </summary>
    /// <param name="surrenderUnit">�߰��� �ɹ� </param>
    /// <returns>�������� ������ true  ���н� false</returns>
    public bool AppendUnit(IControllObject surrenderUnit) 
    {
        return AppendUnit(surrenderUnit.UnitObject);
    }

    /// <summary>
    /// ���� ������ �׺��� ������ target ������ �̵� �ϱ����� �Լ�
    /// </summary>
    /// <param name="member">�׺��� �ɹ�</param>
    /// <returns>�׺� ������ true ���н� false </returns>
    public bool SurrenderUnit(IControllObject member) 
    {
        if (AppendUnit(member)) //���� �߰� �õ�
        { 
            //������ 
            member.SurrenderDataReset(); //���� �ɹ����� ������ �ʱ�ȭ ���� 
            return true;
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
        onRotate?.Invoke(movePos);
    }

    /// <summary>
    /// ������������ ���̶�� ȣ���ϱ�
    /// </summary>
    public void OnAssemble() 
    {
        onAssemble?.Invoke(leaderUnit.transform.position);
        onRotate?.Invoke(leaderUnit.transform.position);
    }

    /// <summary>
    /// ���õ� ������ �ʱ�ȭ 
    /// </summary>
    public void ResetData() 
    {
        foreach (IControllObject member in memberArray)
        {
            member.ResetData();
            OnRemoveAction(member);
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
    
    private void OnAppendAction(IControllObject member)
    {
        member.onDie += UnitDieAndNextLeaderSetting;
        //member.onCollisionOnOff += member.CharcterMoveProcess.PropIsCollision;
    }
    private void OnRemoveAction(IControllObject member)
    {
        member.onDie -= UnitDieAndNextLeaderSetting;
        //if (member.CharcterMoveProcess != null) 
        //{
        //    member.onCollisionOnOff -= member.CharcterMoveProcess.PropIsCollision;
        //}
    }

}

