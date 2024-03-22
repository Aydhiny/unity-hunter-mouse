using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour
{
    private bool isGlowing = false;
    private float originalSpeed;
    public float glowDuration = 5f;
    private Renderer myRenderer; // Reference to the Renderer component
    public GameObject speedPickupFx;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>(); // Initialize the Renderer reference
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isGlowing)
        {
            StartCoroutine(IncreaseSpeedForDuration(other.gameObject));
        }
    }

    private IEnumerator IncreaseSpeedForDuration(GameObject player)
    {
        Instantiate(speedPickupFx, transform.position, transform.rotation);
        isGlowing = true;
        PlayerController playerController = player.GetComponent<PlayerController>();
        originalSpeed = playerController.playerSpeed;
        playerController.playerSpeed += 10;

        // Disable the Mesh Renderer to make the object invisible.
        myRenderer.enabled = false;

        yield return new WaitForSeconds(glowDuration);

        playerController.playerSpeed = originalSpeed;

        // Destroy object after 5 seconds
        Destroy(gameObject);

        // The object remains in the scene and its full code execution can continue.

        isGlowing = false;
    }
}