using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPicBtn : MonoBehaviour
{
    public GameObject pixels;
    public GameObject referencePic;
    public bool isPixelsOn = true;

    public void SwitchPic()
    {
        if (isPixelsOn)
        {
            pixels.SetActive(false);
            referencePic.SetActive(true);
            isPixelsOn = false;
        }
        else
        {
            pixels.SetActive(true);
            referencePic.SetActive(false);
            isPixelsOn = true;
        }
    }
}
