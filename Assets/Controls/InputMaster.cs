// GENERATED AUTOMATICALLY FROM 'Assets/Controls/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Theremin"",
            ""id"": ""55e34f2b-8b74-4a26-b937-b654ee9c8c46"",
            ""actions"": [
                {
                    ""name"": ""Play"",
                    ""type"": ""Button"",
                    ""id"": ""abc8de34-35eb-4e23-a307-ccff59750eb4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""c195c633-a52f-4ae9-9aad-b7782bdd780f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c6c34254-6a38-4951-b466-a9221e929f6a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press,Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Play"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cccf49de-0df8-4849-b559-ade9bc889cff"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
        // Theremin
        m_Theremin = asset.FindActionMap("Theremin", throwIfNotFound: true);
        m_Theremin_Play = m_Theremin.FindAction("Play", throwIfNotFound: true);
        m_Theremin_MousePosition = m_Theremin.FindAction("MousePosition", throwIfNotFound: true);
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

    // Theremin
    private readonly InputActionMap m_Theremin;
    private IThereminActions m_ThereminActionsCallbackInterface;
    private readonly InputAction m_Theremin_Play;
    private readonly InputAction m_Theremin_MousePosition;
    public struct ThereminActions
    {
        private @InputMaster m_Wrapper;
        public ThereminActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Play => m_Wrapper.m_Theremin_Play;
        public InputAction @MousePosition => m_Wrapper.m_Theremin_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Theremin; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ThereminActions set) { return set.Get(); }
        public void SetCallbacks(IThereminActions instance)
        {
            if (m_Wrapper.m_ThereminActionsCallbackInterface != null)
            {
                @Play.started -= m_Wrapper.m_ThereminActionsCallbackInterface.OnPlay;
                @Play.performed -= m_Wrapper.m_ThereminActionsCallbackInterface.OnPlay;
                @Play.canceled -= m_Wrapper.m_ThereminActionsCallbackInterface.OnPlay;
                @MousePosition.started -= m_Wrapper.m_ThereminActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_ThereminActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_ThereminActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_ThereminActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Play.started += instance.OnPlay;
                @Play.performed += instance.OnPlay;
                @Play.canceled += instance.OnPlay;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public ThereminActions @Theremin => new ThereminActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IThereminActions
    {
        void OnPlay(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
