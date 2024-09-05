using UnityEngine;

public class Battery : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Cek jika objek yang bersentuhan adalah player
        if (other.CompareTag("Player"))
        {
            // Coba mendapatkan komponen PlayerBattery dari player
            PlayerBattery playerBattery = other.GetComponent<PlayerBattery>();

            // Jika player memiliki komponen PlayerBattery, isi baterai penuh
            if (playerBattery != null)
            {
                playerBattery.RechargeBattery(playerBattery.MaxBattery - playerBattery.GetCurrentBattery()); // Isi baterai sampai penuh
                Debug.Log("Battery picked up! Battery is now full.");

                // Hancurkan objek baterai setelah diambil
                Destroy(gameObject);
            }
        }
    }
}
