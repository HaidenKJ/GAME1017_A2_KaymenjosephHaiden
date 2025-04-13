using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public AudioClip manualMusic; // Assign manually if needed

    void Awake()
    {
        // Add Hellwalker and start playing it in a loop
        // SoundManager.AddSound("Hellwalker", Resources.Load<AudioClip>("hellwalker"), SoundType.SOUND_MUSIC);

        // Optionally, use manually assigned music
        if (manualMusic != null)
        {
            SoundManager.PlayMusic(manualMusic); 
        }
    }

    void Start()
    {

        // SoundManager.AddSound("PGC", Resources.Load<AudioClip>("Pistol Gun Cocking Sound Effects"), SoundType.SOUND_SFX);
        // SoundManager.AddSound("Collision", Resources.Load<AudioClip>("HalfLife1Crowbarhitonce"), SoundType.SOUND_SFX);
        // SoundManager.AddSound("RestartDing", Resources.Load<AudioClip>("ding-sfx"), SoundType.SOUND_SFX);
        // SoundManager.AddSound("LVLUP", Resources.Load<AudioClip>("LVLUP"), SoundType.SOUND_SFX);
    }
}
