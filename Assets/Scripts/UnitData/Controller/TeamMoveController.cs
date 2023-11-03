using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ���� �ϳ� ���̵��ϱ����� ���۳�Ʈ 
/// </summary>
public class TeamMoveController : MonoBehaviour
{
    /// <summary>
    /// �������� ���� ����
    /// </summary>
    [SerializeField]
    ITeamUnit teamObject;

    /// <summary>
    /// ������ ���� �̵� ����
    /// </summary>
    UnitMoveController[] moveController;


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
    /// �̵��� ����� �̵��Ÿ��� ������.
    /// </summary>
    public Action<Vector3, float> onMove;

    /// <summary>
    /// ��ü �ɹ����� �̵��� ���߶�� ��ȣ�� ������.
    /// </summary>
    public Action onStop;

    //IMoveBase 



    private void Awake()
    {
        teamObject = GetComponent<ITeamUnit>();
        moveController = new UnitMoveController[teamObject.UnitMaxCapacity];
    }

    public void InitDataSetting() 
    {
        //�׼� ���� �ϱ� 
    }

    /// <summary>
    /// �̺�Ʈ��ȣ�� �ޱ����� �����ϴ� �Լ� 
    /// ĳ���Ͱ� ��Ʈ�� �ϴ� ������ 
    /// </summary>
    private void AppendAction(UnitMoveController unitControl) 
    {
        //onAssemble += unitControl.
    }

    /// <summary>
    /// �̺�Ʈ��ȣ�� �޴°��� �����ϱ����� �Լ� 
    /// </summary>
    private void RemoveAction() 
    {

    }

    /// <summary>
    /// �ڵ� ���� ���� 
    /// </summary>
    private void AutoBattle() 
    {
    }

}
