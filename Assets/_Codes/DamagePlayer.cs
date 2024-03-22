using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    public float pushBack;

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player") 
        {
            Vector3 push = transform.position - other.transform.position;
            push = push.normalized * pushBack;
            other.GetComponent<PlayerHealthHandler>().LoseHealth(damage, push);
        }
    }
}
