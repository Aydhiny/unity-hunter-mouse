using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public string DialogMessage;

    private void Start()
    {
        TextField.text = "";
        TextField.transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        TextField.text = DialogMessage;
        TextField.transform.parent.gameObject.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        TextField.transform.parent.gameObject.SetActive(false);
        TextField.text = "";
    }
}

