//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Test.inputactions
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

public partial class @Test: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Test()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Test"",
    ""maps"": [
        {
            ""name"": ""Cube"",
            ""id"": ""1d672a49-35de-46ce-a9c8-02cb05c37fdb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""77739af1-0456-4cce-ad09-dfec71b7b76b"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""81760227-cd9b-480d-9b89-71a156ae8e98"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""33dcde2d-7523-41df-826d-6c6fc808df7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.5)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""2d355fe3-ad60-478a-bf33-4ddfc7c03568"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Join"",
                    ""type"": ""PassThrough"",
                    ""id"": ""00247639-f049-4f3c-8cf3-d2d10fc70dde"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b47c226e-6e8e-4585-b096-9ba1ca31768d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""9b4943a2-433f-4fad-8356-55eaf4c8115b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""d88b1745-82bd-4e81-b0bb-5c2511900197"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Close"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32805f3b-ac59-4983-b40d-e808d97365d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Parachute"",
                    ""type"": ""Button"",
                    ""id"": ""6392f651-a767-448c-b3c6-077693a07889"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""62f011b6-93a5-48ac-93a1-4cf596341c66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Trigger"",
                    ""type"": ""Value"",
                    ""id"": ""ba1e3798-cf6b-4047-84b5-a42ce55d0b2c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Push"",
                    ""type"": ""PassThrough"",
                    ""id"": ""22aaf8b0-e4e2-402d-998d-1b5e4540a246"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9c0196da-8454-4a22-a742-60d385f0b95e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3b08e10-c782-4876-834c-b61d78f24597"",
                    ""path"": ""<HID::Core (Plus) Wired Controller>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""346da98c-08d2-4a3d-bea7-b847a2ee6b9e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b55183e7-87d4-462c-a586-4c79af8ebff4"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""New control scheme"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a71b887-4df0-47cd-a5d8-e1027f43db85"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85acf37c-a9f9-4631-9ce0-21ff4e3e16f6"",
                    ""path"": ""<HID::Core (Plus) Wired Controller>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Join"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa92c473-4441-4bd7-b53c-a645186eedef"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7870e354-0195-45a8-882e-a52e8aaf0974"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c0c4460-4d11-4cce-aa1c-2e6bda97c054"",
                    ""path"": ""<XInputController>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6ac49ebd-510c-4312-83d6-96463d9c79b2"",
                    ""path"": ""<XInputController>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8e6819d3-0911-4faf-88d7-f8563ddcc8de"",
                    ""path"": ""<XInputController>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a62cc56a-7399-47c9-a247-dec6876f1925"",
                    ""path"": ""<XInputController>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""e24c6e81-ebd8-4d37-9fa8-7bd522bddfdf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c918ba8-9069-4151-8236-84ffc715f205"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4aaceaff-a027-4bd6-a699-33cf40f3f256"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ca5484d3-4c5a-460d-8761-706756539210"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c3bf985f-54a4-42ae-a78d-faf96e8a6411"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2e67206c-4f09-4228-b3c4-a2bcd4c59177"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1096b6df-2246-492e-abdb-321235d90a8d"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2dde65e9-d3d7-4eb7-8bc2-f9a300fdd09d"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Close"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53486f20-0fd5-4306-8baf-9d351e6eb3bf"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Parachute"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea75ddb4-04ed-492e-92ab-ccf62f1293f7"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7455457e-7737-4616-a1ac-19437d1d780a"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2dfd4d58-4e0b-492f-b30c-989042508f46"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""21b07403-8461-464c-9f17-1deabea60ff4"",
            ""actions"": [
                {
                    ""name"": ""StartDialogue"",
                    ""type"": ""PassThrough"",
                    ""id"": ""544f32f3-61fc-4011-93e5-e6162f9e0eb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ContinueDialogue"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6511a039-7d0c-4b7b-8e79-4a8e8c5ac2f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8bb0b0aa-18a6-4d82-9678-91ccfae7a3c5"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78871d29-22f3-4413-be37-36b43278049a"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ContinueDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PauseControls"",
            ""id"": ""7f17af04-f74f-440c-b4b1-7e67d1df88c5"",
            ""actions"": [
                {
                    ""name"": ""Pause/Unpause"",
                    ""type"": ""Button"",
                    ""id"": ""86179dbf-23c6-4418-8dd4-2507af6cba15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu Joystick"",
                    ""type"": ""Value"",
                    ""id"": ""c3253ffc-75f4-4660-a25f-9090ed840471"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SelectOption"",
                    ""type"": ""Button"",
                    ""id"": ""542f070a-77f5-4094-bb6f-784f74e1fbbf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5cf1176c-1531-4fc2-a654-150e52853275"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause/Unpause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ca59905-6196-498b-8bb7-f69f754e9291"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43cce7e5-57b1-48cf-bbf1-c331debdddd1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectOption"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Cube
        m_Cube = asset.FindActionMap("Cube", throwIfNotFound: true);
        m_Cube_Move = m_Cube.FindAction("Move", throwIfNotFound: true);
        m_Cube_CameraLook = m_Cube.FindAction("CameraLook", throwIfNotFound: true);
        m_Cube_MoveUp = m_Cube.FindAction("MoveUp", throwIfNotFound: true);
        m_Cube_MoveDown = m_Cube.FindAction("MoveDown", throwIfNotFound: true);
        m_Cube_Join = m_Cube.FindAction("Join", throwIfNotFound: true);
        m_Cube_Pick = m_Cube.FindAction("Pick", throwIfNotFound: true);
        m_Cube_Jump = m_Cube.FindAction("Jump", throwIfNotFound: true);
        m_Cube_Run = m_Cube.FindAction("Run", throwIfNotFound: true);
        m_Cube_Close = m_Cube.FindAction("Close", throwIfNotFound: true);
        m_Cube_Parachute = m_Cube.FindAction("Parachute", throwIfNotFound: true);
        m_Cube_Aim = m_Cube.FindAction("Aim", throwIfNotFound: true);
        m_Cube_Trigger = m_Cube.FindAction("Trigger", throwIfNotFound: true);
        m_Cube_Push = m_Cube.FindAction("Push", throwIfNotFound: true);
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_StartDialogue = m_Dialogue.FindAction("StartDialogue", throwIfNotFound: true);
        m_Dialogue_ContinueDialogue = m_Dialogue.FindAction("ContinueDialogue", throwIfNotFound: true);
        // PauseControls
        m_PauseControls = asset.FindActionMap("PauseControls", throwIfNotFound: true);
        m_PauseControls_PauseUnpause = m_PauseControls.FindAction("Pause/Unpause", throwIfNotFound: true);
        m_PauseControls_MenuJoystick = m_PauseControls.FindAction("Menu Joystick", throwIfNotFound: true);
        m_PauseControls_SelectOption = m_PauseControls.FindAction("SelectOption", throwIfNotFound: true);
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

    // Cube
    private readonly InputActionMap m_Cube;
    private List<ICubeActions> m_CubeActionsCallbackInterfaces = new List<ICubeActions>();
    private readonly InputAction m_Cube_Move;
    private readonly InputAction m_Cube_CameraLook;
    private readonly InputAction m_Cube_MoveUp;
    private readonly InputAction m_Cube_MoveDown;
    private readonly InputAction m_Cube_Join;
    private readonly InputAction m_Cube_Pick;
    private readonly InputAction m_Cube_Jump;
    private readonly InputAction m_Cube_Run;
    private readonly InputAction m_Cube_Close;
    private readonly InputAction m_Cube_Parachute;
    private readonly InputAction m_Cube_Aim;
    private readonly InputAction m_Cube_Trigger;
    private readonly InputAction m_Cube_Push;
    public struct CubeActions
    {
        private @Test m_Wrapper;
        public CubeActions(@Test wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Cube_Move;
        public InputAction @CameraLook => m_Wrapper.m_Cube_CameraLook;
        public InputAction @MoveUp => m_Wrapper.m_Cube_MoveUp;
        public InputAction @MoveDown => m_Wrapper.m_Cube_MoveDown;
        public InputAction @Join => m_Wrapper.m_Cube_Join;
        public InputAction @Pick => m_Wrapper.m_Cube_Pick;
        public InputAction @Jump => m_Wrapper.m_Cube_Jump;
        public InputAction @Run => m_Wrapper.m_Cube_Run;
        public InputAction @Close => m_Wrapper.m_Cube_Close;
        public InputAction @Parachute => m_Wrapper.m_Cube_Parachute;
        public InputAction @Aim => m_Wrapper.m_Cube_Aim;
        public InputAction @Trigger => m_Wrapper.m_Cube_Trigger;
        public InputAction @Push => m_Wrapper.m_Cube_Push;
        public InputActionMap Get() { return m_Wrapper.m_Cube; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CubeActions set) { return set.Get(); }
        public void AddCallbacks(ICubeActions instance)
        {
            if (instance == null || m_Wrapper.m_CubeActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CubeActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @CameraLook.started += instance.OnCameraLook;
            @CameraLook.performed += instance.OnCameraLook;
            @CameraLook.canceled += instance.OnCameraLook;
            @MoveUp.started += instance.OnMoveUp;
            @MoveUp.performed += instance.OnMoveUp;
            @MoveUp.canceled += instance.OnMoveUp;
            @MoveDown.started += instance.OnMoveDown;
            @MoveDown.performed += instance.OnMoveDown;
            @MoveDown.canceled += instance.OnMoveDown;
            @Join.started += instance.OnJoin;
            @Join.performed += instance.OnJoin;
            @Join.canceled += instance.OnJoin;
            @Pick.started += instance.OnPick;
            @Pick.performed += instance.OnPick;
            @Pick.canceled += instance.OnPick;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Close.started += instance.OnClose;
            @Close.performed += instance.OnClose;
            @Close.canceled += instance.OnClose;
            @Parachute.started += instance.OnParachute;
            @Parachute.performed += instance.OnParachute;
            @Parachute.canceled += instance.OnParachute;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @Trigger.started += instance.OnTrigger;
            @Trigger.performed += instance.OnTrigger;
            @Trigger.canceled += instance.OnTrigger;
            @Push.started += instance.OnPush;
            @Push.performed += instance.OnPush;
            @Push.canceled += instance.OnPush;
        }

        private void UnregisterCallbacks(ICubeActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @CameraLook.started -= instance.OnCameraLook;
            @CameraLook.performed -= instance.OnCameraLook;
            @CameraLook.canceled -= instance.OnCameraLook;
            @MoveUp.started -= instance.OnMoveUp;
            @MoveUp.performed -= instance.OnMoveUp;
            @MoveUp.canceled -= instance.OnMoveUp;
            @MoveDown.started -= instance.OnMoveDown;
            @MoveDown.performed -= instance.OnMoveDown;
            @MoveDown.canceled -= instance.OnMoveDown;
            @Join.started -= instance.OnJoin;
            @Join.performed -= instance.OnJoin;
            @Join.canceled -= instance.OnJoin;
            @Pick.started -= instance.OnPick;
            @Pick.performed -= instance.OnPick;
            @Pick.canceled -= instance.OnPick;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Close.started -= instance.OnClose;
            @Close.performed -= instance.OnClose;
            @Close.canceled -= instance.OnClose;
            @Parachute.started -= instance.OnParachute;
            @Parachute.performed -= instance.OnParachute;
            @Parachute.canceled -= instance.OnParachute;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @Trigger.started -= instance.OnTrigger;
            @Trigger.performed -= instance.OnTrigger;
            @Trigger.canceled -= instance.OnTrigger;
            @Push.started -= instance.OnPush;
            @Push.performed -= instance.OnPush;
            @Push.canceled -= instance.OnPush;
        }

        public void RemoveCallbacks(ICubeActions instance)
        {
            if (m_Wrapper.m_CubeActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICubeActions instance)
        {
            foreach (var item in m_Wrapper.m_CubeActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CubeActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CubeActions @Cube => new CubeActions(this);

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private List<IDialogueActions> m_DialogueActionsCallbackInterfaces = new List<IDialogueActions>();
    private readonly InputAction m_Dialogue_StartDialogue;
    private readonly InputAction m_Dialogue_ContinueDialogue;
    public struct DialogueActions
    {
        private @Test m_Wrapper;
        public DialogueActions(@Test wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartDialogue => m_Wrapper.m_Dialogue_StartDialogue;
        public InputAction @ContinueDialogue => m_Wrapper.m_Dialogue_ContinueDialogue;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void AddCallbacks(IDialogueActions instance)
        {
            if (instance == null || m_Wrapper.m_DialogueActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DialogueActionsCallbackInterfaces.Add(instance);
            @StartDialogue.started += instance.OnStartDialogue;
            @StartDialogue.performed += instance.OnStartDialogue;
            @StartDialogue.canceled += instance.OnStartDialogue;
            @ContinueDialogue.started += instance.OnContinueDialogue;
            @ContinueDialogue.performed += instance.OnContinueDialogue;
            @ContinueDialogue.canceled += instance.OnContinueDialogue;
        }

        private void UnregisterCallbacks(IDialogueActions instance)
        {
            @StartDialogue.started -= instance.OnStartDialogue;
            @StartDialogue.performed -= instance.OnStartDialogue;
            @StartDialogue.canceled -= instance.OnStartDialogue;
            @ContinueDialogue.started -= instance.OnContinueDialogue;
            @ContinueDialogue.performed -= instance.OnContinueDialogue;
            @ContinueDialogue.canceled -= instance.OnContinueDialogue;
        }

        public void RemoveCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDialogueActions instance)
        {
            foreach (var item in m_Wrapper.m_DialogueActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DialogueActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);

    // PauseControls
    private readonly InputActionMap m_PauseControls;
    private List<IPauseControlsActions> m_PauseControlsActionsCallbackInterfaces = new List<IPauseControlsActions>();
    private readonly InputAction m_PauseControls_PauseUnpause;
    private readonly InputAction m_PauseControls_MenuJoystick;
    private readonly InputAction m_PauseControls_SelectOption;
    public struct PauseControlsActions
    {
        private @Test m_Wrapper;
        public PauseControlsActions(@Test wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseUnpause => m_Wrapper.m_PauseControls_PauseUnpause;
        public InputAction @MenuJoystick => m_Wrapper.m_PauseControls_MenuJoystick;
        public InputAction @SelectOption => m_Wrapper.m_PauseControls_SelectOption;
        public InputActionMap Get() { return m_Wrapper.m_PauseControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseControlsActions set) { return set.Get(); }
        public void AddCallbacks(IPauseControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_PauseControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PauseControlsActionsCallbackInterfaces.Add(instance);
            @PauseUnpause.started += instance.OnPauseUnpause;
            @PauseUnpause.performed += instance.OnPauseUnpause;
            @PauseUnpause.canceled += instance.OnPauseUnpause;
            @MenuJoystick.started += instance.OnMenuJoystick;
            @MenuJoystick.performed += instance.OnMenuJoystick;
            @MenuJoystick.canceled += instance.OnMenuJoystick;
            @SelectOption.started += instance.OnSelectOption;
            @SelectOption.performed += instance.OnSelectOption;
            @SelectOption.canceled += instance.OnSelectOption;
        }

        private void UnregisterCallbacks(IPauseControlsActions instance)
        {
            @PauseUnpause.started -= instance.OnPauseUnpause;
            @PauseUnpause.performed -= instance.OnPauseUnpause;
            @PauseUnpause.canceled -= instance.OnPauseUnpause;
            @MenuJoystick.started -= instance.OnMenuJoystick;
            @MenuJoystick.performed -= instance.OnMenuJoystick;
            @MenuJoystick.canceled -= instance.OnMenuJoystick;
            @SelectOption.started -= instance.OnSelectOption;
            @SelectOption.performed -= instance.OnSelectOption;
            @SelectOption.canceled -= instance.OnSelectOption;
        }

        public void RemoveCallbacks(IPauseControlsActions instance)
        {
            if (m_Wrapper.m_PauseControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPauseControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_PauseControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PauseControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PauseControlsActions @PauseControls => new PauseControlsActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface ICubeActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCameraLook(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnJoin(InputAction.CallbackContext context);
        void OnPick(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnClose(InputAction.CallbackContext context);
        void OnParachute(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnTrigger(InputAction.CallbackContext context);
        void OnPush(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnStartDialogue(InputAction.CallbackContext context);
        void OnContinueDialogue(InputAction.CallbackContext context);
    }
    public interface IPauseControlsActions
    {
        void OnPauseUnpause(InputAction.CallbackContext context);
        void OnMenuJoystick(InputAction.CallbackContext context);
        void OnSelectOption(InputAction.CallbackContext context);
    }
}
