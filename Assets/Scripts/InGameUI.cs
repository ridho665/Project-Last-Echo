using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameoverUI; // UI GameOver
    [SerializeField] private RectTransform batteryBar;

    private PlayerBattery playerBattery;
    private bool gamePaused;

    private void Start()
    {
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
    }

    private bool CheckIfNotPaused()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            gamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }
    }

    private void UpdateInGameInfo()
    {
        playerBattery = PlayerManager.instance.GetPlayerBattery();
    }

    // Cek jika baterai player habis, aktifkan UI GameOver
    private void CheckIfBatteryDepleted()
    {
        if (playerBattery != null && playerBattery.GetCurrentBattery() <= 0)
        {
            Time.timeScale = 0; // Hentikan waktu permainan
            SwitchUI(gameoverUI); // Aktifkan GameOver UI
        }
    }

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
        GameManager.instance.ResetProgress(); // Reset progress dan mulai dari level 1
        GameManager.instance.LoadScene(1); // Mulai dari Level 1
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PlayBGM(0);
    } 
}
