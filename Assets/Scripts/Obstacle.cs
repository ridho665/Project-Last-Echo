using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float batteryDrainAmount = 10f; // Jumlah pengurangan baterai

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menyentuh obstacle adalah Player
        if (other.CompareTag("Player"))
        {
            // Coba dapatkan komponen PlayerBattery dari player
            PlayerBattery playerBattery = other.GetComponent<PlayerBattery>();

            // Jika player memiliki komponen PlayerBattery
            if (playerBattery != null)
            {
                // Kurangi baterai pemain
                playerBattery.RechargeBattery(-batteryDrainAmount); // Mengurangi 10 unit baterai
                Debug.Log("Player terkena obstacle, baterai berkurang 10!");
                Destroy(gameObject);
            }
        }
    }
}
