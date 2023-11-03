
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 팀별 생성에 사용될 공통적인 내용 
/// </summary>
public interface ITeamUnit
{
    /// <summary>
    /// 현재 팀의 종류 
    /// </summary>
    TeamType Type { get; }

    /// <summary>
    /// 유닛 배열크기값
    /// </summary>
    public int UnitMaxCapacity { get; }

    /// <summary>
    /// 리더 유닛 
    /// </summary>
    public IUnitDefaultBase LeaderUnit { get; }

    /// <summary>
    /// 팀이 가지고있는 유닛 배열
    /// </summary>
    IUnitDefaultBase[] Units { get; }

    /// <summary>
    /// 살아있는 유닛 리스트 일괄 처리용
    /// </summary>
    List<IUnitDefaultBase> LivingMemberList { get; }

    /// <summary>
    /// 팀의 이동로직
    /// </summary>
    TeamMoveController MoveController { get; }

    /// <summary>
    /// 유닛 추가용 함수
    /// </summary>
    /// <param name="unit">추가할 유닛</param>
    /// <returns>추가 성공시 true 실패시 false</returns>
    bool AppendUnit(IUnitDefaultBase unit);

    /// <summary>
    /// 유닛 제거용 함수
    /// </summary>
    /// <param name="unit">제거할 유닛</param>
    void RemoveUnit(IUnitDefaultBase unit);

    /// <summary>
    /// 생성된 팀 데이터 초기화 시키는 함수 
    /// </summary>
    /// <param name="type">팀 타입</param>
    /// <param name="units">팀 유닛 리스트</param>
    /// <param name="initPos">팀 초기위치 좌표</param>
    void InitDataSetting(TeamType type, IUnitDefaultBase[] units, in Vector3 initPos);

    /// <summary>
    /// 풀로 돌리기위한 리셋함수
    /// </summary>
    void ResetData();
}
