using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

    [Header("Movement Axis")]
    public float gravity = 3f;
    public float sensitivity = 3f;

    private InputMaster controls;

    Vector2 directionalInput;
    Vector2 mouseInput;

    private Vector2 currentInput;

    public event EventHandler OnJumpDown;
    public event EventHandler OnJumpUp;


    public event EventHandler OnPausedDown; 
    public event EventHandler OnPausedUp; 


    private void Awake() {
        controls = new InputMaster();

        controls.Player.Jump.started += context => { OnJumpInputDown(); };
		controls.Player.Jump.canceled += context => { OnJumpInputUp(); };

        controls.Player.Pause.started += context => { OnPauseInputDown(); };
        controls.Player.Pause.canceled += context => { OnPauseInputUp(); };
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    void Start() {
        player = GetComponent<Player>();
    }

    void Update() {
        HandleDirectionalInput();
        HandleMouseInput();
    }

    private void HandleDirectionalInput() {
        // Read user input
        directionalInput = controls.Player.Movement.ReadValue<Vector2>();

        if (!player.movementEnabled) {
            directionalInput = Vector2.zero;
        }

        // Input dampening
        if (directionalInput.x != 0) {
            currentInput.x = Mathf.Lerp(currentInput.x, directionalInput.x, sensitivity * Time.deltaTime);
        } else {
            currentInput.x = Mathf.Lerp(currentInput.x, directionalInput.x, (gravity * player.GetDrag()) * Time.deltaTime);
        }
        if (directionalInput.y != 0) {
            currentInput.y = Mathf.Lerp(currentInput.y, directionalInput.y, sensitivity * Time.deltaTime);
        } else {
            currentInput.y = Mathf.Lerp(currentInput.y, directionalInput.y, (gravity * player.GetDrag()) * Time.deltaTime);
        }

        // Set user input
        player.SetDirectionalInput(currentInput);
    }

    private void HandleMouseInput() {
        mouseInput = controls.Player.Look.ReadValue<Vector2>();

        if (!player.mouseLookEnabled) {
            mouseInput = Vector2.zero;
        }

        player.SetMouseInput(mouseInput);
    }

    private void OnJumpInputDown() {
        if (player.movementEnabled) {
            OnJumpDown?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnJumpInputUp() {
        if (player.movementEnabled) {
            OnJumpUp?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnPauseInputDown() {
        OnPausedDown?.Invoke(this, EventArgs.Empty);
    }

    private void OnPauseInputUp() {
        OnPausedUp?.Invoke(this, EventArgs.Empty);
    }
}
