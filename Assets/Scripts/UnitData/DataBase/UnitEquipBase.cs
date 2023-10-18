using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���з�
/// </summary>
public enum EquipType 
{
    None= -1,
    Weapone,        // ����         
    Sheild,         // ����
    Armor,          // ���ʷ�
    Helm,           // ������
    Glove,          // �尩��
    Boot,           // �Ź߷�
    Accessories     // �Ǽ��縮��
}

/// <summary>
/// ��� �з� �ɷ�ġ�� 
/// </summary>
public enum EquipStateType 
{
    None = -1,
    PhysicalPower,      //���� ���� Ÿ��
    PhysicalHit,        //���� ���� Ÿ��
    PhysicalDef,        //���� ��� Ÿ��
    PhysicalEvasion,    //���� ȸ�� Ÿ��
    RangedDef,          //���Ÿ� ��� Ÿ��
    RangedEvasion,      //���Ÿ� ȸ�� Ÿ��
    MagicPower,         //���� �Ŀ� Ÿ��
    MagicDef,           //���� ��� Ÿ��
}
/// <summary>
/// �Ҹ�ǰ Ÿ��
/// </summary>
public enum ConsumeType 
{
    None = -1,          //�Ҹ�ȵǴ� ������
    Equip,              //���Ǵ� ������
    Regain,             //ȸ�� ������
    Nerf,               //���� ������
    Buff,               //���� ������
    Flag,               //�̺�Ʈ �÷��� ������
}
/// <summary>
/// ������ �����ϰ� �ִ� ������ ���� 
/// </summary>
public class UnitEquipBase : MonoBehaviour
{
    
}
