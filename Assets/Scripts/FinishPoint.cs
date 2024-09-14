using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject transitionOut; // Referensi ke GameObject transition out
    [SerializeField] private float transitionDelay = 1f; // Waktu tunggu sebelum pindah ke scene berikutnya
    [SerializeField] private string nextSceneName = "Level2"; // Nama scene tujuan (Level 2)
    [SerializeField] private int nextLevelIndex = 2;

    private bool playerReachedFinish = false;

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah yang menyentuh FinishPoint adalah Player
        if (other.CompareTag("Player") && !playerReachedFinish)
        {
            playerReachedFinish = true; // Hindari trigger berkali-kali
            StartCoroutine(HandleLevelTransition()); // Mulai transisi level
        }
    }

    private IEnumerator HandleLevelTransition()
    {
        // Aktifkan animasi atau GameObject transition out
        if (transitionOut != null)
        {
            transitionOut.SetActive(true);
        }

        // Tunggu sesuai delay sebelum pindah ke scene berikutnya
        yield return new WaitForSeconds(transitionDelay);

        LightSeed lightSeed = FindObjectOfType<LightSeed>();
        if (lightSeed != null)
        {
            PlayerPrefs.SetFloat("LIGHT_RANGE_KEY", lightSeed.GetLightRange());
            PlayerPrefs.Save();
            Debug.Log("LightSeed range tersimpan: " + lightSeed.GetLightRange());
        }

        PlayerPrefs.SetInt("SavedLevel", nextLevelIndex);
        PlayerPrefs.Save();
        Debug.Log("Level " + nextLevelIndex + " tersimpan.");

        // Pindah ke scene berikutnya (Level 2)
        SceneManager.LoadScene(nextSceneName);
        AudioManager.instance.PlayBGM(2);
    }
}
