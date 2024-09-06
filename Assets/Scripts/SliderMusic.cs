using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        if (volumeSlider != null)
        {
            volumeSlider.value = VolumeManager.GetMasterVolume();
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    private void ChangeVolume(float volume)
    {
        VolumeManager.SetMasterVolume(volume);
        // Atur volume game sesuai nilai slider
        AudioListener.volume = volume;
    }
}