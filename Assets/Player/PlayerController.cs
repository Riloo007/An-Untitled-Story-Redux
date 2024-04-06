using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Constants
    public float jumpForce = 10;
    public float gravity = 1;
    public Transform groundCheckPosition;
    public Transform roofCheckPosition;
    public LayerMask groundLayer;
    public int maxJumps = 2;
    public float moveSpeed = 2;
    private Rigidbody2D rb;
    private Transform tf;
    public bool particlesEnabled;
    public ParticleSystem particles;
    public ParticleSystem subParticles;

    public bool active;


    // Multi Frame variables
    private bool isPressingJump; // Used to track longer jumps
    private int jumpCount; // Used for multi-jumps
    private float jumpTime; // Used to time longer jumps by holding the spacebar
    private Vector2 velocity;


    // Single Frame variables
    private bool pressedJump;
    private bool isGrounded;
    private bool isRoofed;
    private float horizontalInput;

    private EventController EventController;

    void Start()
    {
        EventController = FindObjectOfType<EventController>();

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }


    void Update()
    // void FixedUpdate()
    {
        if(EventController.gamePaused == true) return;
        if(!active) return;

        // Constant Forces
        ApplyConstantForces();
        
        // User Forces

        // Jump is still pressed from last frame, continue going up
        // if(isPressingJump) Jump();

        CaptureInput();

        if(isGrounded) jumpCount = 1;

        if(pressedJump && jumpCount < maxJumps) {
            jumpCount += 1;
            // jumpTime = Time.fixedTime;
            // Jump();
            velocity.y = jumpForce;
        }

        velocity.x = horizontalInput * moveSpeed;

        if(velocity.magnitude > 0.01 && isGrounded) {
            Particles();
            // Debug.Log(velocity);
        } else {
            // particles.Pause();
        }

        // Apply movement
        rb.MovePosition(rb.position + velocity);
    }

    void Particles() {
        if(!particlesEnabled) return;

        // Use probability to only emit about 4 bursts per second
        float emissionProbability = 10f * Time.deltaTime;
        if (UnityEngine.Random.value > emissionProbability) return;

        RaycastHit2D hit = Physics2D.Raycast(tf.position, Vector2.down, 10, groundLayer);

        if (hit.collider != null)
        {
            // If the ray hits the ground layer, get the hit position
            Vector2 groundPosition = hit.point;
            Debug.Log("BAM");

            ParticleSystem.EmitParams emitParams = new() {
                position = hit.point
            };
            particles.Emit(emitParams, 1);
            
            // subParticles.Emit(20);
        }
        else
        {
            // If the ray doesn't hit anything, you may want to handle this case
            // Debug.Log("No ground detected within max distance.");
        }
    }

    // Jump with account to jumpCount; The second jump should have a different force than the first one
    void Jump() {
        velocity.y = jumpForce;
    }

    void ApplyConstantForces() {
        velocity -= new Vector2(0, gravity * Time.deltaTime);

        isRoofed = Physics2D.OverlapCircle(roofCheckPosition.position, .02f, groundLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, .02f, groundLayer);
        // if(velocity.y < -1 && isGrounded) velocity.y = -.01f;
        // if(velocity.y < -1 && isGrounded) velocity.y = 0f;
        if(velocity.y > 0f && isRoofed) {
            // Bounce back off the roof
            velocity.y = 0f;
        }
        if(velocity.y < 0f && isGrounded) {
            // Stop falling so sliding doesn't happen
            velocity.y = 0f;

            // Position so the player is actually on the ground // I DONT KNOW HOW
            // RaycastHit2D hit = Physics2D.Raycast(tf.position, Vector2.down, 10, groundLayer);
            // if (hit.collider != null)
            //     velocity.y = -(tf.position.y - hit.point.y);
        }

        Debug.DrawRay(groundCheckPosition.position, Vector3.down * .02f);
        // Debug.Log(jumpCount);
    }

    void CaptureInput() {
        isPressingJump = Input.GetKey(KeyCode.UpArrow);
        pressedJump = Input.GetKeyDown(KeyCode.UpArrow);
        horizontalInput = Input.GetAxis("Horizontal");
    }
}
