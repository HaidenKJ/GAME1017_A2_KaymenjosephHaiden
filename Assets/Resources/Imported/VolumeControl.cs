using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider masterVolumeSlider;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Initialize sliders with current volume levels
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = SoundManager.GetSFXVolume();
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = SoundManager.GetMusicVolume();
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = AudioListener.volume;
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "EndSceneA2") // <- Your target scene
        {
            Debug.Log("[VolumeControl] Muting music due to scene: EndSceneA2");
            SoundManager.SetMusicVolume(0f);

            if (musicVolumeSlider != null)
                musicVolumeSlider.value = 0f;
        }
    }

    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.SetSFXVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.SetMusicVolume(value);
    }

    public void OnMasterVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
