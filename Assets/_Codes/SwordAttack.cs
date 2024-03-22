using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public GameObject SwingDirection;
    public GameObject SwingFx;
    public Transform SwingLocation;
    // Start is called before the first frame update
    void Start()
    {
        SwingDirection.SetActive(false);
    }

    public void SwingOn() 
    {
        SwingDirection.SetActive(true);
        GameObject slash = Instantiate(SwingFx, SwingLocation.position, Quaternion.Euler(90, 0, 180));
        slash.transform.SetParent(transform);
    }
    public void SwingOff() 
    {
        SwingDirection.SetActive(false);
    }

}
