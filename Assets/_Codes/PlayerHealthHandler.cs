using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField]
    public int PlayerMaxHealth;
    [SerializeField]
    public int PlayerCurrentHealth;
    private PlayerController _PlayerController;

    public GameObject pickupParticle;
    public Transform topPlayerHead;

    public Image HeartIcon;
    Transform HeartPanel;
    public Color FillColor, EmptyColor;

    public GameObject DeathParticle;

    Respawner spawner;

    private bool hasRespawned = false;

    void Start()
    {
        _PlayerController = GetComponent<PlayerController>();
        spawner = GameObject.FindGameObjectWithTag("Respawn").
            GetComponent<Respawner>();
        spawner.SetPosition(transform.position);

        HeartPanel = GameObject.Find("HeartPanel").transform;

        for (int i = 0; i < PlayerMaxHealth; i++)
        {
            Image Icon = Instantiate(HeartIcon, HeartPanel);
            Icon.color = FillColor;
        }

        PlayerCurrentHealth = PlayerMaxHealth;
       // LoadPlayerHealth();
    }

    void Update()
    {
        if (PlayerCurrentHealth <= 0)
        {
            print("Player is dead!");
            spawner.playerIsDead();
            Instantiate(DeathParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // Check if the player's y coordinate is below -40
        if (transform.position.y < -40 && !hasRespawned)
        {
            PlayerCurrentHealth = 0;
            Debug.Log("Player position below -40!");
        }

        UpdateHearts();
    }

    public void GainMaxHealth()
    {
        GameObject clone = Instantiate(pickupParticle, topPlayerHead.position, topPlayerHead.rotation);
        clone.transform.SetParent(topPlayerHead);
        PlayerMaxHealth += 1;
        ResetHealth();
    }

    public void GainHealth(int amount)
    {
        PlayerCurrentHealth += amount;
        GameObject clone = Instantiate(pickupParticle, topPlayerHead.position, topPlayerHead.rotation);
        clone.transform.SetParent(topPlayerHead);
        if (PlayerCurrentHealth > PlayerMaxHealth)
        {
            PlayerCurrentHealth = PlayerMaxHealth;
        }
        SavePlayerHealth();
    }

    public void LoseHealth(int amount, Vector3 push)
    {
        PlayerCurrentHealth -= amount;
        if (PlayerCurrentHealth < 0)
        {
            PlayerCurrentHealth = 0;
        }
        _PlayerController.controller.Move(-push);
        _PlayerController.flashRed();

        SavePlayerHealth();
    }

    public void ResetHealth()
    {
        PlayerCurrentHealth = PlayerMaxHealth;
        Image Icon = Instantiate(HeartIcon, HeartPanel);
        Icon.color = FillColor;
    }

    void UpdateHearts()
    {
        Image[] icons = HeartPanel.GetComponentsInChildren<Image>();
        int numIcons = Mathf.Min(PlayerMaxHealth, icons.Length - 1); // new code with gpt i dont give a shit
        for (int n = 0; n < PlayerMaxHealth; n++)
        {
            if (n < PlayerCurrentHealth)
            {
                icons[n + 1].color = FillColor;
            }
            else
            {
                icons[n + 1].color = EmptyColor;
            }
        }
    }

    private void SavePlayerHealth()
    {
        // Save player health to PlayerPrefs
        PlayerPrefs.SetInt("PlayerMaxHealth", PlayerMaxHealth);
        PlayerPrefs.SetInt("PlayerCurrentHealth", PlayerCurrentHealth);
        PlayerPrefs.Save();
    }

    private void LoadPlayerHealth()
    {
        // Load player health from PlayerPrefs
        PlayerMaxHealth = PlayerPrefs.GetInt("PlayerMaxHealth", PlayerMaxHealth);
        PlayerCurrentHealth = PlayerPrefs.GetInt("PlayerCurrentHealth", PlayerMaxHealth);
    }
}

