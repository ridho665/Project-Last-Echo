using System.Collections;
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
    private float currentStamina;
    public UnityEngine.UI.Image staminaBar;

    private Rigidbody rb;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isInDesert = false; // Tambahan: cek apakah pemain di Desert land
    private float overheatMultiplier = 5f; // Tambahan: faktor pengurangan stamina saat overheat

    public Animator playerAnim;
    public Rigidbody playerRigid;
    public bool walking;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
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
            walking = true;
            DrainStamina(2.5f * Time.deltaTime);
        }
        else if (isGrounded)
        {
            walking = false;
            DrainStamina(0.5f * Time.deltaTime);
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
            StartCoroutine(DrainStaminaOverTime(3f, 1f));
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

        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina > 0)
            {
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
                DrainStamina(6f * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }

        // Update stamina bar width dan warnanya
        UpdateStaminaBar();

    }

    private void DrainStamina(float amount)
    {
        // Tambahan: Jika di Desert land, stamina habis lebih cepat
        if (isInDesert)
        {
            amount *= overheatMultiplier;
        }

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

    private void UpdateStaminaBar()
    {
        RectTransform rt = staminaBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(currentStamina / maxStamina * 150f, rt.sizeDelta.y);

        Color32 greenColor = new Color32(150, 187, 124, 255);
        Color32 yellowColor = new Color32(250, 213, 134, 255);
        Color32 redColor = new Color32(198, 71, 86, 255);

        Color32 barColor;

        if (currentStamina / maxStamina > 0.5f)
        {
            barColor = greenColor;
        }
        else if (currentStamina / maxStamina > 0.2f)
        {
            barColor = yellowColor;
        }
        else
        {
            barColor = redColor;
        }

        staminaBar.color = barColor;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private IEnumerator DrainStaminaOverTime(float totalDrain, float duration)
    {
        float amountDrained = 0f;
        while (amountDrained < totalDrain && currentStamina > 0)
        {
            float drainAmount = (totalDrain / duration) * Time.deltaTime;
            DrainStamina(drainAmount);
            amountDrained += drainAmount;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Tambahan: Cek jika player masuk ke Desert land
        if (other.CompareTag("Desert land"))
        {
            isInDesert = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Tambahan: Cek jika player keluar dari Desert land
        if (other.CompareTag("Desert land"))
        {
            isInDesert = false;
        }
    }
}
