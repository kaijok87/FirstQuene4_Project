using System;
using UnityEngine;
/// <summary>
/// ���� ������ ����뵵�� ����� ���°�
/// </summary>
[Serializable]
public struct UnitStateData
{
    /// <summary>
    /// ������ �̸���
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
    /// ������ ����
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
    /// ������ ���� �Ƿε� 
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
    /// ������������ �ڽ��� ��ġ�� 
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