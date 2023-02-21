using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    private bool isDrag = false;
    private Vector3 mousePosIni;
    private Vector3 mousePosEnd;

    public RectTransform selectionBox;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosIni = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if(!isDrag && Vector3.Distance(mousePosIni, Input.mousePosition) > 30){
                isDrag = true;
            }
            if (isDrag)
            {
                mousePosEnd = Input.mousePosition;
                UpdateSelectionBox();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDrag)
            {
                isDrag = false;
                UpdateSelectionBox();
            }
        }
    }
    void UpdateSelectionBox()
    {
        selectionBox.gameObject.SetActive(isDrag);

        float width = mousePosEnd.x - mousePosIni.x;
        float length = mousePosEnd.y - mousePosIni.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(length));

        selectionBox.anchoredPosition = new Vector2(mousePosIni.x, mousePosEnd.y) + new Vector2(width / 2, length / 2);
    }
}
