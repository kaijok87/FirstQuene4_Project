using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum Info_Name 
{
    HP = 0,     //현재체력
    FT ,        //피로도
    ST,         //현재 상태 (대기 , 공격 , 대열유지 , 후퇴 , 가만히있기, 피격, 공격, 감전 등등) 
    ITEM,       //착용한 아이템
    LV,         //레벨
    MXHP,       //최대피
    NAME,       //이름
    AT,         //공격력
    AR,         //공격 확율
    DF,         //방어력
    DR,         //회피율
    AD,         //원거리 방어력
    AA,         //원거리 회피율
    MT,         //마법 공격력
    MD,         //마법 방어력
    CP,         //크리티컬 확율
    CD,         //크리티컬 데미지
}
/// <summary>
/// 전투맵에서의 유닛의 실시간 상태값
/// </summary>
public enum UnitCondition
{
    Wait = 0,     // 대기
    Move,         // 이동
    Stop,         // 정지
    Run,          // 도망
    Attack,       // 공격
    TakingDamage, // 피격상태
    Stun,         // 스턴
}

public class NodeCreate : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer background;



    readonly int unitLength = 18;
    
    private Info_Corps_Data info_Corps;
    
    private void Awake()
    {
        info_Corps = GetComponent<Info_Corps_Data>();
        CreateNodeArray();
    }

    /// <summary>
    /// 앵커값 조절용 
    /// 기본 피벗 X : 0.0f , Y : 1.0f 왼쪽 위를 기준으로둔다
    /// PosX PosY width height top left right bottom 값들 전부 0으로 초기화 
    /// </summary>
    /// <param name="obj">렉트조절될 오브젝트</param>
    /// <param name="anchorsMin">min 값들</param>
    /// <param name="anchorsMax">max 값들</param>
    private void RectSetting(GameObject obj, Vector2 anchorsMin, Vector2 anchorsMax)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt = rt == null ? obj.AddComponent<RectTransform>() : rt; //렉트트랜스폼 가져오고

        rt.pivot = new Vector2(0.0f, 1.0f); //기준점 왼쪽위로 바꾸고

        rt.anchorMax = anchorsMax;   //맥스값 수정하고 
        rt.anchorMin = anchorsMin;   //민값 수정하고  

        //수정된뒤에 값이 바뀐것들을 다시 0값으로 초기화 
        rt.sizeDelta = Vector2.zero;        // width height 초기화        
        rt.anchoredPosition3D = Vector3.zero; // PosX,PosY,PosZ 초기화 

        rt.offsetMax = Vector2.zero; //left, top 초기화
        rt.offsetMin = Vector2.zero; //right. bottom 초기화

    }


    private void CreateNodeArray()
    {
        Info_Name[] infoName = (Info_Name[])Enum.GetValues(typeof(Info_Name)); //이넘 갯수만큼 라인만든다

        for (int i = 0; i < infoName.Length; i++)
        {
            Info_Line_Data info_Line = Create_Info_Line(i); // 라인 생성
            
            Add_Name_Node(info_Line); //라인안에 네임노드생성
            
            for (int j = 0; j < unitLength; j++)
            {
                Add_Unit_Node(info_Line,j); //라인안에 유닛 노드생성
            }


        }
    }






    private Info_Unit_Node_Data Add_Unit_Node(Info_Line_Data info_Line,int index) 
    {
        GameObject obj = new GameObject(); //heap memory
        
        obj.name = $"Info_Unit_Node_{index:d2}";

        Info_Unit_Node_Data info_Unit_Node = obj.AddComponent<Info_Unit_Node_Data>(); //heap memory
      
 
        info_Unit_Node.transform.SetParent(info_Line.transform);

        Image image = obj.AddComponent<Image>();

        image.sprite = background.sprite;
        image.color = background.color;

        RectSetting(obj, 
            new Vector2(0.1f + (index * 0.05f), 0.0f), 
            new Vector2(0.1f + ((index + 1) * 0.05f), 1.0f));
        
            
        return info_Unit_Node;
    }







    private void Add_Name_Node(Info_Line_Data info_Line) 
    {
        GameObject backNameObj = new GameObject();

        backNameObj.name = "Background_Name";

        Image image = backNameObj.AddComponent<Image>();

        image.color = new Color(0.572549f, 0.6980392f, 0.6352941f);

        backNameObj.transform.SetParent(info_Line.transform);

        RectSetting(backNameObj, Vector2.zero ,new Vector2(0.1f, 1.0f));






        GameObject obj = new GameObject(); //heap memory

        obj.name = "Info_Name_Node";

        Info_Name_Node_Data info_Name_Node = obj.AddComponent<Info_Name_Node_Data>(); //heap memory

        info_Name_Node.transform.SetParent(info_Line.transform);
        
        image = obj.AddComponent<Image>();

        image.color = Color.white;
        
        image.sprite = background.sprite;

        RectSetting(obj, Vector2.zero ,new Vector2(0.1f, 1.0f));





        GameObject backUnitObj = new GameObject();

        backUnitObj.name = "Background_Name";

        image = backUnitObj.AddComponent<Image>();

        image.color = new Color(0.7254902f, 0.7882354f, 0.7568628f);
        
        backUnitObj.transform.SetParent(info_Line.transform);

        RectSetting(backUnitObj, new Vector2(0.1f, 0.0f), new Vector2(1.0f, 1.0f));
    }







    private Info_Line_Data Create_Info_Line(int index) 
    {
        GameObject obj = new GameObject(); //heap memory
        obj.name = $"Info_Line_{index:d2}";

        Info_Line_Data info_Line = obj.AddComponent<Info_Line_Data>(); //heap memory

        info_Line.transform.SetParent(info_Corps.transform);

        RectSetting(obj, 
            new Vector2(0.0f, 0.05f * index), 
            new Vector2(1.0f, 0.05f * (index + 1))
            );

        return info_Line;
    }
}
