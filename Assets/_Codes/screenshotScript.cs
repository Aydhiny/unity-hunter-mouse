using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshotScript : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10)) 
        {
            ScreenCapture.CaptureScreenshot("screenshot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
        }
    }
}
