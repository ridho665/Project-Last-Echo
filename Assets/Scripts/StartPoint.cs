using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform respPoint;
    [SerializeField] private float activationDelay = 2f; // Waktu delay sebelum mengaktifkan PlayerController dan PlayerBattery
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera; // Referensi ke Cinemachine Virtual Camera
    [SerializeField] private RectTransform batteryBarUI;

    private void Awake()
    {
        PlayerManager.instance.respawnPoint = respPoint;
        PlayerManager.instance.RespawnPlayer();

        // Atur Cinemachine Camera untuk mengikuti pemain setelah respawn
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = PlayerManager.instance.currentPlayer.transform;
        }

        // Nonaktifkan PlayerController dan PlayerBattery untuk sementara
        PlayerManager.instance.currentPlayer.GetComponent<PlayerController>().enabled = false;
        PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>().enabled = false;

        PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>().InitializeBattery(batteryBarUI);

        // Jalankan coroutine untuk mengaktifkan setelah delay
        StartCoroutine(ActivatePlayerComponentsAfterDelay());

        // PlayerBattery playerBattery = PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>();

        // if (playerBattery != null && batteryBarUI != null)
        // {
        //     // Inisialisasi battery dengan UI battery bar setelah respawn
        //     playerBattery.InitializeBattery(batteryBarUI);
        // }
    }

    // Coroutine untuk menunggu delay dan mengaktifkan PlayerController dan PlayerBattery
    private IEnumerator ActivatePlayerComponentsAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay); // Tunggu sesuai waktu delay

        // Aktifkan PlayerController dan PlayerBattery
        PlayerManager.instance.currentPlayer.GetComponent<PlayerController>().enabled = true;
        PlayerManager.instance.currentPlayer.GetComponent<PlayerBattery>().enabled = true;
    }
}
