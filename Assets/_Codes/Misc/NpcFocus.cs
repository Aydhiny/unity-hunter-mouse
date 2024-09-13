using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFocus : MonoBehaviour
{
    public Vector3 offset;
    public float duration;

    public void SetFocus(Transform target) 
    {
        Vector3 DesiredPosition = target.position + offset;
        Vector3 SmoothFollow = Vector3.Lerp(transform.position, DesiredPosition, duration);
        transform.position = SmoothFollow;
    }

    private void OnDisable()
    {
        GetComponent<CamFollow>().enabled = true;
    }
    private void OnEnable()
    {
        GetComponent<CamFollow>().enabled = false;
    }
}
