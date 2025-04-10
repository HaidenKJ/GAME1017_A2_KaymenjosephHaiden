using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource
    [SerializeField] private AudioClip sceneChangeSFX; // Sound effect clip

    public void ChangeSceneTo(int sceneIndexToLoad)
    {
        if (audioSource != null && sceneChangeSFX != null)
        {
            audioSource.PlayOneShot(sceneChangeSFX); // Play the SFX
        }

        // Call the static method from the SceneManagerHelper class
        SceneLoader.LoadSceneByIndex(sceneIndexToLoad);
    }
}