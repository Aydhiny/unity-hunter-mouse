using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public Image returnButton;
    public pauseMenu pause;
    public Image settingsButton;
    public Image mainMenuButton;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioSource audioSource;
    private Vector3 originalScale;

    public string sceneToLoad;
    public GameObject loadScreen;

    // SETTINGS
    public Image backButton;
    public Image applyButton;
    public GameObject settingScreen;

    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    public Toggle vsyncToggle;

    private void Start()
    {
        originalScale = returnButton.transform.localScale;
        settingScreen.gameObject.SetActive(false);


        //SETTINGS

        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // Default value: 0.5f
        float savedSoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f); // Default value: 0.5f

        // Load VSync setting from PlayerPrefs
        bool savedVSyncEnabled = PlayerPrefs.GetInt("VSyncEnabled", 1) == 1; // Default value: true

        // Update music volume slider value
        musicVolumeSlider.value = savedMusicVolume;

        // Update sound volume slider value
        soundVolumeSlider.value = savedSoundVolume;

        // Update VSync toggle value
        vsyncToggle.isOn = savedVSyncEnabled;
    }

    public void ReturnButtonHover()
    {
       returnButton.transform.localScale = new Vector3(0.07f, 0.10f, 0.07f);
       PlaySound(hoverSound);
    }

    public void ReturnButtonHoverExit()
    {
        returnButton.transform.localScale = originalScale;
    }

    public void SettingsButtonHover()
    {
        settingsButton.transform.localScale = new Vector3(0.07f, 0.10f, 0.07f);
        PlaySound(hoverSound);
    }

    public void SettingsButtonHoverExit()
    {
        settingsButton.transform.localScale = originalScale;
    }

    public void MainMenuButtonHover()
    {
        mainMenuButton.transform.localScale = new Vector3(0.07f, 0.10f, 0.07f);
        PlaySound(hoverSound);
    }

    public void MainMenuButtonHoverExit()
    {
        mainMenuButton.transform.localScale = originalScale;
    }

    public void ReturnToGame()
    {
        pause.Continue();
    }

    public void OpenSettings()
    {
        musicVolumeSlider.gameObject.SetActive(true);
        soundVolumeSlider.gameObject.SetActive(true);
        vsyncToggle.gameObject.SetActive(true);
        applyButton.enabled = true;
        settingScreen.gameObject.SetActive(true);

        returnButton.enabled = false;
        settingsButton.enabled = false;
        mainMenuButton.enabled = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        StartCoroutine(LoadMainMenuAsync(sceneToLoad));

    }

    IEnumerator LoadMainMenuAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadScreen.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void BackButtonClick()
    {
        musicVolumeSlider.gameObject.SetActive(false);
        soundVolumeSlider.gameObject.SetActive(false);
        vsyncToggle.gameObject.SetActive(false);
        applyButton.enabled = false;

        returnButton.enabled = true;
        settingsButton.enabled = true;
        mainMenuButton.enabled = true;
        settingScreen.gameObject.SetActive(false);
    }

    public void BackButtonHover()
    {
        backButton.transform.localScale = new Vector3(0.07f, 0.10f, 0.07f);
        PlaySound(hoverSound);
    }

    public void BackButtonHoverExit()
    {
        backButton.transform.localScale = originalScale;
    }
    //VOLUME

    public void MusicVolumeChanged(float volume)
    {
        GameObject[] musicSources = GameObject.FindGameObjectsWithTag("musicSrc");
        foreach (GameObject sourceObj in musicSources)
        {
            AudioSource source = sourceObj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = volume;
            }
        }

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SoundVolumeChanged(float volume)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (!source.CompareTag("musicSrc"))
            {
                source.volume = volume;
            }
        }

        PlayerPrefs.SetFloat("SoundVolume", volume);
        PlayerPrefs.Save();
    }

    public void ApplySettings()
    {
        float musicVolume = musicVolumeSlider.value;
        AudioListener.volume = musicVolume;

        // Apply sound volume setting
        float soundVolume = soundVolumeSlider.value;
        audioSource.volume = soundVolume;

        // Apply VSync setting
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;

        // Save settings
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.SetInt("VSyncEnabled", vsyncToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ApplyButtonHover()
    {
        applyButton.transform.localScale = new Vector3(0.07f, 0.10f, 0.07f);
        PlaySound(hoverSound);
    }

    public void ApplyButtonHoverExit()
    {
        applyButton.transform.localScale = originalScale;
    }
}
