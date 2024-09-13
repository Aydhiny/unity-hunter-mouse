using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour
{
    public GameObject destroyFX;
    public AudioClip destroySound;

    public AudioSource audioSource; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyRange"))
        {
            Instantiate(destroyFX, collision.transform.position, collision.transform.rotation);

            if (destroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            Destroy(collision.gameObject);
        }
    }
}