using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton pattern untuk GameManager
    private int currentLevel = 1; // Level yang sedang dimainkan (default mulai dari level 1)

    private void Awake()
    {
        // Pastikan hanya ada satu instance GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // GameManager tetap ada saat berganti scene
        }
        else
        {
            Destroy(gameObject); // Hancurkan jika ada lebih dari satu GameManager
        }

        // Load level terakhir yang tersimpan
        LoadLevel();
    }

    // Panggil fungsi ini untuk pindah ke level berikutnya
    public void NextLevel()
    {
        currentLevel++; // Naik ke level berikutnya
        SaveLevel(); // Simpan level baru
        LoadScene(currentLevel); // Pindah ke scene level baru
    }

    // Fungsi untuk memuat scene berdasarkan level
    public void LoadScene(int level)
    {
        string sceneName = "Level" + level; // Sesuaikan penamaan scene (misalnya, Level1, Level2, dst.)
        SceneManager.LoadScene(sceneName);
    }


    // Fungsi untuk menyimpan level saat ini
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("SavedLevel", currentLevel); // Simpan level ke PlayerPrefs
        PlayerPrefs.Save(); // Pastikan data tersimpan
        Debug.Log("Level " + currentLevel + " telah disimpan.");
    }

    // Fungsi untuk memuat level yang tersimpan
    public void LoadLevel()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("SavedLevel"); // Load level yang tersimpan
            Debug.Log("Level " + currentLevel + " dimuat.");
        }
        else
        {
            currentLevel = 1; // Default ke level 1 jika belum ada data tersimpan
            Debug.Log("Tidak ada level tersimpan, memulai dari level 1.");
        }
    }

    // Reset progress game (misalnya, jika ingin memulai dari awal)
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("SavedLevel"); // Hapus data level tersimpan
        currentLevel = 1; // Reset ke level awal
        Debug.Log("Progress telah di-reset, kembali ke level 1.");
    }

    // Fungsi untuk melanjutkan ke level terakhir yang disimpan
    public void ContinueGame()
    {
        // Cek apakah ada level yang tersimpan
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("SavedLevel"); // Ambil level terakhir yang disimpan
            Debug.Log("Melanjutkan game di level " + currentLevel);
            LoadScene(currentLevel); // Pindah ke level yang tersimpan
        }
        else
        {
            Debug.Log("Tidak ada game yang tersimpan, mulai dari level 1.");
            LoadScene(1); // Jika tidak ada save, mulai dari level 1
        }
    }

    public void LoadSavedLevel()
    {
        // Ambil level yang disimpan, default ke level 1 jika belum ada yang disimpan
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        string sceneName = "Level" + savedLevel; // Asumsikan penamaan scene adalah Level1, Level2, dst.
        SceneManager.LoadScene(sceneName);
        Debug.Log("Memuat level " + savedLevel);
    }
}
