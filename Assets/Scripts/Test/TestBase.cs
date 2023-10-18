using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBase : MonoBehaviour
{
    MainInputSystem inputSystem;
    protected virtual void Awake() 
    {
        inputSystem = new();
    }
    private void Start()
    {
        InitInputActionSetting();
    }
    private void OnDisable()
    {
        ResetInputActionSetting();
    }
    private void InitInputActionSetting()
    {
        inputSystem.TestKey.Enable();
        inputSystem.TestKey.Test1.performed += Test1;
        inputSystem.TestKey.Test2.performed += Test2;
        inputSystem.TestKey.Test3.performed += Test3;
        inputSystem.TestKey.Test4.performed += Test4;
        inputSystem.TestKey.Test5.performed += Test5;
        inputSystem.TestKey.TestLeftClick.performed += TestLeftClick;
        inputSystem.TestKey.TestRightClick.performed += TestRightClick;
    }
    private void ResetInputActionSetting()
    {
        inputSystem.TestKey.TestRightClick.performed -= TestRightClick;
        inputSystem.TestKey.TestLeftClick.performed -= TestLeftClick;
        inputSystem.TestKey.Test5.performed -= Test5;
        inputSystem.TestKey.Test4.performed -= Test4;
        inputSystem.TestKey.Test3.performed -= Test3;
        inputSystem.TestKey.Test2.performed -= Test2;
        inputSystem.TestKey.Test1.performed -= Test1;
        inputSystem.TestKey.Disable();
    }

    protected virtual void TestRightClick(InputAction.CallbackContext context)
    {
    }

    protected virtual void TestLeftClick(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test5(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test4(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test3(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test2(InputAction.CallbackContext context)
    {
    }

    protected virtual void Test1(InputAction.CallbackContext context)
    {
    }
}
