using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector] public Transform respawnPoint;
    [HideInInspector] public GameObject currentPlayer;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CameraManager cameraManager;
    
    private PlayerBattery playerBattery;

    public InGameUI inGameUI;



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // public void RespawnPlayer()
    // {
    //     if (currentPlayer == null)
    //     {
    //         currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    //     }

    //     // Setelah respawn, perbarui kamera agar mengikuti pemain baru
    //     if (cameraManager != null)
    //     {
    //         cameraManager.SetCameraFollow(currentPlayer.transform);
    //     }
        
    // }

    public void RespawnPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        }

        if (cameraManager != null)
        {
            cameraManager.SetCameraFollow(currentPlayer.transform);
        }

        // currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        playerBattery = currentPlayer.GetComponent<PlayerBattery>();
    }

    public void KillPlayer()
    {
        // Setelah pemain mati, kamera harus berhenti mengikuti
        if (cameraManager != null)
        {
            cameraManager.ResetCameraFollow(); // Buat fungsi ini di CameraManager
        }

        Destroy(currentPlayer);
        Invoke("RespawnPlayer", 1); // Respawn setelah 1 detik
    }

    public PlayerBattery GetPlayerBattery()
    {
        return playerBattery;
    }
}
