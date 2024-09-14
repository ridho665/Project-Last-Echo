using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck; // Referensi untuk wall check
    [SerializeField] private float wallCheckRadius = 0.2f; // Radius untuk wall check
    [SerializeField] private LayerMask whatIsWall; // Layer mask untuk wall


    private Rigidbody rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isTouchingWall; 
    private bool isWalkingSoundPlaying = false;

    public bool canMove = true;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        isTouchingWall = Physics.CheckSphere(wallCheck.position, wallCheckRadius, whatIsWall);

        if (!canMove) 
        {
            // Jika tidak bisa bergerak, hentikan animasi dan kembalikan velocity ke 0
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            animator.SetBool("isWalking", false);

            if (isWalkingSoundPlaying)
            {
                AudioManager.instance.StopSFX(0); // Hentikan suara langkah
                isWalkingSoundPlaying = false;
            }

            return; // Tidak melanjutkan proses update
        }
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        float move = Input.GetAxis("Horizontal") * moveSpeed;

        if (isTouchingWall && move != 0)
        {
            move = 0;
        }

        rb.velocity = new Vector3(move, rb.velocity.y, 0f);

        // Handle flipping
        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Character is jumping.");
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            animator.SetBool("isJumping", true);

            AudioManager.instance.PlaySFX(1);

        }

        // Handle animation transitions
        if (Mathf.Abs(move) > 0.1f && isGrounded)
        {
            animator.SetBool("isWalking", true);

            if (!isWalkingSoundPlaying)
            {
                AudioManager.instance.PlaySFX(0); // Anggap sound effect jalan berada pada index 1
                isWalkingSoundPlaying = true;
            }

        }
        else
        {
            animator.SetBool("isWalking", false);

            if (isWalkingSoundPlaying)
            {
                AudioManager.instance.StopSFX(0); // Menghentikan sound effect jalan
                isWalkingSoundPlaying = false;
            }

        }

        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Gizmos untuk menampilkan Ground Check di editor
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
        
    }
}
