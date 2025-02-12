using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = walkSpeed;
    public float gravity = -9f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    private const float walkSpeed = 4;
    private const float runSpeed = 6.5f;

    private bool isGrounded;
    private float lastGroundedTime = -1f;
    private float lastJumpPressTime = -1f;
    private bool hasJumped = false; // New flag to prevent double jump exploit

    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Store last time on the ground
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            hasJumped = false; // Reset jump flag when grounded

            // Reset downward velocity when touching the ground
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        // Store jump input time
        if (Input.GetButtonDown("Jump"))
        {
            lastJumpPressTime = Time.time;
        }

        // Handle Jump Buffering & Coyote Time
        if (!hasJumped && (Time.time - lastGroundedTime <= coyoteTime) && (Time.time - lastJumpPressTime <= jumpBufferTime))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            hasJumped = true; // Mark that the player has jumped
            lastJumpPressTime = -1f; // Reset jump buffer to prevent another jump immediately
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Move character using input
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply vertical movement separately to prevent overriding jump
        controller.Move(velocity * Time.deltaTime);

        // Sprinting
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }
}


