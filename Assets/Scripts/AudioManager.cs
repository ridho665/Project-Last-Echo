using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private int bgmIndex = -1; // Inisialisasi dengan nilai default yang tidak valid

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);    

        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start() 
    {
        // Optionally, play a default BGM or handle it via scene-loaded event
        PlayBGMByScene();  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMByScene();
    }

    private void Update() 
    {
        if (bgm.Length > 0 && bgmIndex >= 0 && bgmIndex < bgm.Length && !bgm[bgmIndex].isPlaying)
        {
            bgm[bgmIndex].Play();    
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay >= 0 && sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].Play();
        }
    }

    public void StopSFX(int sfxToStop)
    {
        if (sfxToStop >= 0 && sfxToStop < sfx.Length)
        {
            sfx[sfxToStop].Stop();
        }
    }

    public void PlayBGM(int bgmToPlay)
    {
        if (bgmToPlay >= 0 && bgmToPlay < bgm.Length)
        {
            // Stop all BGM tracks
            for (int i = 0; i < bgm.Length; i++)
            {
                if (bgm[i] != null)
                {
                    bgm[i].Stop();
                }
            }

            // Play the selected BGM
            if (bgm[bgmToPlay] != null)
            {
                bgmIndex = bgmToPlay;
                bgm[bgmToPlay].Play();
            }
        }
    }

    private void PlayBGMByScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene Index: " + sceneIndex);
        // Map scene index to BGM index
        switch (sceneIndex)
        {
            case 0: // Main Menu
                PlayBGM(0); // BGM for Main Menu
                break;
            case 1: // Level 1
                PlayBGM(1); // BGM for Level 1
                break;
            case 2: // Level 2
                PlayBGM(2); // BGM for Level 2
                break;
            case 3:
                PlayBGM(3);
                break;
            default:
                PlayBGM(0); // Default BGM
                break;
        }
    }
}
