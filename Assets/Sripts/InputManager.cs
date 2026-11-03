using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set; }

    public event EventHandler OnJumpButtonPressed;


    private InputSystem_Actions input;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        input = new InputSystem_Actions();
        input.Enable();
    }

    private void OnEnable()
    {
        input.Player.Jump.performed += JumpButtonPressed;
    }

    private void OnDisable()
    {
        input.Player.Jump.performed -= JumpButtonPressed;
    }

    public Vector2 GetMovementInput ()
    {
        Vector2 inputVector = new();
        return inputVector = input.Player.Move.ReadValue<Vector2>();
    }

    public bool SprintKeyIsPressed()
    {        
        return input.Player.Sprint.IsPressed();
    }

    public void JumpButtonPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnJumpButtonPressed?.Invoke(this, EventArgs.Empty);
    }

}
