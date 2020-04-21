using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace LD46
{
    public class CharacterInput : MonoBehaviour
    {
        public enum InputType
        {
            None,
            Keyboard,
            Controller,
        }
        
        public InputType inputType = InputType.None;
        
        private InputControl _input;
        
        public void Awake()
        {
            InputSystem.RegisterInteraction<KeepHoldInteraction>();
            
            _input = new InputControl();

            _input.KeyboardCharacterControl.Interact.started += context =>
            {
                if (inputType == InputType.Keyboard)
                {
                    if (context.interaction is KeepHoldInteraction)
                    {
                        InputHold(true);
                    }
                }
            };

            _input.KeyboardCharacterControl.Interact.performed += context =>
            {
                if (inputType == InputType.Keyboard)
                {
                    if (context.interaction is TapInteraction)
                    {
                        InputTap();
                    }
                    else if (context.interaction is KeepHoldInteraction)
                    {
                        InputHold(false);
                    }
                }
            };
        
            _input.ControllerCharacterControl.Interact.started += context =>
            {
                if (inputType == InputType.Controller)
                {
                    if (context.interaction is KeepHoldInteraction)
                    {
                        InputHold(true);
                    }
                }
            };

            _input.ControllerCharacterControl.Interact.performed += context =>
            {
                if (inputType == InputType.Controller)
                {
                    if (context.interaction is TapInteraction)
                    {
                        InputTap();
                    }
                    else if (context.interaction is KeepHoldInteraction)
                    {
                        InputHold(false);
                    }
                }
            };
        }
        
        public void OnEnable()
        {
            _input.Enable();
        }

        public void OnDisable()
        {
            _input.Disable();
        }

        public Vector2 GetMoveInput()
        {
            switch (inputType)
            {
                case InputType.Keyboard:
                    return _input.KeyboardCharacterControl.Move.ReadValue<Vector2>();
                case InputType.Controller:
                    return _input.ControllerCharacterControl.Move.ReadValue<Vector2>();
                default:
                    return Vector2.zero;
            }
        }

        public bool GetRunning()
        {
            return false;
        }
        
        private void InputTap()
        {
            MessageSystem.SendMessage(MessageType.InteractTap, gameObject);
        }

        private void InputHold(bool holding)
        {
            MessageSystem.SendMessage(MessageType.InteractHold, new MessageDataHold
            {
                Holding = holding, Player = gameObject
            });
        }
    }
}
