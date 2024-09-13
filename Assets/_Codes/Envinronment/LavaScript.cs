using UnityEngine;

public class LavaScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthHandler playerHealth = other.GetComponent<PlayerHealthHandler>();

            if (playerHealth != null)
            {
                playerHealth.PlayerCurrentHealth = 0;
            }
        }
    }
}