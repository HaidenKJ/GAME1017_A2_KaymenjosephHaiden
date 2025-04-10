using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    SOUND_SFX,
    SOUND_MUSIC
}

public class SoundManager
{
    private static Dictionary<string, AudioClip> sfxDictionary;
    private static Dictionary<string, AudioClip> musicDictionary;
    private static AudioSource sfxSource;
    private static AudioSource musicSource;

    static SoundManager()
    {
        Initialize();
    }

    private static void Initialize()
    {
        sfxDictionary = new Dictionary<string, AudioClip>();
        musicDictionary = new Dictionary<string, AudioClip>();
        
        GameObject soundManagerObject = new GameObject("SoundManager");
        UnityEngine.Object.DontDestroyOnLoad(soundManagerObject); // Persist between scenes

        sfxSource = soundManagerObject.AddComponent<AudioSource>();
        musicSource = soundManagerObject.AddComponent<AudioSource>();
        musicSource.loop = true;
    }

    public static void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
    {
        if (audioClip == null)
        {
            Debug.LogError($"Error loading AudioClip: {soundKey}.");
            return;
        }

        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                if (!sfxDictionary.ContainsKey(soundKey))
                {
                    sfxDictionary.Add(soundKey, audioClip);
                }
                else
                {
                    Debug.LogWarning($"Key: {soundKey} already found in Dictionary.");
                }
                break;
            case SoundType.SOUND_MUSIC:
                if (!musicDictionary.ContainsKey(soundKey))
                {
                    musicDictionary.Add(soundKey, audioClip);
                }
                else
                {
                    Debug.LogWarning($"Key: {soundKey} already found in Dictionary.");
                }
                break;
        }
    }

    public static void PlayMusic(string soundKey)
    {
        if (musicDictionary.ContainsKey(soundKey))
        {
            if (musicSource.clip == musicDictionary[soundKey] && musicSource.isPlaying)
                return;

            musicSource.Stop();
            musicSource.clip = musicDictionary[soundKey];
            musicSource.Play();
        }
        else
        {
            Debug.LogError($"Key: {soundKey} not found in Dictionary.");
        }
    }

    // ✅ **New method to play manually assigned AudioClip**
    public static void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("Attempted to play a null AudioClip.");
            return;
        }

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
    public static float GetSFXVolume()
    {
        return sfxSource.volume;
    }

    public static float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public static void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // Ensure volume stays between 0 and 1
    }

    public static void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume); // Ensure volume stays between 0 and 1
    }

}
