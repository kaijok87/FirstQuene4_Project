using System;
using Unity.VisualScripting;
using UnityEngine;

/*
 ��ũ���ͺ� �� �ʿ��� �ּҵ����� ���� 
 1. ���� ���� 
 2. ������ �� ������ 
 3. �ʱ�ȭ�� �ʿ��� ��������Ʈ �� �Լ� 
  1,2���� �����ͻ󿡼� ������ �����ϰ� �����.
 */

/// <summary>
/// ���� Ÿ�Ժ� �⺻ ������ ���ÿ�
/// </summary>
[CreateAssetMenu(fileName = "UnitBase", menuName = "CreateUnit_Type", order = 1)]
public class Scriptable_UnitType_DataBase : ScriptableObject
{
    /// <summary>
    /// ���� �⺻���� 
    /// </summary>
    [SerializeField]
    UnitStateData unitStateData;
    public UnitStateData UnitStateData => unitStateData;

    /// <summary>
    /// ��ũ���ͺ� �� �������� ����ü �ɹ��� Instance�������� ���� �ؽ� ���� ������.
    /// </summary>
    [SerializeField]
    UnitDataBase unitBaseData;
    public UnitDataBase UnitDataBase  => unitBaseData;


    /// <summary>
    /// ������ ������ 
    /// </summary>
    [SerializeField]
    UnitBaseNode unitPrefabBase;

    public UnitBaseNode UnitPrefab => unitPrefabBase;
    
    [SerializeField]
    int poolSizeCapasity = 0;
    public int PoolSizeCapasity => poolSizeCapasity;
   
}
