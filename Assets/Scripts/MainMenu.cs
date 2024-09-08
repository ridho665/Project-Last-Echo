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
    [SerializeField] private GameObject quitConfirmationPanel; // Panel for quit confirmation
    [SerializeField] private Button continueButton;

    private void Start()
    {
        // Enable or disable Continue button based on if there's a saved game
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            continueButton.interactable = true; // Enable if save exists
        }
        else
        {
            continueButton.interactable = false; // Disable if no save exists
        }

        // Ensure the quit confirmation panel is not visible at start
        quitConfirmationPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            string savedLevel = PlayerPrefs.GetString("SavedLevel");
            StartCoroutine(LoadLevel(savedLevel));
        }
        else
        {
            Debug.Log("No saved game found.");
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("SavedLevel"); // Optional: Delete saved level if starting new game
        StartCoroutine(LoadLevel("Level1"));
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

    // Open the quit confirmation panel
    public void QuitGame()
    {
        quitConfirmationPanel.SetActive(true); // Show quit confirmation panel
    }

    // If "Yes" is clicked on quit confirmation
    public void ConfirmQuit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    // If "No" is clicked on quit confirmation
    public void CancelQuit()
    {
        quitConfirmationPanel.SetActive(false); // Hide quit confirmation panel
    }
}
