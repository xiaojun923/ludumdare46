// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/InputControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace LD46
{
    public class @InputControl : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControl"",
    ""maps"": [
        {
            ""name"": ""KeyboardCharacterControl"",
            ""id"": ""e6c81145-544d-4983-b943-1637eeedbae3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e725ca2f-fd8f-4806-8b7e-f5eeb6d83f5a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""1d531d1c-47a4-45b2-a4b3-770b169814db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,KeepHold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""59c30ef7-8a22-4835-8576-b368d72dcf63"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""64c91f27-56d2-49e8-b13a-fc4598e6d611"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""68036a90-a7e8-45e0-ae50-c0079d1ee2ff"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""11e89e27-7d97-4c82-813a-17cd89825f88"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""da269a56-714c-47da-acce-bf996e11fcde"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2feed1f1-7528-4a55-9de9-d1b9e9a26b95"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e49bd21e-9b9a-450d-b2d5-ac2cc0d7048d"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ControllerCharacterControl"",
            ""id"": ""8105da90-6ed6-4e1d-92f4-ea8fc55378ab"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""48a3ceb3-96f5-423b-b9b3-1e9f185701d2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c5257de5-518e-4371-874c-368e5f59a116"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap,KeepHold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f56fb9e0-3565-40b9-a6a0-df03e9736cca"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Dpad"",
                    ""id"": ""1cbac833-54f4-4c76-969a-f8c5611f9563"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""96489e0e-5902-456d-be0f-7ad8e9a230b6"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eb7b68f8-1441-4751-8204-02620051c651"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4cb9a6a6-3f8e-4339-a42f-7b7e13250991"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d3c043ca-6a72-4d9a-bb08-754fa81d3a92"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e521c554-9285-46ac-af7d-863ca9428740"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // KeyboardCharacterControl
            m_KeyboardCharacterControl = asset.FindActionMap("KeyboardCharacterControl", throwIfNotFound: true);
            m_KeyboardCharacterControl_Move = m_KeyboardCharacterControl.FindAction("Move", throwIfNotFound: true);
            m_KeyboardCharacterControl_Interact = m_KeyboardCharacterControl.FindAction("Interact", throwIfNotFound: true);
            // ControllerCharacterControl
            m_ControllerCharacterControl = asset.FindActionMap("ControllerCharacterControl", throwIfNotFound: true);
            m_ControllerCharacterControl_Move = m_ControllerCharacterControl.FindAction("Move", throwIfNotFound: true);
            m_ControllerCharacterControl_Interact = m_ControllerCharacterControl.FindAction("Interact", throwIfNotFound: true);
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

        // KeyboardCharacterControl
        private readonly InputActionMap m_KeyboardCharacterControl;
        private IKeyboardCharacterControlActions m_KeyboardCharacterControlActionsCallbackInterface;
        private readonly InputAction m_KeyboardCharacterControl_Move;
        private readonly InputAction m_KeyboardCharacterControl_Interact;
        public struct KeyboardCharacterControlActions
        {
            private @InputControl m_Wrapper;
            public KeyboardCharacterControlActions(@InputControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_KeyboardCharacterControl_Move;
            public InputAction @Interact => m_Wrapper.m_KeyboardCharacterControl_Interact;
            public InputActionMap Get() { return m_Wrapper.m_KeyboardCharacterControl; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(KeyboardCharacterControlActions set) { return set.Get(); }
            public void SetCallbacks(IKeyboardCharacterControlActions instance)
            {
                if (m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface.OnMove;
                    @Interact.started -= m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface.OnInteract;
                }
                m_Wrapper.m_KeyboardCharacterControlActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                }
            }
        }
        public KeyboardCharacterControlActions @KeyboardCharacterControl => new KeyboardCharacterControlActions(this);

        // ControllerCharacterControl
        private readonly InputActionMap m_ControllerCharacterControl;
        private IControllerCharacterControlActions m_ControllerCharacterControlActionsCallbackInterface;
        private readonly InputAction m_ControllerCharacterControl_Move;
        private readonly InputAction m_ControllerCharacterControl_Interact;
        public struct ControllerCharacterControlActions
        {
            private @InputControl m_Wrapper;
            public ControllerCharacterControlActions(@InputControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_ControllerCharacterControl_Move;
            public InputAction @Interact => m_Wrapper.m_ControllerCharacterControl_Interact;
            public InputActionMap Get() { return m_Wrapper.m_ControllerCharacterControl; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ControllerCharacterControlActions set) { return set.Get(); }
            public void SetCallbacks(IControllerCharacterControlActions instance)
            {
                if (m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface.OnMove;
                    @Interact.started -= m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface.OnInteract;
                }
                m_Wrapper.m_ControllerCharacterControlActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                }
            }
        }
        public ControllerCharacterControlActions @ControllerCharacterControl => new ControllerCharacterControlActions(this);
        public interface IKeyboardCharacterControlActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
        }
        public interface IControllerCharacterControlActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
        }
    }
}
