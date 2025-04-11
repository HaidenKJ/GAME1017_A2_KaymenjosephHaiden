using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider masterVolumeSlider;

    private float savedMusicVolume = 1f;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Save the initial music volume at the start
        savedMusicVolume = SoundManager.GetMusicVolume();
        Debug.Log("[VolumeControl] Initial saved music volume: " + savedMusicVolume); // Debug log

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
        string sceneName = scene.name;

        if (sceneName == "EndSceneA2")
        {
            Debug.Log("[VolumeControl] Muting music for EndSceneA2");
            SoundManager.SetMusicVolume(0f);

            if (musicVolumeSlider != null)
                musicVolumeSlider.value = 0f;
        }
        else if (sceneName == "StartSceneA2" || sceneName == "GameSceneA2")
        {
            Debug.Log("[VolumeControl] Restoring music volume for: " + sceneName);

            // Delay restoring the music volume to ensure the scene is fully loaded
            Invoke(nameof(RestoreMusicVolume), 0.1f); // Small delay to allow scene changes
        }
    }

    private void RestoreMusicVolume()
    {
        Debug.Log("[VolumeControl] Restoring music volume to: " + savedMusicVolume); // Debug log to check volume restoration
        SoundManager.SetMusicVolume(savedMusicVolume);

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = savedMusicVolume;
    }

    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.SetSFXVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.SetMusicVolume(value);
        savedMusicVolume = value; // Update saved volume if the user changes it
    }

    public void OnMasterVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
