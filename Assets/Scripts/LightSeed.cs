using System.Collections;
using UnityEngine;

public class LightSeed : MonoBehaviour
{
    [SerializeField] private Light lightSource; // Referensi ke sumber cahaya dari biji
    [SerializeField] private float drainRate = 1f; // Laju pengurangan cahaya per detik
    [SerializeField] private float rechargeAmount = 2f; // Jumlah pengisian cahaya saat menyentuh air atau cahaya matahari
    [SerializeField] private float maxLightRange = 10f; // Batas maksimum range cahaya
    [SerializeField] private float maxLightIntensity = 7f; // Batas maksimum intensitas cahaya
    [SerializeField] private float flickerSpeed = 0.05f; // Kecepatan berkedip
    [SerializeField] private float flickerAmount = 0.5f; // Besarnya intensitas berkedip

    private bool isTouchingWaterOrSunlight = false; // Status jika biji sedang menyentuh air atau cahaya matahari
    private bool isFlickering = false; // Status apakah biji sedang berkedip

    private void Start()
    {
        // Jika lightSource tidak diset di Inspector, coba cari Light yang ada di object ini
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }
    }

    private void Update()
    {
        // Jika tidak menyentuh air atau cahaya matahari, redupkan cahaya
        if (!isTouchingWaterOrSunlight)
        {
            DimLight();
        }

        // Aktifkan efek flicker jika intensitas cahaya mencapai 5
        if (lightSource.range <= 5f && !isFlickering)
        {
            StartCoroutine(FlickerLight());
        }
    }

    // Mengurangi intensitas cahaya setiap detik
    private void DimLight()
    {
        if (lightSource.range > 0f)
        {
            lightSource.range -= drainRate * Time.deltaTime;
            lightSource.range = Mathf.Clamp(lightSource.range, 0f, maxLightRange); // Batasi agar tidak kurang dari 0
        }
    }

    // Mengisi cahaya biji saat menyentuh air atau cahaya matahari
    private void RechargeLight()
    {
        lightSource.range += rechargeAmount;
        lightSource.range = Mathf.Clamp(lightSource.range, 0f, maxLightRange); // Batasi agar tidak lebih dari maxLightRange
        
        // Membatasi intensitas cahaya maksimal 7f
        lightSource.intensity += rechargeAmount;
        lightSource.intensity = Mathf.Clamp(lightSource.intensity, 0f, maxLightIntensity); // Batasi agar tidak lebih dari maxLightIntensity
    }

    public void ReduceLightRange(float amount)
    {
        // Kurangi range cahaya
        lightSource.range -= amount;
        lightSource.range = Mathf.Clamp(lightSource.range, 0f, maxLightRange); // Jaga agar tidak kurang dari 0
    }

    // Efek berkedip (flicker) saat cahaya rendah
    private IEnumerator FlickerLight()
    {
        isFlickering = true;
        
        while (lightSource.range <= 5f)
        {
            // Ubah intensitas cahaya secara acak
            float randomFlicker = Random.Range(-flickerAmount, flickerAmount);
            lightSource.intensity += randomFlicker;
            lightSource.intensity = Mathf.Clamp(lightSource.intensity, 0f, maxLightIntensity); // Jaga agar intensitas tetap dalam batas
            
            // Tunggu sebentar sebelum mengubah kembali
            yield return new WaitForSeconds(flickerSpeed);
        }

        // Hentikan flickering jika range cahaya sudah lebih besar dari 5
        isFlickering = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jika menyentuh air
        if (other.CompareTag("Water"))
        {
            isTouchingWaterOrSunlight = true;
            RechargeLight(); // Isi cahaya
        }

        // Jika menyentuh cahaya matahari (tag "Sunlight")
        if (other.CompareTag("Sunlight"))
        {
            isTouchingWaterOrSunlight = true;
            RechargeLight(); // Isi cahaya
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Terus mengisi cahaya selama menyentuh air atau cahaya matahari
        if (other.CompareTag("Water") || other.CompareTag("Sunlight"))
        {
            RechargeLight(); // Isi cahaya selama kontak
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Saat tidak lagi menyentuh air atau cahaya matahari
        if (other.CompareTag("Water") || other.CompareTag("Sunlight"))
        {
            isTouchingWaterOrSunlight = false;
        }
    }
}
