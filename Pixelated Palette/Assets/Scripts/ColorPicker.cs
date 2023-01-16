using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color buttonColor;
    
    public void SetColor()
    {
        buttonColor = this.gameObject.GetComponent<Image>().color;
        ColorManager.instance.selectedColor = buttonColor;
    }
}
