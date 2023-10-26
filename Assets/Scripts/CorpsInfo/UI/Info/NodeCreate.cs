using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum Info_Name 
{
    HP = 0,     //����ü��
    FT ,        //�Ƿε�
    ST,         //���� ���� (��� , ���� , �뿭���� , ���� , �������ֱ�, �ǰ�, ����, ���� ���) 
    ITEM,       //������ ������
    LV,         //����
    MXHP,       //�ִ���
    NAME,       //�̸�
    AT,         //���ݷ�
    AR,         //���� Ȯ��
    DF,         //����
    DR,         //ȸ����
    AD,         //���Ÿ� ����
    AA,         //���Ÿ� ȸ����
    MT,         //���� ���ݷ�
    MD,         //���� ����
    CP,         //ũ��Ƽ�� Ȯ��
    CD,         //ũ��Ƽ�� ������
}
/// <summary>
/// �����ʿ����� ������ �ǽð� ���°�
/// </summary>
public enum UnitCondition
{
    Wait = 0,     // ���
    Move,         // �̵�
    Stop,         // ����
    Run,          // ����
    Attack,       // ����
    TakingDamage, // �ǰݻ���
    Stun,         // ����
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
    /// ��Ŀ�� ������ 
    /// �⺻ �ǹ� X : 0.0f , Y : 1.0f ���� ���� �������εд�
    /// PosX PosY width height top left right bottom ���� ���� 0���� �ʱ�ȭ 
    /// </summary>
    /// <param name="obj">��Ʈ������ ������Ʈ</param>
    /// <param name="anchorsMin">min ����</param>
    /// <param name="anchorsMax">max ����</param>
    private void RectSetting(GameObject obj, Vector2 anchorsMin, Vector2 anchorsMax)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt = rt == null ? obj.AddComponent<RectTransform>() : rt; //��ƮƮ������ ��������

        rt.pivot = new Vector2(0.0f, 1.0f); //������ �������� �ٲٰ�

        rt.anchorMax = anchorsMax;   //�ƽ��� �����ϰ� 
        rt.anchorMin = anchorsMin;   //�ΰ� �����ϰ�  

        //�����ȵڿ� ���� �ٲ�͵��� �ٽ� 0������ �ʱ�ȭ 
        rt.sizeDelta = Vector2.zero;        // width height �ʱ�ȭ        
        rt.anchoredPosition3D = Vector3.zero; // PosX,PosY,PosZ �ʱ�ȭ 

        rt.offsetMax = Vector2.zero; //left, top �ʱ�ȭ
        rt.offsetMin = Vector2.zero; //right. bottom �ʱ�ȭ

    }


    private void CreateNodeArray()
    {
        Info_Name[] infoName = (Info_Name[])Enum.GetValues(typeof(Info_Name)); //�̳� ������ŭ ���θ����

        for (int i = 0; i < infoName.Length; i++)
        {
            Info_Line_Data info_Line = Create_Info_Line(i); // ���� ����
            
            Add_Name_Node(info_Line); //���ξȿ� ���ӳ�����
            
            for (int j = 0; j < unitLength; j++)
            {
                Add_Unit_Node(info_Line,j); //���ξȿ� ���� ������
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
