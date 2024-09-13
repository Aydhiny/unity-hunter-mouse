using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip enemyDeathSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "EnemyDeath":
                if (enemyDeathSound != null)
                {
                    audioSource.PlayOneShot(enemyDeathSound);
                }
                else
                {
                    Debug.LogWarning("Enemy death sound is not assigned!");
                }
                break;
            default:
                Debug.LogWarning("Sound '" + soundName + "' is not recognized!");
                break;
        }
    }
}
