using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Respawner spawner;
    public Color GizmoColor;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawner.SetPosition(other.transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        Collider col = GetComponent<Collider>();
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, col.bounds.size);
    }
}
