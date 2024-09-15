using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination;  // Posisi tujuan teleport
    [SerializeField] private GameObject transitionOut;       // Animasi TransitionOut
    [SerializeField] private GameObject transitionIn;        // Animasi TransitionIn
    [SerializeField] private GameObject interactText;        // Text UI untuk instruksi "Pencet Key E"
    [SerializeField] private float transitionDuration = 1f;  // Durasi animasi transisi

    private bool isPlayerInRange = false;  // Apakah player berada dalam range teleport
    private bool isTeleporting = false;    // Status apakah sedang teleport

    private void Start()
    {
        // Pastikan instruksi UI tidak ditampilkan di awa+l
        interactText.SetActive(false);
    }

    private void Update()
    {
        // Cek jika player berada di range teleport dan menekan tombol E
        if (isPlayerInRange && !isTeleporting && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Teleport());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jika player menyentuh trigger teleport
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(true);  // Tampilkan teks instruksi
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Jika player meninggalkan trigger teleport
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactText.SetActive(false); // Sembunyikan teks instruksi
        }
    }

    private IEnumerator Teleport()
    {
        isTeleporting = true;
        interactText.SetActive(false); // Sembunyikan teks instruksi

        PlayerController playerController = PlayerManager.instance.currentPlayer.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.canMove = false; // Set canMove menjadi false
        }

        // Jika ada animasi TransitionOut, aktifkan
        if (transitionOut != null)
        {
            transitionOut.SetActive(true);
        }

        // Tunggu durasi transisi
        yield return new WaitForSeconds(transitionDuration);

        // Pindahkan player ke lokasi tujuan teleport
        if (PlayerManager.instance != null && teleportDestination != null)
        {
            PlayerManager.instance.currentPlayer.transform.position = teleportDestination.position;
        }

        // Jika ada animasi TransitionIn, aktifkan
        if (transitionIn != null)
        {
            transitionOut.SetActive(false);  // Nonaktifkan TransitionOut
            transitionIn.SetActive(true);    // Aktifkan TransitionIn
        }

        // Tunggu durasi transisi setelah teleport
        yield return new WaitForSeconds(transitionDuration);

        // Nonaktifkan animasi TransitionIn setelah selesai
        if (transitionIn != null)
        {
            transitionIn.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.canMove = true; // Set canMove menjadi true
        }

        isTeleporting = false;  // Teleport selesai
    }
}
