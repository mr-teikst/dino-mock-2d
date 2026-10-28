using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputActionReference jumpAction;
    public InputActionReference dashAction;
    public LayerMask groundLayer;

    public float jumpForce = 9f;
    public float dashDistance = 2.5f;  
    public float dashTime = 0.35f;      
    public float dashCooldown = 1f;     
    public float returnSpeed = 1f;      
    public Vector2 dashColliderSize = new Vector2(0.9f, 0.75f);     
    public Vector2 dashColliderOffset = new Vector2(0.25f, 0.375f); 

    public bool IsGrounded { get; private set; }
    public bool IsDashing { get; private set; }

    Rigidbody2D rb;
    CapsuleCollider2D col;
    Vector2 normalSize;
    Vector2 normalOffset;
    float homeX;
    float dashTimer;      
    float cooldownTimer;  

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        normalSize = col.size;
        normalOffset = col.offset;
        homeX = transform.position.x;
    }

    void OnEnable()  { jumpAction.action.Enable();  dashAction.action.Enable(); }
    void OnDisable() { jumpAction.action.Disable(); dashAction.action.Disable(); }

    void Update()
    {
        IsGrounded = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.1f, groundLayer);

        if (jumpAction.action.WasPressedThisFrame() && IsGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        cooldownTimer -= Time.deltaTime;
        if (dashAction.action.WasPressedThisFrame() && cooldownTimer <= 0f)
        {
            dashTimer = dashTime;
            cooldownTimer = dashCooldown;
        }

        float vx;
        if (dashTimer > 0f)
        {
            dashTimer -= Time.deltaTime;
            vx = dashDistance / dashTime;
        }
        else
        {
            vx = Mathf.Clamp(homeX - transform.position.x, -returnSpeed, returnSpeed);
        }
        IsDashing = dashTimer > 0f;

        col.size = IsDashing ? dashColliderSize : normalSize;
        col.offset = IsDashing ? dashColliderOffset : normalOffset;

        rb.linearVelocity = new Vector2(vx, rb.linearVelocity.y);
    }
}
