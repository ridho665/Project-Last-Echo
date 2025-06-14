using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] public GameObject gameoverUI; // UI GameOver
    [SerializeField] private RectTransform batteryBar;

    private PlayerController playerController;
    private PlayerShield playerShield;
    private PlayerBattery playerBattery;
    private bool gamePaused;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            playerBattery = player.GetComponent<PlayerBattery>();
            playerShield = player.GetComponent<PlayerShield>();
            // lightSeed = player.GetComponent<LightSeed>();
        }

        PlayerManager.instance.inGameUI = this;
        Time.timeScale = 1;
        SwitchUI(inGameUI); // Set UI awal ke in-game UI

        // Pastikan GameOver UI tidak aktif di awal
        if (gameoverUI != null)
        {
            gameoverUI.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateInGameInfo();

        if (Input.GetKeyDown(KeyCode.Escape))
            CheckIfNotPaused();

        // Cek apakah baterai habis
        CheckIfBatteryDepleted();

        // Cek apakah range LightSeed habis
        // CheckIfLightSeedDepleted();
    }

    private bool CheckIfNotPaused()
    {
        if (!gamePaused)
        {
            if (playerController != null) playerController.enabled = false;
            if (playerShield != null) playerShield.enabled = false;

            AudioManager.instance.StopSFX(0);

            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            if (playerController != null) playerController.enabled = true;
            if (playerShield != null) playerShield.enabled = true;

            // AudioManager.instance.PlaySFX(0);
            
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }

    private void UpdateInGameInfo()
    {
        playerBattery = PlayerManager.instance.GetPlayerBattery();
        // lightSeed = PlayerManager.instance.GetLightSeed(); // Ambil referensi ke LightSeed dari PlayerManager
    }

    // Cek jika baterai player habis, aktifkan UI GameOver
    private void CheckIfBatteryDepleted()
    {
        if (playerBattery != null && playerBattery.GetCurrentBattery() <= 0)
        {
            if (playerShield != null) playerShield.enabled = false;
            AudioManager.instance.StopSFX(0);
            Time.timeScale = 0; // Hentikan waktu permainan
            SwitchUI(gameoverUI); // Aktifkan GameOver UI
        }
    }

    // Cek jika range LightSeed habis, aktifkan UI GameOver
    // private void CheckIfLightSeedDepleted()
    // {
    //     if (lightSeed != null && lightSeed.GetLightRange() <= 0) // Asumsi ada fungsi GetRange() di LightSeed
    //     {
    //         Time.timeScale = 0; // Hentikan waktu permainan
    //         SwitchUI(gameoverUI); // Aktifkan GameOver UI
    //     }
    // }

    public void SwitchUI(GameObject uiMenu)
    {
        // Nonaktifkan semua UI child, kemudian aktifkan yang sesuai
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);
    }  

    public void RestartGame()
    {
        AudioManager.instance.PlaySFX(5);

        GameManager.instance.ResetProgress(); // Reset progress dan mulai dari level 1
        GameManager.instance.LoadScene(1); // Mulai dari Level 1
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.PlaySFX(5);

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PlayBGM(0);
    } 
}
