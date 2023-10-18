
using System;
using UnityEngine;

/// <summary>
/// 기능 정리 
/// 1. 유닛 죽었을경우 엔딩 크레딧에 언제 어디서 무엇을하다 누구에게 죽었는지 표시해주기위한 데이터수집기능이필요 
/// 
/// </summary>
public interface IUnitDataBase 
{
    Transform transform { get; }

    Action onDie { get; set; }
    void InitDataSetting();
    void ResetData();

}
