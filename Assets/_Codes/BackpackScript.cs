using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class BackpackScript : MonoBehaviour
{
    public float rotationSpeed = 85f;
    public GameObject backpackPickupFx;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(x: 0f, y: rotationSpeed * Time.deltaTime, z: 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Increase the coin count
            ScoreManager.Instance.IncreaseBackpack();

            // Instantiate coin pickup effect
            Instantiate(backpackPickupFx, transform.position, transform.rotation);

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
