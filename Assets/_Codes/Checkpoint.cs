using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Respawner spawner;
    public Color GizmoColor;
    public CheckpointDisplay checkpointDisplay;

    private void Start()
    {
        GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnObject != null)
        {
            spawner = respawnObject.GetComponent<Respawner>();
        }
        else
        {
            Debug.LogWarning("Respawner not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // This shit should work
        //  if (other.gameObject.tag == "Player")
        //    {
        //      spawner.SetPosition(other.transform.position);
        //      checkpointDisplay.DisplayCheckpointText();
        //   }
        if (other.CompareTag("Player"))
        {
            if (spawner == null)
            {
                GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
                if (respawnObject != null)
                {
                    spawner = respawnObject.GetComponent<Respawner>();
                }
                else
                {
                    Debug.LogWarning("Respawner not found!");
                    return;
                }
            }

            spawner.SetPosition(other.transform.position);

            if (checkpointDisplay == null)
            {
                GameObject checkpointDisplayObject = GameObject.FindGameObjectWithTag("checkpointText");
                if (checkpointDisplayObject != null)
                {
                    checkpointDisplay = checkpointDisplayObject.GetComponent<CheckpointDisplay>();
                }
                else
                {
                    Debug.LogWarning("Checkpoint Display not found!");
                    return;
                }
            }

            checkpointDisplay.DisplayCheckpointText();
        }
    }
    private void OnDrawGizmos()
    {
        Collider col = GetComponent<Collider>();
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, col.bounds.size);
    }
}
