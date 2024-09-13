using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Transform target; // The central point to rotate around
    public float rotationSpeed = 30f; // Speed of rotation

    void Update()
    {
        // Rotate the camera around the target
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}