using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // [SerializeField] private float batteryDrainAmount = 10f; // Jumlah pengurangan baterai
    [SerializeField] private float lightRangeReduction = 1f; // Pengurangan range cahaya

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menyentuh obstacle adalah Player
        if (other.CompareTag("Player"))
        {
            // // Kurangi baterai pemain
            // PlayerBattery playerBattery = other.GetComponent<PlayerBattery>();
            // if (playerBattery != null)
            // {
            //     playerBattery.RechargeBattery(-batteryDrainAmount); // Mengurangi baterai
            //     Debug.Log("Player terkena obstacle, baterai berkurang 10!");
            // }

            // Kurangi range cahaya dari LightSeed
            LightSeed lightSeed = other.GetComponent<LightSeed>();
            if (lightSeed != null)
            {
                lightSeed.ReduceLightRange(lightRangeReduction); // Kurangi range cahaya
                Debug.Log("Player terkena obstacle, range cahaya berkurang 1!");
            }

            // Hancurkan obstacle setelah menyentuh player
            Destroy(gameObject);
        }
    }
}
