using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField]
    public int PlayerMaxHealth;
    [SerializeField]
    private int PlayerCurrentHealth;
    private PlayerController _PlayerController;

    public GameObject pickupParticle;
    public Transform topPlayerHead;

    public Image HeartIcon;
    Transform HeartPanel;
    public Color FillColor, EmptyColor;

    Respawner spawner;
    public GameObject DeathParticle;
    // Start is called before the first frame update
    void Start()
    {
        _PlayerController = GetComponent<PlayerController>();
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawner>();
        spawner.SetPosition(transform.position);
        HeartPanel = GameObject.Find("HeartPanel").transform;

        for (int i = 0; i < PlayerMaxHealth; i++) 
        {
            Image Icon = Instantiate(HeartIcon, HeartPanel);
            Icon.color = FillColor;
        }

        PlayerCurrentHealth = PlayerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerCurrentHealth <= 0) 
        {
            print("Player is dead!");
            spawner.playerIsDead();
            Instantiate(DeathParticle, transform.position, transform.rotation);
            Destroy(gameObject);
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
        if(PlayerCurrentHealth > PlayerMaxHealth) 
        {
            PlayerCurrentHealth = PlayerMaxHealth;
        }
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
        for (int n = 0; n < PlayerMaxHealth; n++) 
        {
            if(n < PlayerCurrentHealth) 
            {
                icons[n + 1].color = FillColor;
            }
            else 
            {
                icons[n + 1].color = EmptyColor;
            }
        }
    }
}
