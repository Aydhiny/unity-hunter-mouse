using UnityEngine;

public class BounceObject : MonoBehaviour
{
    private PlayerController Controller;
    private float backupHeight;
    public float bounceHeight = 2.0f;

    public Material newMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;

    public AudioSource audioSource;
    public AudioClip clickSound;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Controller == null)
            {
                Controller = other.GetComponent<PlayerController>();
            }
            if (Controller != null)
            {
                if (audioSource != null && clickSound != null)
                {
                    audioSource.PlayOneShot(clickSound);
                }
                objectRenderer.material = newMaterial;
                backupHeight = Controller.jumpHeight;
                Controller.jumpHeight = bounceHeight;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Controller != null)
            {
                Controller.jumpHeight = backupHeight;
                objectRenderer.material = originalMaterial;
            }
        }
    }

}