using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private RectTransform batteryBar; // Referensi ke RectTransform dari UI Battery Bar

    private PlayerBattery playerBattery;

    private void Start()
    {
        PlayerManager.instance.inGameUI = this;
        // // Ambil referensi ke PlayerBattery dari PlayerManager
        // playerBattery = PlayerManager.instance.GetPlayerBattery();

        // // Set RectTransform batteryBar di PlayerBattery
        // if (playerBattery != null && batteryBar != null)
        // {
        //     playerBattery.SetBatteryBar(batteryBar); // Hubungkan batteryBar UI dengan PlayerBattery
        // }
    }

    private void Update()
    {
        UpdateInGameInfo();
    }

    private void UpdateInGameInfo()
    {
        playerBattery = PlayerManager.instance.GetPlayerBattery();
    }   
}
