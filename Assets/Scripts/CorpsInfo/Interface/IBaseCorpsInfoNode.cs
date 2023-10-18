
public enum CorpsInfoNodeType
{
    None = 0,               //�����Ͱ� �Ⱥ��϶� ǥ���� ���.
    TypeName,               //���ٿ� ǥ�õǴ� ���� �������� ǥ���ϴ� ���
    CharterName,            //ĳ���� �̸��� �� ���
    EquipItem,              //ĳ������ �����°� �� ���
    CharterStatus,          //ĳ������ �����̻� ���� ��� (������Ȳ�� ���� ����,����,���,�������� ,Ÿ�ݹ޴��� �� )
    StateValue,             //ĳ������ �������� �����Ͱ��� ǥ�õ� ��� (���ڸ�)
    Gap                     //��尣�� ������ ���� ǥ������ �� ���
}

public interface IBaseCorpsInfoNode 
{
    
    public CorpsInfoNodeType NodeType { get; set; }

    public void SetValue();
}