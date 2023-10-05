using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Variable field
    [SerializeField] MovementController movement;
    [SerializeField] AttackController attack;

    public void OnMove(InputAction.CallbackContext context)
    {
        movement.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            if (Mouse.current.leftButton.isPressed)
            {
                attack.SwingSword();
            }
            else if (Mouse.current.rightButton.isPressed)
            {
                attack.FireGun();
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
