using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject; // Referensi ke objek shield
    [SerializeField] private float shieldDuration = 5f; // Durasi shield aktif
    [SerializeField] private float shieldCooldown = 10f; // Cooldown shield setelah digunakan

    private bool isShieldActive = false;
    private bool isCooldown = false;
    private bool canActivateShield = false;

    private void Update()
    {
        if (canActivateShield && Input.GetMouseButtonDown(0) && !isShieldActive && !isCooldown) // Klik kiri mouse
        {
            ActivateShield();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShieldDetect"))
        {
            canActivateShield = true; // Bisa mengaktifkan shield setelah menyentuh objek dengan tag ShieldDetect
        }
    }

    private void ActivateShield()
    {
        isShieldActive = true;
        isCooldown = true;
        shieldObject.SetActive(true); // Tampilkan shield

        Invoke("DeactivateShield", shieldDuration); // Matikan shield setelah durasi
    }

    private void DeactivateShield()
    {
        shieldObject.SetActive(false); // Sembunyikan shield
        isShieldActive = false;

        Invoke("ResetCooldown", shieldCooldown); // Atur cooldown sebelum bisa menggunakan shield lagi
    }

    private void ResetCooldown()
    {
        isCooldown = false;
    }
}
