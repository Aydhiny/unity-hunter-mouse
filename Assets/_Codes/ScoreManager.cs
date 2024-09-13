using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int coinCount;
    public TextMeshProUGUI coinCountText;

    private const string coinCountKey = "CoinCount";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            LoadCoinCount(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignCoinCountText();
        UpdateCoinCountText();

        if (scene.name == "OutsideHouse")
        {
            ResetCoinCount();
        }
    }

    private void AssignCoinCountText()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score");
        if (scoreObject != null)
        {
            coinCountText = scoreObject.GetComponent<TextMeshProUGUI>();
        }
    }

    private void ResetCoinCount()
    {
        coinCount = 0;
        UpdateCoinCountText();
        SaveCoinCount();
    }

    public void IncreaseCoinCount()
    {
        coinCount++;
        UpdateCoinCountText();
        SaveCoinCount();
    }

    public void IncreaseBackpack()
    {
        coinCount += 50;
        UpdateCoinCountText();
        SaveCoinCount(); 
    }

    private void UpdateCoinCountText()
    {
        if (coinCountText != null)
        {
            coinCountText.text = coinCount.ToString();
        }
    }

    private void SaveCoinCount()
    {
        PlayerPrefs.SetInt(coinCountKey, coinCount);
        PlayerPrefs.Save();
    }

    private void LoadCoinCount()
    {
        if (PlayerPrefs.HasKey(coinCountKey))
        {
            coinCount = PlayerPrefs.GetInt(coinCountKey);
        }
    }
}
