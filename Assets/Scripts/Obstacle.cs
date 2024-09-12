using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float lightRangeReduction = 1f; // Pengurangan range cahaya
    private PlayerShield playerShield;

    private void Start()
    {
        // Cari PlayerShield di objek pemain jika belum diatur di Inspector
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerShield = player.GetComponent<PlayerShield>();
        }
        else
        {
            Debug.LogError("Player tidak ditemukan. Pastikan tag Player diterapkan pada objek pemain.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerShield != null)
            {
                if (playerShield.IsShieldActive)
                {
                    // Jika shield aktif, jangan kurangi lightseed
                    Debug.Log("Shield aktif, tidak mengurangi lightseed.");
                }
                else
                {
                    // Kurangi range cahaya dari LightSeed
                    LightSeed lightSeed = other.GetComponent<LightSeed>();
                    if (lightSeed != null)
                    {
                        lightSeed.ReduceLightRange(lightRangeReduction); // Kurangi range cahaya
                        Debug.Log("Player terkena obstacle, range cahaya berkurang 1!");
                    }
                }
            }
            else
            {
                Debug.LogWarning("PlayerShield tidak ditemukan pada objek pemain.");
            }

            // Hancurkan obstacle setelah menyentuh player
            Destroy(gameObject);
        }
    }
}
