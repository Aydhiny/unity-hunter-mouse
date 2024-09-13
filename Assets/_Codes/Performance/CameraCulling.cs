using UnityEngine;

public class CameraCulling : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Create a plane representing the camera's view frustum
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // Loop through all objects in the scene
        foreach (var obj in FindObjectsOfType<Renderer>())
        {
            // Check if the object is within the camera's view frustum
            if (GeometryUtility.TestPlanesAABB(planes, obj.bounds))
            {
                // Object is within view, enable rendering
                obj.enabled = true;
            }
            else
            {
                // Object is outside view, disable rendering
                obj.enabled = false;
            }
        }
    }
}