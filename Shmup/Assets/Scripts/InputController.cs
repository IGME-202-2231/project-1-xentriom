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
    [SerializeField] Monster_Fly spawn;

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
                attack.Swing();
            }
            else if (Mouse.current.rightButton.isPressed)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                mousePosition = mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                attack.Throw(mousePosition);
            }
        };
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        movement.Jump();
    }

    public void OnSheild(InputAction.CallbackContext context)
    {
        Debug.Log("Sheild");
        spawn.Spawn();
    }
}
