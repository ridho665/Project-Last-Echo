    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask whatIsGround;

        private Rigidbody rb;
        private bool isFacingRight = true;
        private bool isGrounded;

        public Animator playerAnim;
        public Rigidbody playerRigid;
        public bool walking;


        private void Start()
        {

        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); // Menyambungkan secara otomatis ke komponen Animator di objek yang sama
      
        }

    private void Update()
        {
            // Ground Check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

            float move = Input.GetAxis("Horizontal") * moveSpeed;
            rb.velocity = new Vector3(move, rb.velocity.y, 0f);

            if (move > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (move < 0 && isFacingRight)
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
               playerAnim.SetTrigger("jump");
                playerAnim.ResetTrigger("idle");
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {

            playerAnim.ResetTrigger("jump");
            playerAnim.SetTrigger("idle");
            //steps1.SetActive(false); // Nonaktifkan suara langkah jika ada
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                walking = true;
                //steps1.SetActive(true); // Aktifkan suara langkah jika ada
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                walking = false;
                //steps1.SetActive(false); // Nonaktifkan suara langkah jika ada
            }

           


        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }


    }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
