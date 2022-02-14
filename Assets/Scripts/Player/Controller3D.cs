using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Controller3D : MonoBehaviour {

    Player player;

    Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float accelerationGround = 0.1f;
    public float accelerationAir = 0.2f;

    [Header("Jump")]
    public float jumpForceMax = 4f;
    public float jumpForceMin = 1f;
    public float fallSpeed = 1f;

    [Header("Ground")]
    public string groundTag;
    public float checkRadius;
    public float checkDistance;
    public float slopeLimit;
    public float slopeRayLength;
    public float slopeForce;

    [Header("Physics")]
    public float gravity = 1f;
    public float drag = 1f;
    public float terminalVelocity;

    [Header("Knockback")]
    public bool kbDisableMovement;
    public float kbSpeed = 5f;
    private Vector3 kbImpact = Vector3.zero;
    private bool wasMovementEnabled;

    Vector3 velocity;
    private float currentSpeed;
    private Vector3 previousVelocity;
    private float directionY;

    private Vector3 groundSlopeDir;
    private float groundSlopeAngle;
    private RaycastHit groundRay;
    private Vector3 slopeVelocity;

    void Start() {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        
        PlayerInput playerInput = GetComponent<PlayerInput>();
        playerInput .OnJumpDown += OnJumpInputDown;
        playerInput .OnJumpUp += OnJumpInputUp;
    }

    void Update() {
        player.isGrounded = Physics.SphereCast(transform.position, checkRadius, Vector3.down, out groundRay, checkDistance) && (groundRay.collider.gameObject.tag == groundTag);

        CalculateVelocity();
        HandleGround();
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * checkDistance, checkRadius);
    }

    private void CalculateVelocity() {
        // Initialize veloctiy vector
        velocity = Vector3.zero;

        // Reset Y velocity
        if (player.isGrounded && !player.isJumping) {
            directionY = 0;
        }

        // Movement
        PlayerMovement();

        // Gravity
        if (!player.isGrounded && (directionY > -terminalVelocity)) {
            directionY += -gravity * Time.deltaTime;
        }
        velocity.y += directionY;

        // Handle slopes
        if (groundSlopeAngle > slopeLimit) {
            slopeVelocity += groundSlopeDir * -gravity * Time.deltaTime;
        } else {
            slopeVelocity = Vector3.zero;
        }
        velocity += slopeVelocity;

        // Slope pull down
        if ((Mathf.Round(player.directionalInput.x) != 0 || Mathf.Round(player.directionalInput.y) != 0) && OnSlope()) {
            velocity.y += slopeForce;
        }

        // Handle knockback
        if (kbImpact.magnitude > 0.2f) {
            player.isKnockback = true;
            velocity += new Vector3(kbImpact.x, 0, kbImpact.z);
            if (kbDisableMovement && player.movementEnabled) {
                wasMovementEnabled = player.movementEnabled;
                player.movementEnabled = false;
            }
        } else {
            player.isKnockback = false;
            if (kbDisableMovement && wasMovementEnabled) {
                player.movementEnabled = true;
            }
        }
        kbImpact = Vector3.Lerp(kbImpact, Vector3.zero, kbSpeed*Time.deltaTime);

        // Apply movement
        Move(velocity);
    }

    private void PlayerMovement() {
        float targetSpeed = moveSpeed * player.directionalInput.magnitude;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelerationGround * Time.deltaTime);

        Vector3 forward = player.transform.forward;
        Vector3 right = player.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();

        Vector3 movementDirection;

        if (targetSpeed != 0) {
            movementDirection = forward * player.directionalInput.y + right * player.directionalInput.x;
        } else {
            movementDirection = previousVelocity.normalized;
        }
        //movementDirection += groundSlopeDir;
        if (groundRay.normal != Vector3.zero) {
            Vector3 temp = Vector3.Cross(groundRay.normal, movementDirection);
            movementDirection = Vector3.Cross(temp, groundRay.normal);
            // Debug.DrawRay(transform.position, movementDirection * 2, Color.red);
        }
        
        velocity += movementDirection * currentSpeed;

        previousVelocity = rb.velocity;
        previousVelocity.y = 0f;
    }

    private void HandleGround() {
        if (player.isGrounded) {
            Vector3 temp = Vector3.Cross(groundRay.normal, Vector3.up);
            groundSlopeDir = Vector3.Cross(temp, groundRay.normal);
            groundSlopeAngle = Vector3.Angle(groundRay.normal, Vector3.up);
            // Debug.DrawRay(transform.position, groundSlopeDir * 2, Color.red);
        } else {
            groundSlopeDir = Vector3.zero;
            groundSlopeAngle = 0f;
            // groundSlopeDir = new Vector3(0, 1, 0);
        }
    }

    private void Move(Vector3 moveAmount) {
        rb.velocity = moveAmount;
    }

    public void Knockback(float strength, Vector3 pos, bool yVel, float yVelAngle) {
        Vector3 dir = transform.position - pos;
        dir.Normalize();
        if (yVel) {
            dir.y = yVelAngle;
        }
        kbImpact += dir.normalized * strength;
        player.isJumping = true;
        directionY = kbImpact.y;
        // Debug.Log(kbImpact.y + " " + directionY);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == groundTag) {
            player.isKnockback = false;
            player.isJumping = player.isJumping && !player.isGrounded;
        }
    }

    public void OnJumpInputDown(object sender, EventArgs e) {
        if (player.isGrounded && !player.isJumping) {
            player.isJumping = true;
            directionY = jumpForceMax;
        }
    }

    public void OnJumpInputUp(object sender, EventArgs e) {
        if (player.isJumping && !player.isGrounded) {
            if (directionY > jumpForceMin) {
                directionY = jumpForceMin;
            }
        }
    }
    
    private bool OnSlope() {
        if (player.isJumping) {
            return false;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeRayLength)) {
            if (hit.normal != Vector3.up) {
                return true;
            }
        }
        return false;
    }
    
}
