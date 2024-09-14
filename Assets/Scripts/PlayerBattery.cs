using UnityEngine;
using UnityEngine.UI;

public class PlayerBattery : MonoBehaviour
{
    [SerializeField] private float maxBattery = 100f; // Maksimum daya baterai
    [SerializeField] private float batteryDrainRate = 1f; // Laju pengurangan baterai per detik
    [SerializeField] private RectTransform batteryBar; // Referensi ke UI RectTransform (battery bar)
    // [SerializeField] private GameObject gameoverUI; // Referensi ke UI GameOver

    private float currentBattery;
    private bool isDead = false;
    private PlayerController playerController;

    private void Start()
    {
        currentBattery = maxBattery;
        playerController = GetComponent<PlayerController>();

        if (batteryBar == null)
        {
            FindBatteryBar(); // Cari battery bar saat Start jika belum di-set
        }
        
        UpdateBatteryUI(); // Update UI saat game dimulai
    }

    private void Update()
    {      
        if (isDead)
            return;

        // Baterai berkurang terus, baik saat player bergerak maupun diam
        DrainBattery();
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

    public void UpdateBatteryUI()
    {
        if (batteryBar != null)
        {
            // Update RectTransform scale berdasarkan sisa baterai
            float scaleX = currentBattery / maxBattery;
            batteryBar.localScale = new Vector3(scaleX, 1f, 1f); // Hanya skala X yang berubah
        }
    }

    public void InitializeBattery(RectTransform batteryBarUI)
    {
        // Inisialisasi ulang baterai dan UI saat player respawn
        batteryBar = batteryBarUI;
        currentBattery = maxBattery;
        isDead = false;
        UpdateBatteryUI(); // Update UI agar sinkron dengan state baterai saat ini
    }

    private void FindBatteryBar()
    {
        // Cari GameObject dengan nama "BarCurrent"
        GameObject batteryBarObject = GameObject.Find("BarCurrent");

        // Pastikan object ditemukan dan memiliki RectTransform
        if (batteryBarObject != null)
        {
            batteryBar = batteryBarObject.GetComponent<RectTransform>();
            if (batteryBar != null)
            {
                Debug.Log("Battery bar ditemukan dan diassign.");
            }
            else
            {
                Debug.LogError("GameObject BarCurrent ditemukan, tetapi tidak memiliki RectTransform.");
            }
        }
        else
        {
            Debug.LogError("GameObject BarCurrent tidak ditemukan!");
        }
    }

}
