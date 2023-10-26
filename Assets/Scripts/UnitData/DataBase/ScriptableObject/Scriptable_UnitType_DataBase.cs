using System;
using UnityEngine;



/// <summary>
/// ���� Ÿ�Ժ� �⺻ ������ ���ÿ�
/// </summary>
[CreateAssetMenu(fileName = "UnitBase", menuName = "CreateUnit_Type", order = 1)]
public class Scriptable_UnitType_DataBase : ScriptableObject
{
    /// <summary>
    /// ��ũ���ͺ� �� �������� ����ü �ɹ��� Instance�������� ���� �ؽ� ���� ������.
    /// </summary>
    [SerializeField]
    public UnitDataBase unitBaseData;
    public UnitDataBase UnitDataBase  => unitBaseData;

}
