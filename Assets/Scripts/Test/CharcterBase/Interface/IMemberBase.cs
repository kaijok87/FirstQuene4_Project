using System;
/// <summary>
/// �ɹ� ��ȣ�� �����ϰ�  
/// ���� ���� �����ؾ��ϰ�
/// ���� �� �����ؾ��ϰ� 
/// �̵� �� ȸ�� �� �����ؾ��ϰ� 
/// ���ݹ� �̵� �˻��� �����ؾ��Ѵ�.
/// �׾����� ����� �����ؾ��ϰ�
/// 
/// </summary>
public interface IMemberBase 
{
    int MemberIndex { get; }
    UnitData UnitData { get; }

    IMoveBase CharcterMoveProcess { get; }

    IRotateBase CharcterRotateProcess { get; }

    Action OnUnitDie { get; set; }

    void InitDataSetting(int memberIndex, UnitData unitData);
    

}
