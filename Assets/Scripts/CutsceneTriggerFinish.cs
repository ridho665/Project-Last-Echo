using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTriggerFinish : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneCamera;
    [SerializeField] private GameObject interactText;
    [SerializeField] private float cutsceneDuration = 15f;

    private bool isPlayerInRange = false;
    private PlayerController playerController;
    private PlayerBattery playerBattery;
    private PlayerShield playerShield;
    private LightSeed lightSeed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            playerBattery = player.GetComponent<PlayerBattery>();
            playerShield = player.GetComponent<PlayerShield>();
            lightSeed = player.GetComponent<LightSeed>();
        }

        if (cutsceneCamera != null)
        {
            cutsceneCamera.SetActive(false);
        }

        interactText.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerController != null) playerController.canMove = false;
            if (playerBattery != null) playerBattery.enabled = false;
            if (playerShield != null) playerShield.enabled = false;
            if (lightSeed != null) lightSeed.enabled = false;

            AudioManager.instance.StopSFX(0);

            if (cutsceneCamera != null)
            {
                cutsceneCamera.SetActive(true);
            }

            StartCoroutine(StartCutscene());

            // Destroy(gameObject);   
        }
    }

    private IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(cutsceneDuration);

        SceneManager.LoadScene("MainMenu");
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
}
