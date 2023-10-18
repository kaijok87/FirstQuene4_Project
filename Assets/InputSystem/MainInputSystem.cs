//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/InputSystem/MainInputSystem.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MainInputSystem: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInputSystem"",
    ""maps"": [
        {
            ""name"": ""BattleMap"",
            ""id"": ""342efb61-3d2b-4a29-b724-b2a5d7cdf294"",
            ""actions"": [
                {
                    ""name"": ""MouseLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""9bd07b45-0ce7-47aa-93d1-d008327b0618"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""60e5f73e-b895-427d-8d5f-d8c5e7473cad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftArrowKey"",
                    ""type"": ""Value"",
                    ""id"": ""ceb08590-ba46-4d91-b3df-45513451cd3c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightArrowKey"",
                    ""type"": ""Value"",
                    ""id"": ""b20278b4-b4b8-4d26-987c-c00f971dfbe7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UpArrowKey"",
                    ""type"": ""Value"",
                    ""id"": ""0425bad1-d1e6-4160-8e37-d58ba74c939e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DownArrowKey"",
                    ""type"": ""Value"",
                    ""id"": ""4069330e-d2c9-40cf-8fef-b84fd79d9637"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""EnterKey"",
                    ""type"": ""Button"",
                    ""id"": ""a2f035ab-80af-4a28-9a76-dbf562f24a96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLeftPress"",
                    ""type"": ""Button"",
                    ""id"": ""de24be06-34ef-48b6-b122-d45bc2f625f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9d68931b-7927-444c-a4cd-d2572bfcfba1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MouseLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""502f390a-4731-4105-be5d-5d9dd42309ab"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e597fd4-2e2b-42e1-8884-4634687bfd8b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""LeftArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b6cc727-ed12-4aed-875b-aa58455b26d3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""LeftArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f984da9-86fc-4676-bfbc-ba305ce471a9"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""RightArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2660407-1478-404c-83ef-b3b40e2be2e7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""RightArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a025ac9-15ac-4901-bdd1-9843f306f0c1"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""UpArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21fcfcc5-893c-4885-8481-b76a708b6899"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""UpArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2bac9b9-5e64-4958-a395-532e60ed67e8"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""DownArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be8a440d-29da-4d6b-8ebe-4b49134808b5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""DownArrowKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46ce7705-569c-4f60-82d3-7459f9a52a26"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""EnterKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71d489fa-86da-40b7-abb9-6d8f560c544f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MouseLeftPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TestKey"",
            ""id"": ""c0a3b2db-dfe8-422c-a51c-8f28a3e08e0b"",
            ""actions"": [
                {
                    ""name"": ""Test1"",
                    ""type"": ""Button"",
                    ""id"": ""2669f749-35f2-4b76-afb4-9d09106e9298"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test2"",
                    ""type"": ""Button"",
                    ""id"": ""015e0243-df7b-418e-8642-60963e5d09f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test3"",
                    ""type"": ""Button"",
                    ""id"": ""a1cc74e3-7d12-4969-9985-0651ae94cce9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test4"",
                    ""type"": ""Button"",
                    ""id"": ""f9ddca6e-e94d-4425-b27d-c8cda8bf68c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test5"",
                    ""type"": ""Button"",
                    ""id"": ""d71e52dc-a4a2-4253-8c38-2c03b718167e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TestLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""ddfc19fc-8b87-49c6-84c3-ccc775831c58"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TestRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""81e30c3f-1da5-4ffd-952e-8f50038e6617"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9a410050-5e34-4e1a-b15c-a8c55bbafb6c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Test1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2582e190-ad0b-4f17-8edb-2a2eb3909bf5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Test2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""529efd52-a276-427a-b454-2de7bfd330a2"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Test3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7934cbc2-91e8-4201-a455-35fa6f3a77d8"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Test4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""780a32cc-a611-4625-8897-1e31dc54475e"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Test5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65a90599-3fbb-4393-8b7d-342a648f6041"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""TestLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c6621d1-f262-4568-944a-9a6493bb3ce6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""TestRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoardAndMouse"",
            ""bindingGroup"": ""KeyBoardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // BattleMap
        m_BattleMap = asset.FindActionMap("BattleMap", throwIfNotFound: true);
        m_BattleMap_MouseLeftClick = m_BattleMap.FindAction("MouseLeftClick", throwIfNotFound: true);
        m_BattleMap_MouseRightClick = m_BattleMap.FindAction("MouseRightClick", throwIfNotFound: true);
        m_BattleMap_LeftArrowKey = m_BattleMap.FindAction("LeftArrowKey", throwIfNotFound: true);
        m_BattleMap_RightArrowKey = m_BattleMap.FindAction("RightArrowKey", throwIfNotFound: true);
        m_BattleMap_UpArrowKey = m_BattleMap.FindAction("UpArrowKey", throwIfNotFound: true);
        m_BattleMap_DownArrowKey = m_BattleMap.FindAction("DownArrowKey", throwIfNotFound: true);
        m_BattleMap_EnterKey = m_BattleMap.FindAction("EnterKey", throwIfNotFound: true);
        m_BattleMap_MouseLeftPress = m_BattleMap.FindAction("MouseLeftPress", throwIfNotFound: true);
        // TestKey
        m_TestKey = asset.FindActionMap("TestKey", throwIfNotFound: true);
        m_TestKey_Test1 = m_TestKey.FindAction("Test1", throwIfNotFound: true);
        m_TestKey_Test2 = m_TestKey.FindAction("Test2", throwIfNotFound: true);
        m_TestKey_Test3 = m_TestKey.FindAction("Test3", throwIfNotFound: true);
        m_TestKey_Test4 = m_TestKey.FindAction("Test4", throwIfNotFound: true);
        m_TestKey_Test5 = m_TestKey.FindAction("Test5", throwIfNotFound: true);
        m_TestKey_TestLeftClick = m_TestKey.FindAction("TestLeftClick", throwIfNotFound: true);
        m_TestKey_TestRightClick = m_TestKey.FindAction("TestRightClick", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BattleMap
    private readonly InputActionMap m_BattleMap;
    private List<IBattleMapActions> m_BattleMapActionsCallbackInterfaces = new List<IBattleMapActions>();
    private readonly InputAction m_BattleMap_MouseLeftClick;
    private readonly InputAction m_BattleMap_MouseRightClick;
    private readonly InputAction m_BattleMap_LeftArrowKey;
    private readonly InputAction m_BattleMap_RightArrowKey;
    private readonly InputAction m_BattleMap_UpArrowKey;
    private readonly InputAction m_BattleMap_DownArrowKey;
    private readonly InputAction m_BattleMap_EnterKey;
    private readonly InputAction m_BattleMap_MouseLeftPress;
    public struct BattleMapActions
    {
        private @MainInputSystem m_Wrapper;
        public BattleMapActions(@MainInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseLeftClick => m_Wrapper.m_BattleMap_MouseLeftClick;
        public InputAction @MouseRightClick => m_Wrapper.m_BattleMap_MouseRightClick;
        public InputAction @LeftArrowKey => m_Wrapper.m_BattleMap_LeftArrowKey;
        public InputAction @RightArrowKey => m_Wrapper.m_BattleMap_RightArrowKey;
        public InputAction @UpArrowKey => m_Wrapper.m_BattleMap_UpArrowKey;
        public InputAction @DownArrowKey => m_Wrapper.m_BattleMap_DownArrowKey;
        public InputAction @EnterKey => m_Wrapper.m_BattleMap_EnterKey;
        public InputAction @MouseLeftPress => m_Wrapper.m_BattleMap_MouseLeftPress;
        public InputActionMap Get() { return m_Wrapper.m_BattleMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleMapActions set) { return set.Get(); }
        public void AddCallbacks(IBattleMapActions instance)
        {
            if (instance == null || m_Wrapper.m_BattleMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BattleMapActionsCallbackInterfaces.Add(instance);
            @MouseLeftClick.started += instance.OnMouseLeftClick;
            @MouseLeftClick.performed += instance.OnMouseLeftClick;
            @MouseLeftClick.canceled += instance.OnMouseLeftClick;
            @MouseRightClick.started += instance.OnMouseRightClick;
            @MouseRightClick.performed += instance.OnMouseRightClick;
            @MouseRightClick.canceled += instance.OnMouseRightClick;
            @LeftArrowKey.started += instance.OnLeftArrowKey;
            @LeftArrowKey.performed += instance.OnLeftArrowKey;
            @LeftArrowKey.canceled += instance.OnLeftArrowKey;
            @RightArrowKey.started += instance.OnRightArrowKey;
            @RightArrowKey.performed += instance.OnRightArrowKey;
            @RightArrowKey.canceled += instance.OnRightArrowKey;
            @UpArrowKey.started += instance.OnUpArrowKey;
            @UpArrowKey.performed += instance.OnUpArrowKey;
            @UpArrowKey.canceled += instance.OnUpArrowKey;
            @DownArrowKey.started += instance.OnDownArrowKey;
            @DownArrowKey.performed += instance.OnDownArrowKey;
            @DownArrowKey.canceled += instance.OnDownArrowKey;
            @EnterKey.started += instance.OnEnterKey;
            @EnterKey.performed += instance.OnEnterKey;
            @EnterKey.canceled += instance.OnEnterKey;
            @MouseLeftPress.started += instance.OnMouseLeftPress;
            @MouseLeftPress.performed += instance.OnMouseLeftPress;
            @MouseLeftPress.canceled += instance.OnMouseLeftPress;
        }

        private void UnregisterCallbacks(IBattleMapActions instance)
        {
            @MouseLeftClick.started -= instance.OnMouseLeftClick;
            @MouseLeftClick.performed -= instance.OnMouseLeftClick;
            @MouseLeftClick.canceled -= instance.OnMouseLeftClick;
            @MouseRightClick.started -= instance.OnMouseRightClick;
            @MouseRightClick.performed -= instance.OnMouseRightClick;
            @MouseRightClick.canceled -= instance.OnMouseRightClick;
            @LeftArrowKey.started -= instance.OnLeftArrowKey;
            @LeftArrowKey.performed -= instance.OnLeftArrowKey;
            @LeftArrowKey.canceled -= instance.OnLeftArrowKey;
            @RightArrowKey.started -= instance.OnRightArrowKey;
            @RightArrowKey.performed -= instance.OnRightArrowKey;
            @RightArrowKey.canceled -= instance.OnRightArrowKey;
            @UpArrowKey.started -= instance.OnUpArrowKey;
            @UpArrowKey.performed -= instance.OnUpArrowKey;
            @UpArrowKey.canceled -= instance.OnUpArrowKey;
            @DownArrowKey.started -= instance.OnDownArrowKey;
            @DownArrowKey.performed -= instance.OnDownArrowKey;
            @DownArrowKey.canceled -= instance.OnDownArrowKey;
            @EnterKey.started -= instance.OnEnterKey;
            @EnterKey.performed -= instance.OnEnterKey;
            @EnterKey.canceled -= instance.OnEnterKey;
            @MouseLeftPress.started -= instance.OnMouseLeftPress;
            @MouseLeftPress.performed -= instance.OnMouseLeftPress;
            @MouseLeftPress.canceled -= instance.OnMouseLeftPress;
        }

        public void RemoveCallbacks(IBattleMapActions instance)
        {
            if (m_Wrapper.m_BattleMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBattleMapActions instance)
        {
            foreach (var item in m_Wrapper.m_BattleMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BattleMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BattleMapActions @BattleMap => new BattleMapActions(this);

    // TestKey
    private readonly InputActionMap m_TestKey;
    private List<ITestKeyActions> m_TestKeyActionsCallbackInterfaces = new List<ITestKeyActions>();
    private readonly InputAction m_TestKey_Test1;
    private readonly InputAction m_TestKey_Test2;
    private readonly InputAction m_TestKey_Test3;
    private readonly InputAction m_TestKey_Test4;
    private readonly InputAction m_TestKey_Test5;
    private readonly InputAction m_TestKey_TestLeftClick;
    private readonly InputAction m_TestKey_TestRightClick;
    public struct TestKeyActions
    {
        private @MainInputSystem m_Wrapper;
        public TestKeyActions(@MainInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test1 => m_Wrapper.m_TestKey_Test1;
        public InputAction @Test2 => m_Wrapper.m_TestKey_Test2;
        public InputAction @Test3 => m_Wrapper.m_TestKey_Test3;
        public InputAction @Test4 => m_Wrapper.m_TestKey_Test4;
        public InputAction @Test5 => m_Wrapper.m_TestKey_Test5;
        public InputAction @TestLeftClick => m_Wrapper.m_TestKey_TestLeftClick;
        public InputAction @TestRightClick => m_Wrapper.m_TestKey_TestRightClick;
        public InputActionMap Get() { return m_Wrapper.m_TestKey; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestKeyActions set) { return set.Get(); }
        public void AddCallbacks(ITestKeyActions instance)
        {
            if (instance == null || m_Wrapper.m_TestKeyActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TestKeyActionsCallbackInterfaces.Add(instance);
            @Test1.started += instance.OnTest1;
            @Test1.performed += instance.OnTest1;
            @Test1.canceled += instance.OnTest1;
            @Test2.started += instance.OnTest2;
            @Test2.performed += instance.OnTest2;
            @Test2.canceled += instance.OnTest2;
            @Test3.started += instance.OnTest3;
            @Test3.performed += instance.OnTest3;
            @Test3.canceled += instance.OnTest3;
            @Test4.started += instance.OnTest4;
            @Test4.performed += instance.OnTest4;
            @Test4.canceled += instance.OnTest4;
            @Test5.started += instance.OnTest5;
            @Test5.performed += instance.OnTest5;
            @Test5.canceled += instance.OnTest5;
            @TestLeftClick.started += instance.OnTestLeftClick;
            @TestLeftClick.performed += instance.OnTestLeftClick;
            @TestLeftClick.canceled += instance.OnTestLeftClick;
            @TestRightClick.started += instance.OnTestRightClick;
            @TestRightClick.performed += instance.OnTestRightClick;
            @TestRightClick.canceled += instance.OnTestRightClick;
        }

        private void UnregisterCallbacks(ITestKeyActions instance)
        {
            @Test1.started -= instance.OnTest1;
            @Test1.performed -= instance.OnTest1;
            @Test1.canceled -= instance.OnTest1;
            @Test2.started -= instance.OnTest2;
            @Test2.performed -= instance.OnTest2;
            @Test2.canceled -= instance.OnTest2;
            @Test3.started -= instance.OnTest3;
            @Test3.performed -= instance.OnTest3;
            @Test3.canceled -= instance.OnTest3;
            @Test4.started -= instance.OnTest4;
            @Test4.performed -= instance.OnTest4;
            @Test4.canceled -= instance.OnTest4;
            @Test5.started -= instance.OnTest5;
            @Test5.performed -= instance.OnTest5;
            @Test5.canceled -= instance.OnTest5;
            @TestLeftClick.started -= instance.OnTestLeftClick;
            @TestLeftClick.performed -= instance.OnTestLeftClick;
            @TestLeftClick.canceled -= instance.OnTestLeftClick;
            @TestRightClick.started -= instance.OnTestRightClick;
            @TestRightClick.performed -= instance.OnTestRightClick;
            @TestRightClick.canceled -= instance.OnTestRightClick;
        }

        public void RemoveCallbacks(ITestKeyActions instance)
        {
            if (m_Wrapper.m_TestKeyActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITestKeyActions instance)
        {
            foreach (var item in m_Wrapper.m_TestKeyActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TestKeyActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TestKeyActions @TestKey => new TestKeyActions(this);
    private int m_KeyBoardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyBoardAndMouseScheme
    {
        get
        {
            if (m_KeyBoardAndMouseSchemeIndex == -1) m_KeyBoardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoardAndMouse");
            return asset.controlSchemes[m_KeyBoardAndMouseSchemeIndex];
        }
    }
    public interface IBattleMapActions
    {
        void OnMouseLeftClick(InputAction.CallbackContext context);
        void OnMouseRightClick(InputAction.CallbackContext context);
        void OnLeftArrowKey(InputAction.CallbackContext context);
        void OnRightArrowKey(InputAction.CallbackContext context);
        void OnUpArrowKey(InputAction.CallbackContext context);
        void OnDownArrowKey(InputAction.CallbackContext context);
        void OnEnterKey(InputAction.CallbackContext context);
        void OnMouseLeftPress(InputAction.CallbackContext context);
    }
    public interface ITestKeyActions
    {
        void OnTest1(InputAction.CallbackContext context);
        void OnTest2(InputAction.CallbackContext context);
        void OnTest3(InputAction.CallbackContext context);
        void OnTest4(InputAction.CallbackContext context);
        void OnTest5(InputAction.CallbackContext context);
        void OnTestLeftClick(InputAction.CallbackContext context);
        void OnTestRightClick(InputAction.CallbackContext context);
    }
}
