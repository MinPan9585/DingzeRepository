using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Color squareColor;
    bool isFilled = false;

    private void OnMouseEnter()
    {
        if (!isFilled)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
    private void OnMouseExit()
    {
        if (!isFilled)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
            
    }
    private void OnMouseDown()
    {
        squareColor = ColorManager.instance.selectedColor;
        if (squareColor != Color.white)
        {
            isFilled = true;

            this.gameObject.GetComponent<SpriteRenderer>().color = squareColor;
        }
        
    }
}
