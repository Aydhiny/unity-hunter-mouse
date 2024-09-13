using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogueSet : MonoBehaviour
{
    public GameObject textField;
    private void Start()
    {
        textField.SetActive(false);
    }
}

