using UnityEngine;
using UnityEngine.UI;

public class PlayerBattery : MonoBehaviour
{
    [SerializeField] private float maxBattery = 100f; // Maksimum daya baterai
    [SerializeField] private float batteryDrainRate = 1f; // Laju pengurangan baterai per detik
    [SerializeField] private RectTransform batteryBar; // Referensi ke UI RectTransform (battery bar)

    private float currentBattery;
    private bool isDead = false;
    private PlayerController playerController;

    private void Start()
    {
        currentBattery = maxBattery;
        playerController = GetComponent<PlayerController>();
        UpdateBatteryUI(); // Update UI saat game dimulai
    }

    private void Update()
    {
        if (isDead)
            return;

        // Baterai berkurang terus, baik saat player bergerak maupun diam
        DrainBattery();

        // Jika baterai habis
        if (currentBattery <= 0)
        {
            Die();
        }
    }

    public float MaxBattery
    {
        get { return maxBattery; }
    }

    private void DrainBattery()
    {
        currentBattery -= batteryDrainRate * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery); // Pastikan baterai tidak turun di bawah 0
        UpdateBatteryUI(); // Update UI saat baterai berkurang
    }

    private void Die()
    {
        isDead = true;
        playerController.enabled = false; // Matikan kontrol pemain
        Debug.Log("Player mati karena baterai habis!");
        // Tambahkan efek kematian atau animasi di sini
    }

    public void RechargeBattery(float amount)
    {
        if (!isDead)
        {
            currentBattery += amount;
            currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery); // Pastikan baterai tidak melebihi maxBattery
            UpdateBatteryUI(); // Update UI saat baterai diisi ulang
        }
    }

    public float GetCurrentBattery()
    {
        return currentBattery;
    }

    private void UpdateBatteryUI()
    {
        if (batteryBar != null)
        {
            // Update RectTransform scale berdasarkan sisa baterai
            float scaleX = currentBattery / maxBattery;
            batteryBar.localScale = new Vector3(scaleX, 1f, 1f); // Hanya skala X yang berubah
        }
    }
}
