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
    IUnitDataBase UnitData { get; }

    /// <summary>
    /// 죽었을때 실행할 델리게이트
    /// </summary>
    Action<IControllObject> onDie { get; set; }

    /// <summary>
    /// 집합호출시 실행할 함수
    /// </summary>
    void OnAssemble(){ }

    /// <summary>
    /// 집단 이동호출시 실행할 함수
    /// </summary>
    void CharcterMove(Vector3 direction, float distance, float radius = 0.0f) { }

    /// <summary>
    /// 집단 이동도중 멈추라고 호출시 실행할 함수
    /// </summary>
    void OnFlockingMovingStop() { }

    /// <summary>
    /// 데이터 초기화할 함수
    /// </summary>
    /// <param name="parnetNode">부모 군체</param>
    /// <param name="index">자신의 인덱스값</param>
    void InitDataSetting(FlockingManager parnetNode, PoolObj_Unit unitObject, int index);

    /// <summary>
    /// 셋팅된 데이터 초기화 
    /// </summary>
    void ResetData();
}
