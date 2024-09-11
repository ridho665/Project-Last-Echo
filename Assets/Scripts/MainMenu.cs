using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject transitionObject;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Button continueButton;

    // public void ContinueGame()
    // {
    //     if (PlayerPrefs.HasKey("SavedLevel"))
    //     {
    //         string savedLevel = PlayerPrefs.GetString("SavedLevel");
    //         StartCoroutine(LoadLevel(savedLevel));
    //     }
    // }


    // public void StartNewGame()
    // {
    //     StartCoroutine(LoadLevel("Level1"));
    // }

    private void Start()
    {
        // Pastikan tombol Continue hanya aktif jika ada level yang tersimpan
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void OnContinuePressed()
    {
        GameManager.instance.LoadSavedLevel();
    }

    public void OnStartNewGame()
    {
        GameManager.instance.ResetProgress(); // Reset progress dan mulai dari level 1
        GameManager.instance.LoadScene(1); // Mulai dari Level 1
    }

    public void OnContinueGame()
    {
        GameManager.instance.ContinueGame(); // Lanjutkan game dari level terakhir yang disimpan
    }

    private IEnumerator LoadLevel(string levelName)
    {
        transitionObject.SetActive(true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
