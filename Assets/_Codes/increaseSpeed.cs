using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour
{
    private bool isGlowing = false;
    private float originalSpeed;
    public float glowDuration = 5f;
    public float speedIncreaseAmount = 10f;
    private Renderer myRenderer; // Reference to the Renderer component
    public GameObject speedPickupFx;
    public TextMeshProUGUI durationText; // Reference to the TextMeshPro text component

    private void Start()
    {
        myRenderer = GetComponent<Renderer>(); // Initialize the Renderer reference
        // Disable duration text initially
        durationText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isGlowing)
        {
            StartCoroutine(IncreaseSpeedForDuration(other.gameObject));
        }
    }

    private IEnumerator IncreaseSpeedForDuration(GameObject player)
    {
        isGlowing = true;
        PlayerController playerController = player.GetComponent<PlayerController>();
        originalSpeed = playerController.playerSpeed;
        playerController.playerSpeed += speedIncreaseAmount;

        Instantiate(speedPickupFx, transform.position, Quaternion.identity);

        // Enable duration text
        durationText.gameObject.SetActive(true);

        float startTime = Time.time;
        while (Time.time - startTime < glowDuration)
        {
            float remainingTime = glowDuration - (Time.time - startTime);
            // Update duration text
            durationText.text = "Duration: " + Mathf.CeilToInt(remainingTime).ToString();

            yield return null;
        }

        // Reset player's speed
        playerController.playerSpeed = originalSpeed;

        // Hide duration text
        durationText.gameObject.SetActive(false);

        isGlowing = false;

        // Destroy object after duration
        Destroy(gameObject);
    }
}