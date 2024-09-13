using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public MeleeAI bossMeleeAI;
    public Image healthBarFill;
    public GameObject healthBarContainer; // Container for the health bar UI

    private int maxHealth;

    void Start()
    {
        if (bossMeleeAI != null)
        {
            maxHealth = bossMeleeAI.enemyHP;
        }
        else
        {
            Debug.LogError("MeleeAI script not assigned or found on the boss GameObject.");
        }

        // Ensure health bar is visible at start
        if (healthBarContainer != null)
        {
            healthBarContainer.SetActive(true);
        }
    }

    void Update()
    {
        if (bossMeleeAI != null && healthBarFill != null)
        {
            if (bossMeleeAI.enemyHP > 0)
            {
                float healthPercentage = (float)bossMeleeAI.enemyHP / maxHealth;
                healthBarFill.fillAmount = healthPercentage;
            }
            else
            {
                // Boss is dead, disable the health bar
                if (healthBarContainer != null)
                {
                    healthBarContainer.SetActive(false);
                }
            }
        }
    }
}