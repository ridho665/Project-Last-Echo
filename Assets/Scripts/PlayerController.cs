    using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask whatIsGround;


    // Tambahan untuk stamina
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float staminaRegenRate = 5f;
        [SerializeField] private float staminaDrainRate = 10f;
        private float currentStamina;
        public UnityEngine.UI.Image staminaBar; // pastikan ini disambungkan ke UI Image di Canvas


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
        currentStamina = maxStamina;

         }

    private void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        float move = Input.GetAxis("Horizontal") * moveSpeed;
        rb.velocity = new Vector3(move, rb.velocity.y, 0f);

        if (move != 0 && isGrounded)
        {
            DrainStamina(staminaDrainRate * Time.deltaTime);
        }
        else if (isGrounded)
        {
            RegenStamina(staminaRegenRate * Time.deltaTime);
        }

        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded && currentStamina > 0)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");
            DrainStamina(staminaDrainRate); // Kurangi stamina saat lompat
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.ResetTrigger("jump");
            playerAnim.SetTrigger("idle");
        }

        if (Input.GetKeyDown(KeyCode.D) && currentStamina > 0)
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina > 0)
            {
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
                DrainStamina(staminaDrainRate * Time.deltaTime * 2); // Drain lebih banyak saat berlari
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }

        // Update stamina bar width
        RectTransform rt = staminaBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(currentStamina / maxStamina * 150f, rt.sizeDelta.y); // 150f adalah nilai width maksimal, sesuaikan dengan kebutuhan Anda
    }
    


    private void DrainStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    private void RegenStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
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
