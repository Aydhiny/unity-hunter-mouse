using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BossExplo : MonoBehaviour
{
    public AudioClip destroySound; 
    public AudioSource audioSource;
    private void OnDestroy()
    {
        // Check if the destroyed object has the tag "LevelBoss"
        if (gameObject.CompareTag("LevelBoss"))
        {
            // Play the destroy sound effect
            if (destroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(destroySound);
            }
        }
    }
}

