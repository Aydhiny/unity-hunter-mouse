using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostSpeed = 50f; // Set the desired boost speed
    public float boostDuration = 2f; // Set the duration of the speed boost in seconds

    private bool isBoosting = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag (attached to the body element)
        if (other.CompareTag("Player"))
        {
            // Get the car's Rigidbody component
            Rigidbody carRigidbody = other.transform.parent.GetComponent<Rigidbody>(); // Assuming the car's Rigidbody is attached to its parent

            if (carRigidbody != null && !isBoosting)
            {
                // Set the velocity of the car to the boost speed in the forward direction
                carRigidbody.velocity = transform.forward * boostSpeed;
            }
        }
    }

    private System.Collections.IEnumerator ResetSpeed(Rigidbody carRigidbody)
    {
        isBoosting = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(boostDuration);

        // Reset the car's velocity to zero
        carRigidbody.velocity = Vector3.zero;

        isBoosting = false;
    }
}