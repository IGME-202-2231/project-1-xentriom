using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Variable field
    [SerializeField] MovementController controller;

    public void OnMove(InputAction.CallbackContext context)
    {
        controller.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Debug.Log("Left Mouse Click");
            }
            else if (Mouse.current.rightButton.isPressed)
            {
                Debug.Log("Right Mouse Click");
            }
        };
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
    }

    public void OnSheild(InputAction.CallbackContext context)
    {
        Debug.Log("Sheild");
    }
}
