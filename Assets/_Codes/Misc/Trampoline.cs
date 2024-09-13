using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceHeight = 5f; // Adjust this value to control the height of the bounce
    public AudioClip bounceSound; // Sound to play when bouncing

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Simulate bounce by directly changing the player's position
            Vector3 newPosition = other.transform.position + Vector3.up * bounceHeight;
            other.transform.position = newPosition;

            // Play the bounce sound
            if (bounceSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }
        }
    }
}