using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneCamera; // Referensi ke kamera cutscene
    [SerializeField] private float cutsceneDuration = 15f; // Durasi cutscene dalam detik

    private PlayerController playerController;
    private PlayerBattery playerBattery;
    private PlayerShield playerShield;
    private LightSeed lightSeed;

    private void Start()
    {
        // Cari komponen yang akan dikontrol saat cutscene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            playerBattery = player.GetComponent<PlayerBattery>();
            playerShield = player.GetComponent<PlayerShield>();
            lightSeed = player.GetComponent<LightSeed>();
        }

        // Nonaktifkan kamera cutscene di awal
        if (cutsceneCamera != null)
        {
            cutsceneCamera.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Mulai cutscene jika player masuk trigger
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartCutscene());
        }
    }

    private IEnumerator StartCutscene()
    {
        // Nonaktifkan pergerakan player tanpa menonaktifkan PlayerController
        if (playerController != null) playerController.canMove = false;
        if (playerBattery != null) playerBattery.enabled = false;
        if (playerShield != null) playerShield.enabled = false;
        if (lightSeed != null) lightSeed.enabled = false;

        // Aktifkan kamera cutscene
        if (cutsceneCamera != null)
        {
            cutsceneCamera.SetActive(true);
        }

        // Tunggu selama durasi cutscene
        yield return new WaitForSeconds(cutsceneDuration);

        // Aktifkan kembali pergerakan player setelah cutscene selesai
        if (playerController != null) playerController.canMove = true;
        if (playerBattery != null) playerBattery.enabled = true;
        // if (playerShield != null) playerShield.enabled = true;
        if (lightSeed != null) lightSeed.enabled = true;

        // Nonaktifkan kamera cutscene
        if (cutsceneCamera != null)
        {
            cutsceneCamera.SetActive(false);
        }

        Destroy(gameObject);
    }
}
