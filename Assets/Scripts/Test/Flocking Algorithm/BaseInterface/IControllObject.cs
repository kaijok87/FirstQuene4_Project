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
    /// 맴버의 초기위치를 자신의 위치로 수정하는 함수
    /// </summary>
    Vector3 SetFlockingDirectionPos();

    /// <summary>
    /// 캐릭터 개별이동함수
    /// </summary>
    /// <param name="endPos">도착 위치값</param>
    public void OnAssemble(Vector3 endPos);

    /// <summary>
    /// 데이터 초기화할 함수
    /// </summary>
    /// <param name="parnetNode">부모 군체</param>
    /// <param name="flockingPos">군체에서의 자신의 위치값</param>
    /// <param name="index">자신의 인덱스값</param>
    void InitDataSetting(BattleMapTeamManager parnetNode, PoolObj_Unit unitObject, Vector3 flockingPos , int index);

    /// <summary>
    /// 셋팅된 데이터 초기화 
    /// </summary>
    void ResetData();
}
