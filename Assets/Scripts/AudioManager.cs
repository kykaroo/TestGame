using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    
    private void Start()
    {
        PlayMusic("Theme");
    }

    private void PlayMusic(string musicName)
    {
        var s = Array.Find(musicSounds, x => x.name == musicName);

        if (s == null)
        {
            print($"Sound {s} not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string sfxName)
    {
        var s = Array.Find(sfxSounds, x => x.name == sfxName);

        if (s == null)
        {
            print($"SFX {s} not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void ToggleMusic(bool musicToggle)
    {
        musicSource.mute = !musicToggle;
    }

    public void ToggleSfx(bool sfxToggle)
    {
        sfxSource.mute = !sfxToggle;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
