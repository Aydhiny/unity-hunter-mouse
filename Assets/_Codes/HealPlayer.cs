using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int GainAmount;
    public GameObject boo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //poof
            Instantiate(boo, transform.position, transform.rotation);
            other.GetComponent<PlayerHealthHandler>().GainHealth(GainAmount);
            Destroy(gameObject);
        }
    }

}
