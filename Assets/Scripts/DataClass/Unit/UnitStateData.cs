using System;
using UnityEngine;
/// <summary>
/// 유닛 데이터 저장용도로 사용할 상태값
/// </summary>
[Serializable]
public struct UnitStateData
{
    /// <summary>
    /// 유닛의 이름값
    /// </summary>
    [SerializeField]
    string unitName;
    public string UnitName
    {
        get => unitName;
        set
        {
            if (unitName !=  value)
            {
                unitName = value;
                onNameChange?.Invoke(unitName);
            }
        }
    }
    public Action<string> onNameChange;

    /// <summary>
    /// 유닛의 레벨
    /// </summary>
    [SerializeField]
    int level;
    public int Level
    {
        get => level;
        set
        {
            if (level != value)
            {
                level = value;
                onLevelChange?.Invoke(level);
            }
        }
    }
    public Action<int> onLevelChange;

    /// <summary>
    /// 유닛의 현재 피로도 
    /// </summary>
    [SerializeField]
    float fatigue;
    public float Fatigue
    {
        get => fatigue;
        set
        {
            if (fatigue != value)
            {
                fatigue = value;
                onFatigueChange?.Invoke(fatigue);
            }
        }
    }
    public Action<float> onFatigueChange;

    /// <summary>
    /// 팀기준점에서 자신의 위치값 
    /// </summary>
    Vector3 colonyMemberPosition;
    public Vector3 ColonyMemberPosition
    {
        get => colonyMemberPosition;
        set
        {
            if (colonyMemberPosition != value)
            {
                colonyMemberPosition = value;
                onColonyMemberPositionChange?.Invoke(colonyMemberPosition);
            }
        }
    }
    public Action<Vector3> onColonyMemberPositionChange;
}