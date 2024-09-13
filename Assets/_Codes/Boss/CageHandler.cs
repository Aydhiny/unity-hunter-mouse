using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CageHandler : MonoBehaviour
{
    public GameObject cage;
    public GameObject friends;

    private void Start()
    {
        friends.SetActive(false);
    }

    private void OnDestroy()
    {
        cage.SetActive(false);
        friends.SetActive(true);
    }
}
