using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;
    // private variable uses underScore before name
    // public variable uses no underScore before name

    public float Horizontal;
    public bool Jump;
    public bool Jumped;
    public bool Attack;
    public bool Interact;

    private void Update()
    {
        Horizontal = 
            _inputSystem.Player.Move.
                ReadValue<Vector2>().x;
        Jump = 
            _inputSystem.Player.Jump.
                WasPressedThisFrame();
        Jumped =
            _inputSystem.Player.Jump.
                WasReleasedThisFrame();
        Attack = 
            _inputSystem.Player.Attack.
                WasPressedThisFrame();
        Interact = 
            _inputSystem.Player.Interact.
                WasPressedThisFrame();
    }

    private void Awake() { _inputSystem = new InputSystem_Actions(); }

    private void OnEnable() { _inputSystem.Enable(); }

    private void OnDisable() { _inputSystem.Disable(); }
}
