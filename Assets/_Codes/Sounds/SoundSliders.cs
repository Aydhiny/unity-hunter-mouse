using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliders : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;
    public float minVolume = 0f;
    public float maxVolume = 1f;
    public string musicTag = "musicSrc"; // Tag for music audio sources

    private void Start()
    {
        // Initialize sliders' values based on the current volume levels
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f); // Load saved sound volume
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f); // Load saved music volume

        SetSoundVolume(soundSlider.value);
        SetMusicVolume(musicSlider.value);
    }

    public void SetSoundVolume(float volume)
    {
        // Clamp the volume value between minVolume and maxVolume
        volume = Mathf.Clamp(volume, minVolume, maxVolume);

        // Update sound volume for all audio sources in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (!source.CompareTag(musicTag))
                source.volume = volume;
        }

        // Save the sound volume setting
        PlayerPrefs.SetFloat("SoundVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        // Clamp the volume value between minVolume and maxVolume
        volume = Mathf.Clamp(volume, minVolume, maxVolume);

        // Update music volume for all audio sources with the specified tag
        GameObject[] musicSources = GameObject.FindGameObjectsWithTag(musicTag);
        foreach (GameObject sourceObj in musicSources)
        {
            AudioSource source = sourceObj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = volume;
            }
        }

        // Save the music volume setting
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}