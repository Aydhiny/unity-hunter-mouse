using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Sword : MonoBehaviour
{
    public int SwordDamage;
    public float knockbackPower, knockbackEnemyPower;
    public GameObject hitFx;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            other.GetComponent<MeleeAI>().TakeDamage(SwordDamage);
            Instantiate(hitFx, other.transform);
            knockbackEnemy(other.gameObject);
        }
        if (other.gameObject.tag == "EnemyRange")
        {
            other.GetComponent<RangeAI>().TakeDamage(SwordDamage);
            Instantiate(hitFx, other.transform);
            knockbackEnemy(other.gameObject);
        }

        if (other.gameObject.tag == "Interactable") 
        {
            knockBack(other.gameObject);
        }

        void knockBack(GameObject go) 
        {
            Vector3 dif = go.transform.position - transform.position;
            dif = dif.normalized * knockbackPower;
            go.GetComponent<Rigidbody>().AddForce(dif, ForceMode.Impulse);
        }
    }

    void knockbackEnemy(GameObject go) 
    {
        NavMeshAgent agent = go.GetComponent<NavMeshAgent>();
        Vector3 dif = go.transform.position - transform.position;
        dif = dif * knockbackEnemyPower / 2;
        dif = new Vector3(dif.x, 0f, dif.z);
        agent.Move(dif);
    }

}
