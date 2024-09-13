using UnityEngine;

public class TeleportCar : MonoBehaviour
{
    public Vector3 teleportPosition = new Vector3(42, 157, 512); 
    public Vector3 teleportRotation = Vector3.zero; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        GameObject playerBody = GameObject.FindGameObjectWithTag("Player");

        if (playerBody != null)
        {
            Transform carTransform = playerBody.transform.parent;

            if (carTransform != null)
            {
                carTransform.position = teleportPosition;

                carTransform.rotation = Quaternion.Euler(teleportRotation);

                Rigidbody carRigidbody = carTransform.GetComponent<Rigidbody>();
                if (carRigidbody != null)
                {
                    carRigidbody.velocity = Vector3.zero;
                    carRigidbody.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                Debug.LogWarning("The Player tag's parent is null. Ensure the Player tag is assigned to the car body, which is a child of the car.");
            }
        }
        else
        {
            Debug.LogWarning("No object with the 'Player' tag found.");
        }
    }
}
