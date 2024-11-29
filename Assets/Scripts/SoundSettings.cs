using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    private void Start()
    {
        SetValue(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    private void GetValue(float _value)
    {
        if (_value < 1)
        {
            _value = 0.001f;
        }
        RefreshSlider(_value);
    }

    private void SetValue(float _value)
    {
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        RefreshSlider(_value);
    }

    public void SetVolumeFromSlider(float _value)
    {
        SetValue(soundSlider.value);
    }

    private void RefreshSlider(float _value)
    {
        soundSlider.value = _value;
    }
}
