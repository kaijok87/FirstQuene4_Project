using System;
using UnityEngine;

/// <summary>
/// 이동 회전 전투 컨트롤 관련 기능정리할 인터페이스
/// </summary>
public interface IControllObject
{
    /// <summary>
    /// 유니티 오브젝트를 상속해서 컨트롤하기위해 기본적으로 들어가는 트랜스폼
    /// </summary>
    Transform transform { get; }

    /// <summary>
    /// 메인 군체 맴버배열의 인덱스값
    /// </summary>
    int ArrayIndex { get; }

    /// <summary>
    /// 맴버가 리더인지 체크
    /// </summary>
    bool IsLeader { get; set; }

    /// <summary>
    /// 해당군체 맴버에 등록된 유닛
    /// </summary>
    IUnitDefaultBase UnitData { get; }

    /// <summary>
    /// 유닛 움직이는 로직 
    /// </summary>
    public IMoveBase CharcterMoveProcess { get; }

    /// <summary>
    /// 맴버객체가 리더위치에서 얼마나떨어져있는지에대한 위치값
    /// </summary>
    Vector3 FlockingDirectionPos { get; set; }

    /// <summary>
    /// 죽었을때 실행할 델리게이트
    /// </summary>
    Action<IControllObject> onDie { get; set; }

    /// <summary>
    /// 장애물에 충돌 됬을때 장애물 반지름값을 전달하는 함수
    /// </summary>
    //Action<bool> onCollisionOnOff { get; set; }

    /// <summary>
    /// 맴버의 초기위치를 자신의 위치로 수정하는 함수
    /// </summary>
    Vector3 SetFlockingDirectionPos();

    /// <summary>
    /// 캐릭터 개별이동함수
    /// </summary>
    /// <param name="endPos">도착 위치값</param>
    void OnAssemble(Vector3 endPos);

    /// <summary>
    /// 데이터 초기화할 함수
    /// </summary>
    /// <param name="parnetNode">부모 군체</param>
    /// <param name="unitData">유닛 정보 </param>
    /// <param name="index">자신의 인덱스값</param>
    void InitDataSetting(TeamObject parnetNode, IUnitDefaultBase unitData , int index);

    /// <summary>
    /// 셋팅된 데이터 초기화 
    /// </summary>
    void ResetData();

    /// <summary>
    /// 항복시 데이터 셋팅용 함수
    /// </summary>
    void SurrenderDataReset();
}
