using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Transform targetToFollow;
    public Vector3 offset;
    public float duration;
    public bool canMove = true;
    // Start is called before the first frame update
    private void Start()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;

        // Debug log to check if the player GameObject is found
        if (targetToFollow != null)
        {
            enabled = true;
            Debug.Log("Player GameObject found. Starting camera follow.");

            // Set camera position to follow player
            Vector3 DesiredPosition = targetToFollow.position + offset;
            transform.position = DesiredPosition;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found. CamFollow script cannot follow the player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (targetToFollow != null)
            {
                Vector3 DesiredPosition = targetToFollow.position + offset;
                Vector3 SmoothFollow = Vector3.Lerp(transform.position, DesiredPosition, duration);
                transform.position = SmoothFollow;
            }
            else
            {
                Debug.LogWarning("targetToFollow is null. Attempting to find 'Player' GameObject again.");
                targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
            }        
        }
    }
}
