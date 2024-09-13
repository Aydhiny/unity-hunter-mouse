using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets._Codes.Misc
{
    public class MenuScript : MonoBehaviour
    {
        public Image playButton;
        public Image settingsButton;
        public Image quitButton;
        public Image background;
        public Image logo;

        public Image helpButton; // New help button reference
        public Image backButton; // New back button reference
        public Image helpScreen; // New help screen reference

        public AudioClip hoverSound;
        public AudioClip clickSound;

        public AudioSource audioSource;
        private Vector3 originalScale;

        //SETTINGS

        public Image settingsPanel;
        public Slider musicVolumeSlider;
        public Slider soundVolumeSlider;
        public Toggle vsyncToggle;

        public Image applyButton;

        // OPENING
        public Image opening;
        public Image openingBtn;
        public Image backMenuBtn;

        private void Start()
        {
            backButton.enabled = false;
            helpScreen.enabled = false;
            settingsPanel.enabled = false;
            musicVolumeSlider.gameObject.SetActive(false);
            soundVolumeSlider.gameObject.SetActive(false);
            vsyncToggle.gameObject.SetActive(false);
            applyButton.enabled = false;
            originalScale = playButton.transform.localScale;

            //OPENING
            opening.enabled = false;
            openingBtn.enabled = false;
            backMenuBtn.enabled = false;
        }

        public void PlayButtonHover()
        {
            // Enlarge the button and play hover sound
            // You can use Vector3.Scale or change RectTransform properties
            playButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }
        public void PlayButtonHoverExit()
        {
            // Restore the button to its original size
            playButton.transform.localScale = originalScale;
        }

        public void SettingsButtonHover()
        {
            // Enlarge the button and play hover sound
            settingsButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }
        public void SettingsButtonHoverExit()
        {
            // Restore the button to its original size
            settingsButton.transform.localScale = originalScale;
        }

        public void QuitButtonHover()
        {
            // Enlarge the button and play hover sound
            quitButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            //quitButton.rectTransform.sizeDelta = new Vector2(150f, 150f);
            PlaySound(hoverSound);
        }
        public void QuitButtonHoverExit()
        {
            // Restore the button to its original size
            quitButton.transform.localScale = originalScale;
        }

        public void PlayButtonClick()
        {
            opening.enabled = true;
            openingBtn.enabled = true;
            backMenuBtn.enabled = true;
        }

        public void BackMenuButtonClick()
        {
            opening.enabled = false;
            openingBtn.enabled = false;
            backMenuBtn.enabled = false;
        }

        public void BackMenuButtonHover()
        {
            // Enlarge the button and play hover sound
            // You can use Vector3.Scale or change RectTransform properties
            backMenuBtn.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }
        public void BackMenuButtonHoverExit()
        {
            // Restore the button to its original size
            backMenuBtn.transform.localScale = originalScale;
        }

        // OPENING
        public void StartButtonClick() 
        {
            PlaySound(clickSound);
            SceneManager.LoadScene("OutsideHouse");
        }

        public void StartButtonHover()
        {
            // Enlarge the button and play hover sound
            // You can use Vector3.Scale or change RectTransform properties
            openingBtn.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }
        public void StartButtonHoverExit()
        {
            // Restore the button to its original size
            openingBtn.transform.localScale = originalScale;
        }

        public void QuitButtonClick()
        {
            PlaySound(clickSound);
            // Quit the application
            Application.Quit();
        }

        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        // HELP AND BACK
        public void HelpButtonClick()
        {
            // Hide other buttons and show help screen with back button
            logo.enabled = false;
            playButton.enabled = false;
            settingsButton.enabled = false;
            quitButton.enabled = false;
            helpButton.enabled = false;
            backButton.enabled = true;
            helpScreen.enabled = true;
        }

        public void BackButtonClick()
        {
            // Hide help screen and back button, show main menu buttons
            logo.enabled = true;
            playButton.enabled = true;
            settingsButton.enabled = true;
            quitButton.enabled = true;
            helpButton.enabled = true;
            backButton.enabled = false;
            helpScreen.enabled = false;
            settingsPanel.enabled = false;
            musicVolumeSlider.gameObject.SetActive(false);
            soundVolumeSlider.gameObject.SetActive(false);
            vsyncToggle.gameObject.SetActive(false);
            applyButton.enabled = false;
        }

        // HOVER 
        public void HelpButtonHover()
        {
            helpButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }

        public void HelpButtonHoverExit()
        {
            helpButton.transform.localScale = originalScale;
        }

        public void BackButtonHover()
        {
            backButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }

        public void BackButtonHoverExit()
        {
            backButton.transform.localScale = originalScale;
        }


        // SETTINGS

        public void SettingsButtonClick()
        {
            PlaySound(clickSound);
            // Hide main menu UI and show settings UI
            logo.enabled = false;
            playButton.enabled = false;
            settingsButton.enabled = false;
            quitButton.enabled = false;
            helpButton.enabled = false;
            backButton.enabled = true;
            settingsPanel.enabled = true;
            musicVolumeSlider.gameObject.SetActive(true);
            soundVolumeSlider.gameObject.SetActive(true);
            vsyncToggle.gameObject.SetActive(true);
            applyButton.enabled = true;
        }

        public void SaveSettings()
        {
            // Save settings to PlayerPrefs or other storage mechanism
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            PlayerPrefs.SetFloat("SoundVolume", soundVolumeSlider.value);
            PlayerPrefs.SetInt("VSyncEnabled", vsyncToggle.isOn ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void LoadSettings()
        {
            // Load settings from PlayerPrefs or other storage mechanism
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
            vsyncToggle.isOn = PlayerPrefs.GetInt("VSyncEnabled", 1) == 1;
        }

        public void ApplySettings()
        {
            AudioListener.volume = soundVolumeSlider.value; 
            
            GameObject[] musicSources = GameObject.FindGameObjectsWithTag("musicSrc");
            foreach (GameObject sourceObj in musicSources)
            {
                AudioSource source = sourceObj.GetComponent<AudioSource>();
                if (source != null)
                {
                    source.volume = musicVolumeSlider.value;
                }
            }
            QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;

            SaveSettings();
        }

        public void ApplyButtonHover()
        {
            // Enlarge the button and play hover sound
            // You can use Vector3.Scale or change RectTransform properties
            applyButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            PlaySound(hoverSound);
        }
        public void ApplyButtonHoverExit()
        {
            // Restore the button to its original size
            applyButton.transform.localScale = originalScale;
        }
    }
}