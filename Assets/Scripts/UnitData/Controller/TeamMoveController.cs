using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 하나 씩이동하기위한 컴퍼넌트 
/// </summary>
public class TeamMoveController : MonoBehaviour
{
    /// <summary>
    /// 내가속한 팀의 정보
    /// </summary>
    [SerializeField]
    ITeamUnit teamObject;

    /// <summary>
    /// 움직일 유닛 이동 로직
    /// </summary>
    UnitMoveController[] moveController;


    /// <summary>
    /// 정해진 군체 모양으로 집합하라고 신호를 보내는 델리게이트
    /// </summary>
    public Action<Vector3> onAssemble;

    /// <summary>
    /// 군체이동처리 시 같이 회전처리하기위한 델리게이트 회전 따로 할수도있을거같아서 뺏다.
    /// </summary>
    public Action<Vector3> onRotate;

    /// <summary>
    /// 군체 배열의 이동신호를 보낸다.
    /// 이동할 방향과 이동거리를 보낸다.
    /// </summary>
    public Action<Vector3, float> onMove;

    /// <summary>
    /// 군체 맴버들의 이동을 멈추라고 신호를 보낸다.
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
        //액션 연결 하기 
    }

    /// <summary>
    /// 이벤트신호를 받기위해 연결하는 함수 
    /// 캐릭터가 컨트롤 하는 팀들의 
    /// </summary>
    private void AppendAction(UnitMoveController unitControl) 
    {
        //onAssemble += unitControl.
    }

    /// <summary>
    /// 이벤트신호를 받는것을 해제하기위한 함수 
    /// </summary>
    private void RemoveAction() 
    {

    }

    /// <summary>
    /// 자동 전투 로직 
    /// </summary>
    private void AutoBattle() 
    {
    }

}
