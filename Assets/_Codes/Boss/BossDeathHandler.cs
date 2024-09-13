using UnityEngine;

public class BossDeathHandler : MonoBehaviour
{
    public GameObject bossObject; 
    public Collider bossCollider;
    public MeshRenderer bossMeshRenderer; 
    public Collider otherTrigger; 

    void Start()
    {
        // Hide the collider and mesh renderer at the start
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
        }
        if (bossMeshRenderer != null)
        {
            bossMeshRenderer.enabled = false;
        }

        // Disable the other trigger at the start
        if (otherTrigger != null)
        {
            otherTrigger.enabled = false;
        }
    }

    void Update()
    {
        // Check if the LevelBoss object is destroyed
        if (bossObject == null)
        {
            // If destroyed, make the collider and mesh renderer visible
            if (bossCollider != null)
            {
                bossCollider.enabled = true;
            }
            if (bossMeshRenderer != null)
            {
                bossMeshRenderer.enabled = true;
            }

            // Enable the other trigger when the boss dies
            if (otherTrigger != null)
            {
                otherTrigger.enabled = true;
            }
        }
    }
}