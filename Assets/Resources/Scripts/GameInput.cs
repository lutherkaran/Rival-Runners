using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    PlayerInputAction inputActions;

    public bool jump { get; private set; }
    public Vector2 move { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        MoveInput();
        JumpInput();
    }

    private void JumpInput()
    {
        if (inputActions.Player.Jump.triggered)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

    }

    private void MoveInput()
    {
        move = inputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool Started()
    {
        return inputActions.Player.Start.IsPressed();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
