
using System;
using UnityEngine;

/// <summary>
/// ��� ���� 
/// 1. ���� �׾������ ���� ũ������ ���� ��� �������ϴ� �������� �׾����� ǥ�����ֱ����� �����ͼ���������ʿ� 
/// 
/// </summary>
public interface IUnitDataBase 
{
    Transform transform { get; }

    Action onDie { get; set; }
    void InitDataSetting();
    void ResetData();

}
