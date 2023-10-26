using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleTest : TestBase
{
    [SerializeField]
    UnitMoveController testCharter;

    [SerializeField]
    BattleMapTeamManager flockingManager;

    [SerializeField]
    float cameraZPos = 0.0f;

    [SerializeField]
    GameSpeedType tempSType;
    [SerializeField]
    Vector3[] flockingPos =
        {
            new Vector3(0,0,0),new Vector3(3,0,0),new Vector3(6,0,0),new Vector3(9,0,0),
            new Vector3(12,0,0),new Vector3(-3,0,0),new Vector3(-6,0,0),new Vector3(-9,0,0),
            new Vector3(-12,0,0),new Vector3(2,0,1),new Vector3(2,0,4),new Vector3(2,0,7),
            new Vector3(2,0,10),new Vector3(-2,0,1),new Vector3(-2,0,4),new Vector3(-2,0,7),
            new Vector3(-2,0,10),new Vector3(5,0,0),new Vector3(5,0,2), new Vector3(5,0,5)

        };
    [SerializeField]
    UnitDataBaseNode[] testPlayerTeam;
    [SerializeField]
    UnitDataBaseNode[] testEnemyTeam;

    UnitSearchController testPlayerSearch;
    UnitSearchController testEnemySearch;

    [SerializeField]
    BindingFlags bindings;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < testPlayerTeam.Length; i++)
        {
            if (testPlayerTeam[i] != null)
            {
                testPlayerTeam[i].MemberIndex = i;
            }
        }
    }
    protected override void Test1(InputAction.CallbackContext context)
    {
        Type type = typeof(UnitDataBase);
        FieldInfo[] fields = type.GetFields(bindings);
        PropertyInfo[] properties = type.GetProperties(bindings);
        PropertyBuilder pBuilder;
        MethodBuilder mBuilder;
        //foreach (FieldInfo field in fields)
        //{
        //    Debug.Log($"필드 이름 :  {field.Name}");
        //    Debug.Log($"필드 타입 : {field.FieldType}");
        //    Debug.Log($"private : {field.IsPrivate}");
        //    Debug.Log($"속성여부 :{field.Attributes}");
        //    Debug.Log($"사용자속성여부 : {field.CustomAttributes}");
        //    Debug.Log($"DeclaringType : {field.DeclaringType}");
        //    Debug.Log($"필드핸들러 :{field.FieldHandle}");
        //    Debug.Log($"필드 ReflectedType {field.ReflectedType}");
            
        //}
        foreach (PropertyInfo property in properties) 
        {
            Debug.Log(property.Name);
            Debug.Log(property.MemberType);
            Debug.Log(property.PropertyType);
            Debug.Log(property.ReflectedType);
            Debug.Log(property.GetMethod);
            Debug.Log(property.SetMethod);
            Debug.Log(property.IsSpecialName);
            Debug.Log(property.Attributes);
            Debug.Log(property.CanRead);
            Debug.Log(property.CanWrite);
        }
    }
}
