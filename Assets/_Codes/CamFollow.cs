using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Transform targetToFollow;
    public Vector3 offset;
    public float duration;
    // Start is called before the first frame update
    private void Start()
    {
        targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetToFollow != null) 
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
