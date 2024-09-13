using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour
{
    public AudioClip touchSound; 
    public AudioSource audioSource; 

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (touchSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(touchSound);
            }
        }
    }
}