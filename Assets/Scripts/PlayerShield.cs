using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject; // Referensi ke objek shield
    [SerializeField] private float shieldDuration = 5f; // Durasi shield aktif
    [SerializeField] private float shieldCooldown = 10f; // Cooldown shield setelah digunakan

    public bool IsShieldActive { get; private set; } = false; // Properti publik untuk memeriksa status shield

    private bool isCooldown = false;
    private bool canActivateShield = false;

    private void Update()
    {
        if (canActivateShield && Input.GetMouseButtonDown(0) && !IsShieldActive && !isCooldown) // Klik kiri mouse
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
        IsShieldActive = true;
        isCooldown = true;
        shieldObject.SetActive(true); // Tampilkan shield

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(4);
        }

        Invoke("DeactivateShield", shieldDuration); // Matikan shield setelah durasi
    }

    private void DeactivateShield()
    {
        shieldObject.SetActive(false); // Sembunyikan shield
        IsShieldActive = false;

        Invoke("ResetCooldown", shieldCooldown); // Atur cooldown sebelum bisa menggunakan shield lagi
    }

    private void ResetCooldown()
    {
        isCooldown = false;
    }
}
