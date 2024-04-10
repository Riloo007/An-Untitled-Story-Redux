using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    
    // Constants
    [Tooltip("The upwards velocity of the player the frame a jump begins")]
    public float initialJumpVelocity = 0.02f;

    [Tooltip("The longest the user can hold the jump button for a maximum jump")]
    public float risingJumpDuration = .1f;

    [Tooltip("The jump formula multiplier (doesn't affect initial jump velocity)")]
    public float longJumpMultiplier = 0.001f;

    public float gravity = 1;
    public Transform groundCheckPosition;
    public Transform roofCheckPosition;
    public LayerMask groundLayer;
    public int maxJumpCount = 2;
    public float moveSpeed = 2;
    private Rigidbody2D rb;
    private Transform tf;
    public bool particlesEnabled;
    public ParticleSystem particles;
    public ParticleSystem subParticles;

    public bool active;


    // Multi Frame variables
    private bool isPressingJump; // Used to track longer jumps
    private int currentJumpCount; // Used for multi-jumps
    private float jumpTimer; // Used to time longer jumps by holding the spacebar
    public Vector2 velocity;


    // Single Frame variables
    private bool pressedJump;
    private bool touchingGround;
    private bool wasGrounded;
    private bool groundedEnough;
    private bool isRoofed;
    private float horizontalInput;

    private EventController EventController;

    void Start()
    {
        EventController = FindObjectOfType<EventController>();

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    void UpdateTimers() {
        jumpTimer += Time.deltaTime;
    }

    void Update()
    // void FixedUpdate()
    {
        if(EventController.gamePaused == true) return;
        if(!active) return;

        UpdateTimers();
        Physics();
        if(groundedEnough) currentJumpCount = 1;


        CaptureInput(); // isPressingJump, pressedJump, horizontalInput

        if(pressedJump && currentJumpCount < maxJumpCount) {
            jumpTimer = 0;
            currentJumpCount += 1;
            velocity.y = Mathf.Max(velocity.y, initialJumpVelocity);
        }

        if(currentJumpCount <= maxJumpCount && isPressingJump) {
            // Apply a sloped velocity to the player
            velocity.y += Mathf.Max(-Mathf.Pow(jumpTimer, 2) + risingJumpDuration, 0) * longJumpMultiplier;
        }

        velocity.x = horizontalInput * moveSpeed;

        // if(velocity.magnitude > 0.01 && touchingGround) {
        //     Particles();
        //     // Debug.Log(velocity);
        // } else {
        //     // particles.Pause();
        // }

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
        velocity.y = longJumpMultiplier;
    }

    void Physics() {
        velocity -= new Vector2(0, gravity * Time.deltaTime);
        wasGrounded = groundedEnough;

        isRoofed = Physics2D.OverlapCircle(roofCheckPosition.position, .02f, groundLayer);
        touchingGround = Physics2D.OverlapCircle(groundCheckPosition.position, .005f, groundLayer);
        groundedEnough = Physics2D.OverlapCircle(groundCheckPosition.position, .03f, groundLayer);
        // if(velocity.y < -1 && isGrounded) velocity.y = -.01f;
        // if(velocity.y < -1 && isGrounded) velocity.y = 0f;
        if(velocity.y > 0f && isRoofed) {
            // Bounce back off the roof
            velocity.y = 0f;
        }
        if(velocity.y < 0f && touchingGround) {
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

    public static implicit operator PlayerController(SlotData v)
    {
        throw new NotImplementedException();
    }
}
