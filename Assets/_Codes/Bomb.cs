using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explo;
    public int damage;
    public float knockbackRadius, exploForce;

    public float pushBack;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explo, transform.position, transform.rotation);
        knockback();
        Destroy(gameObject);
    }
    void knockback() 
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, knockbackRadius);
        foreach(Collider x in colls) 
        {
            Rigidbody rig = x.GetComponent<Rigidbody>();
            if(rig != null) 
            {
                rig.AddExplosionForce(exploForce, transform.position, knockbackRadius);
            }

            if (x.transform.tag == "Player") 
            {
                Vector3 push = transform.position - x.transform.position;
                push = push.normalized * pushBack;
                x.GetComponent<PlayerHealthHandler>().LoseHealth(damage, push);
            }
        }
    }
}