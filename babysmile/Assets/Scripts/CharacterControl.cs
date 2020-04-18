using System;
using System.Collections;
using System.Collections.Generic;
using LD46;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public enum InputType
    {
        None,
        Keyboard,
        Controller,
    }

    public InputType inputType = InputType.None;
    
    private InputControl _input;

    #region MonoEvents

    public void Awake()
    {
        _input = new InputControl();

        _input.KeyboardCharacterControl.Interact.performed += context =>
        {
            if (inputType == InputType.Keyboard)
            {
                CharacterInteract();                
            }
        };

        _input.ControllerCharacterControl.Interact.performed += context =>
        {
            if (inputType == InputType.Controller)
            {
                CharacterInteract();                
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

    public void Update()
    {
        switch (inputType)
        {
            case InputType.Keyboard:
            {
                var moveInput = _input.KeyboardCharacterControl.Move.ReadValue<Vector2>();
                CharacterMove(moveInput);
                break;
            }
            case InputType.Controller:
            {
                var moveInput = _input.ControllerCharacterControl.Move.ReadValue<Vector2>();
                CharacterMove(moveInput);
                break;
            }
        }
        
    }

    #endregion

    public CharacterTriggerInteract componentInteract;

    public float moveSpeed;
    public float rotateSpeed;

    private void CharacterMove(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {
            return;
        }
        
        float da = -Vector2.SignedAngle(Vector2.up, direction);
        var targetRotation = Quaternion.AngleAxis(da - 135f, Vector3.up);

        var t = transform;
        t.rotation = Quaternion.RotateTowards(t.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        t.position += moveSpeed * Time.deltaTime * t.forward;

        if (componentInteract != null)
        {
            componentInteract.UpdateActiveItem(gameObject);
        }
    }


    private void CharacterInteract()
    {
        if (componentInteract != null)
        {
            componentInteract.FireActiveItemInteraction();
        }
    }
}
