using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject heartPickupFx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(heartPickupFx, transform.position, transform.rotation);
            other.GetComponent<PlayerHealthHandler>().GainMaxHealth();
            Destroy(gameObject);
        }
    }
}
