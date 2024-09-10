using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera; // Referensi ke Cinemachine Virtual Camera

    private void Start()
    {
        PlayerManager.instance.cameraManager = this;

        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera is not assigned.");
        }
    }

    // Atur kamera untuk mengikuti pemain yang baru
    public void SetCameraFollow(Transform playerTransform)
    {
        if (cinemachineCamera != null && playerTransform != null)
        {
            cinemachineCamera.Follow = playerTransform;
        }
    }

    // Reset follow jika pemain mati
    public void ResetCameraFollow()
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = null; // Kamera tidak akan mengikuti apa pun
        }
    }
}
