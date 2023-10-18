
public enum CorpsInfoNodeType
{
    None = 0,               //데이터가 안보일때 표시할 노드.
    TypeName,               //한줄에 표시되는 것이 무엇인지 표시하는 노드
    CharterName,            //캐릭터 이름이 들어갈 노드
    EquipItem,              //캐릭터의 장비상태가 들어갈 노드
    CharterStatus,          //캐릭터의 상태이상에 대한 노드 (전투상황도 포함 돌격,후퇴,대기,진형유지 ,타격받는중 등 )
    StateValue,             //캐릭터의 실질적인 데이터값이 표시될 노드 (숫자만)
    Gap                     //노드간의 구분을 위해 표시해줄 갭 노드
}

public interface IBaseCorpsInfoNode 
{
    
    public CorpsInfoNodeType NodeType { get; set; }

    public void SetValue();
}