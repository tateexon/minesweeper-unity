//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Inputs/ClickActions.inputactions
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

public partial class @ClickActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ClickActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ClickActions"",
    ""maps"": [
        {
            ""name"": ""ClickAction"",
            ""id"": ""4010bc90-4f8e-42a8-8c95-0f594492702a"",
            ""actions"": [
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""404be389-ab43-42ca-a0f8-7343919b10c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""a7c0e5e7-b5b6-4eaf-bf51-379730b64be7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1dbdc3bd-296a-4e74-b5ea-46955f95de8f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68c5eefb-263d-4420-9f68-6795b134176f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ClickAction
        m_ClickAction = asset.FindActionMap("ClickAction", throwIfNotFound: true);
        m_ClickAction_RightClick = m_ClickAction.FindAction("RightClick", throwIfNotFound: true);
        m_ClickAction_LeftClick = m_ClickAction.FindAction("LeftClick", throwIfNotFound: true);
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

    // ClickAction
    private readonly InputActionMap m_ClickAction;
    private IClickActionActions m_ClickActionActionsCallbackInterface;
    private readonly InputAction m_ClickAction_RightClick;
    private readonly InputAction m_ClickAction_LeftClick;
    public struct ClickActionActions
    {
        private @ClickActions m_Wrapper;
        public ClickActionActions(@ClickActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightClick => m_Wrapper.m_ClickAction_RightClick;
        public InputAction @LeftClick => m_Wrapper.m_ClickAction_LeftClick;
        public InputActionMap Get() { return m_Wrapper.m_ClickAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ClickActionActions set) { return set.Get(); }
        public void SetCallbacks(IClickActionActions instance)
        {
            if (m_Wrapper.m_ClickActionActionsCallbackInterface != null)
            {
                @RightClick.started -= m_Wrapper.m_ClickActionActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_ClickActionActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_ClickActionActionsCallbackInterface.OnRightClick;
                @LeftClick.started -= m_Wrapper.m_ClickActionActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_ClickActionActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_ClickActionActionsCallbackInterface.OnLeftClick;
            }
            m_Wrapper.m_ClickActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
            }
        }
    }
    public ClickActionActions @ClickAction => new ClickActionActions(this);
    public interface IClickActionActions
    {
        void OnRightClick(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
    }
}
