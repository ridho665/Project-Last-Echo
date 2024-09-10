using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private RectTransform batteryBar;

    private PlayerBattery playerBattery;
    private bool gamePaused;

    private void Start()
    {
        PlayerManager.instance.inGameUI = this;
        Time.timeScale = 1;
        SwitchUI(inGameUI); 
    }

    private void Update()
    {
        UpdateInGameInfo();

        if (Input.GetKeyDown(KeyCode.Escape))
            CheckIfNotPaused();
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

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);
    }  

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    } 
}
