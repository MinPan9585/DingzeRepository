using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private Vector3 mouseCurrentPos;
    //private Vector3 mouseEndPos;
    private float mainTime;
    public GameObject[] pixelObjects;
    private GameObject closestObj;

    //public Vector3[] pixelScreenPos;
    //public List<Vector3> pixelScreenPositions = new List<Vector3>();

    void Start()
    {
        pixelObjects = GameObject.FindGameObjectsWithTag("Pixel");
        //for (int i = 0; i < pixelObjects.Length; i++)
        //{
        //    Vector3 pixelScreenPos = Camera.main.WorldToScreenPoint(pixelObjects[i].transform.position);
        //    pixelScreenPositions.Add(pixelScreenPos);
        //}
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(mainTime == 0.0f)
            {
                mouseStartPos = Input.mousePosition;
                mainTime = Time.time;
            }
            if(Time.time - mainTime > 0.2f)
            {
                mouseCurrentPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - mainTime < 0.2f)
            {
                float shortesetDis = 10000f;
                
                for (int i = 0; i < pixelObjects.Length; i++)
                {
                    float dis = Vector3.Distance(Camera.main.WorldToScreenPoint(pixelObjects[i].transform.position), mouseStartPos);
                    
                    if(dis < shortesetDis)
                    {
                        shortesetDis = dis;

                        closestObj = pixelObjects[i];
                    }
                }
                Debug.Log(shortesetDis);
                if (shortesetDis<=13.5f)
                {
                    closestObj.GetComponent<SpriteRenderer>().color = ColorManager.instance.selectedColor;
                }
                
                mainTime = 0;
            }
            else
            {
                
                for (int i = 0; i < pixelObjects.Length; i++)
                {
                    Vector2 po = Camera.main.WorldToScreenPoint(pixelObjects[i].transform.position);
                    Vector2 ms = mouseStartPos;
                    Vector2 mc = mouseCurrentPos;

                    if ((po.x > ms.x && po.x < mc.x && po.y > ms.y && po.y < mc.y) ||
                    (po.x > ms.x && po.x < mc.x && po.y < ms.y && po.y > mc.y) ||
                    (po.x < ms.x && po.x > mc.x && po.y > ms.y && po.y < mc.y) ||
                    (po.x < ms.x && po.x > mc.x && po.y < ms.y && po.y > mc.y))
                    {
                        //Debug.Log(mouseStartPos);
                        //Debug.Log(mouseCurrentPos);
                        //Debug.Log(pixelObjects[i].name);
                        pixelObjects[i].GetComponent<SpriteRenderer>().color = ColorManager.instance.selectedColor;
                    }

                }
                
                mainTime = 0;
            }
        }
    }
}
