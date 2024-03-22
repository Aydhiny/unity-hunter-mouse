using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public GameObject attackBox;

    public void TurnOn() 
    {
        attackBox.SetActive(true);
        Invoke("TurnOff", 1f);
    }
    public void TurnOff() 
    {
        attackBox.SetActive(false);
    }
    public void Start()
    {
        attackBox.SetActive(false);
    }
}
