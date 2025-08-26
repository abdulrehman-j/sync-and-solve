using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    private const float defaultVolume = 1f;

    void Awake()
    {
        // Only set to default ONCE per app launch, not every time settings open
        if (!PlayerPrefs.HasKey("HasGameStarted"))
        {
            AudioListener.volume = defaultVolume;
            PlayerPrefs.SetInt("HasGameStarted", 1); // flag that game already started
        }
        else
        {
            // Keep current volume (don’t reset it)
            AudioListener.volume = PlayerPrefs.GetFloat("GameMusic", defaultVolume);
        }

        if (volumeSlider != null)
            volumeSlider.value = AudioListener.volume;
    }

    public void ChangeVolume()
    {
        if (volumeSlider != null)
        {
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("GameMusic", volumeSlider.value);
        }
    }
}
