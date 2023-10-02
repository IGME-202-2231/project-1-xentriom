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
}
