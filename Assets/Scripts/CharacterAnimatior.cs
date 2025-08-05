using UnityEngine;

public class CharacterAnimatior : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float jumpForce = 6f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
        AdjustJumpAnimationSpeed();
    }

    void Update()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.ResetTrigger("jump"); 
            animator.SetTrigger("jump");   
        }

        
        animator.SetBool("isRunning", moveInput != 0);
        animator.SetBool("isGrounded", isGrounded);

        
        if (Input.GetKeyDown(KeyCode.F))
            animator.SetTrigger("strike");
        if (Input.GetKeyDown(KeyCode.H))
            animator.SetTrigger("hurt");
        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("die");
    }

    void AdjustJumpAnimationSpeed()
    {
        
        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float totalAirTime = (2 * jumpForce) / gravity;

        
        float defaultAnimLength = 0.3f; 
        float newSpeed = defaultAnimLength / totalAirTime;

        
        animator.SetFloat("JumpSpeed", newSpeed);
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

