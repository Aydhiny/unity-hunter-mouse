using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public Image CreditScreen;
    public Image finishGame;
    public Image backtoMenuBtn;
    public Image loadingPanel;
    public TextMeshProUGUI score;

    public AudioClip hoverSound;
    public AudioClip clickSound;

    public AudioSource audioSource;
    private Vector3 originalScale;
    private const string coinCountKey = "CoinCount";


    private void Start()
    {
        originalScale = finishGame.transform.localScale;
        loadingPanel.enabled = false;
        score.enabled = false;
        CreditScreen.enabled = false;
        backtoMenuBtn.enabled = false;
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void FinishButtonHover()
    {
        // Enlarge the button and play hover sound
        // You can use Vector3.Scale or change RectTransform properties
        finishGame.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        PlaySound(hoverSound);
    }
    public void FinishButtonHoverExit()
    {
        // Restore the button to its original size
        finishGame.transform.localScale = originalScale;
    }
    public void FinishButtonClick()
    {
        score.enabled = true;
        CreditScreen.enabled = true;
        backtoMenuBtn.enabled = true;
        DisplayHighScore();
    }

    public void BackButtonHover()
    {
        // Enlarge the button and play hover sound
        // You can use Vector3.Scale or change RectTransform properties
        backtoMenuBtn.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        PlaySound(hoverSound);
    }
    public void BackButtonHoverExit()
    {
        // Restore the button to its original size
        backtoMenuBtn.transform.localScale = originalScale;
    }
    public void BackButtonClick() 
    {
        loadingPanel.enabled = true;
        PlaySound(clickSound);
        SceneManager.LoadScene("MainMenu");
    }
    private void DisplayHighScore()
    {
        if (PlayerPrefs.HasKey(coinCountKey))
        {
            int highScore = PlayerPrefs.GetInt(coinCountKey);
            score.text = highScore.ToString();
        }
        else
        {
            score.text = "0";
        }
    }
}

