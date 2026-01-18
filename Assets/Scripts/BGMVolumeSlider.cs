using UnityEngine;
using UnityEngine.UI;

public class BGMVolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.SetVolume(value);
        }
    }
}
