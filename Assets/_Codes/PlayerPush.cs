using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField]
    private float pushPower;

    private void OnControllerColliderHit(ControllerColliderHit WhatWehit)
    {
        Rigidbody rigg = WhatWehit.collider.attachedRigidbody;
        if (rigg != null) 
        {
            Vector3 forceDirection = WhatWehit.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigg.AddForceAtPosition(forceDirection * pushPower, transform.position, ForceMode.Impulse);
        }
    }
}
