using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPause : MonoBehaviour
{
    public GameObject pausePanel;
    public AudioSource mainMusic;
    public AudioSource pauseMenuMusic;
    public string nonMusicTag = "NonMusic";

    private List<AudioSource> allAudioSources = new List<AudioSource>();
    private List<AudioSource> nonMusicAudioSources = new List<AudioSource>();
    private bool isMusicPlayingBeforePause;

    private void Start()
    {
        pauseMenuMusic.enabled = false;
        pausePanel.SetActive(false);
        AudioSource[] foundAudioSources = FindObjectsOfType<AudioSource>();
        allAudioSources.AddRange(foundAudioSources);

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != mainMusic && audioSource != pauseMenuMusic)
            {
                if (audioSource.CompareTag(nonMusicTag))
                {
                    nonMusicAudioSources.Add(audioSource);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pausePanel.activeSelf)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        pauseMenuMusic.enabled = true;
        Time.timeScale = 0;

        if (mainMusic != null)
        {
            isMusicPlayingBeforePause = mainMusic.isPlaying;
            mainMusic.Pause();
        }

        if (pauseMenuMusic != null)
        {
            pauseMenuMusic.Play();
        }

        foreach (AudioSource audioSource in nonMusicAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.mute = true;
            }
        }
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;

        if (pauseMenuMusic != null)
        {
            pauseMenuMusic.Stop();
        }

        if (mainMusic != null)
        {
            if (isMusicPlayingBeforePause)
            {
                mainMusic.Play();
            }
        }

        foreach (AudioSource audioSource in nonMusicAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.mute = false;
            }
        }
    }
    public void ReturnToGame()
    {
        Continue();
    }
}