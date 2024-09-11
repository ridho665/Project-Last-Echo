using System.Collections;
using UnityEngine;
using Cinemachine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform respPoint;
    [SerializeField] private float activationDelay = 2f; // Waktu delay sebelum mengaktifkan PlayerController, PlayerBattery, dan LightSeed
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera; // Referensi ke Cinemachine Virtual Camera
    [SerializeField] private RectTransform batteryBarUI;

    private void Awake()
    {
        // Set respawn point dan respawn player
        PlayerManager.instance.respawnPoint = respPoint;
        PlayerManager.instance.RespawnPlayer();

        // Atur Cinemachine Camera untuk mengikuti pemain setelah respawn
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = PlayerManager.instance.currentPlayer.transform;
        }

        // Nonaktifkan PlayerController, PlayerBattery, dan LightSeed untuk sementara
        PlayerManager.instance.currentPlayer.GetComponent<PlayerController>().enabled = false;
        PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>().enabled = false;
        PlayerManager.instance.currentPlayer.GetComponent<LightSeed>().enabled = false;

        // Inisialisasi battery bar UI
        PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>().InitializeBattery(batteryBarUI);

        // Jalankan coroutine untuk mengaktifkan komponen setelah delay
        StartCoroutine(ActivatePlayerComponentsAfterDelay());
    }

    // Coroutine untuk menunggu delay dan mengaktifkan PlayerController, PlayerBattery, dan LightSeed
    private IEnumerator ActivatePlayerComponentsAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay); // Tunggu sesuai waktu delay

        // Aktifkan PlayerController, PlayerBattery, dan LightSeed
        PlayerManager.instance.currentPlayer.GetComponent<PlayerController>().enabled = true;
        PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>().enabled = true;
        PlayerManager.instance.currentPlayer.GetComponent<LightSeed>().enabled = true;
    }
}
