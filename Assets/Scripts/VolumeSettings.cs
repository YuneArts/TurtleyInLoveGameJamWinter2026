using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private const string VolumePrefKey = "master_volume";

    private void Awake()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("VolumeSettings: Slider not assigned.", this);
            return;
        }

        // Load saved value (default 0.8)
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.8f);
        savedVolume = Mathf.Clamp01(savedVolume);

        volumeSlider.SetValueWithoutNotify(savedVolume);
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        value = Mathf.Clamp01(value);

        // Global master volume
        AudioListener.volume = value;

        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }
}
