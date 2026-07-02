using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumping : MonoBehaviour
{
    [Header("Jump Physics")]
    public float jumpForce = 8f;
    public float fallMultiplier = 2.5f;       // Speeds up the descent
    public float lowJumpMultiplier = 2.0f;    // Cuts jump short if button is released early

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Forgiveness Timers (Juice)")]
    public float coyoteTimeMax = 0.15f;
    public float jumpBufferMax = 0.15f;

    private Rigidbody rb;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Ground Check & Coyote Time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTimeMax;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // 2. Jump Buffering Input
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferMax;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // 3. Trigger the Jump
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            ExecuteJump();
        }
    }

    void FixedUpdate()
    {
        // 4. Custom Snappy Gravity Multipliers
        // If falling, drop faster
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        // If rising but the jump button was released, drop faster early
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void ExecuteJump()
    {
        // Reset vertical speed before applying force to keep it consistent
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Clear buffers so the jump cannot instantly retrigger
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }

    
}