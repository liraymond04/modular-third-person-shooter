using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller3D))]
public class Player : MonoBehaviour {

    public bool movementEnabled = true;
    public bool mouseLookEnabled = true;
    public GameObject _camera;

    Controller3D controller;

    [HideInInspector]
    public Vector2 directionalInput;
    [HideInInspector]
    public Vector2 mouseInput;

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isJumping = false;
    [HideInInspector]
    public bool isKnockback = false;
    [HideInInspector]
    public bool isMoving = false;

    void Start() {
        controller = GetComponent<Controller3D>();
    }

    public void SetDirectionalInput(Vector2 input) {
        directionalInput = input;
    }

    public void SetMouseInput(Vector2 input) {
        mouseInput = input;
    }

    public float GetDrag() {
        return controller.drag;
    }
}
