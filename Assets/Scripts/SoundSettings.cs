using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("GameMusic"))
        {
            PlayerPrefs.SetFloat("GameMusic", 0.5f); // Set default volume
        }

        Load();
    }

    public void ChangeVolume()
    {
         if (volumeSlider != null)
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("GameMusic");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("GameMusic", volumeSlider.value);
    }
}
