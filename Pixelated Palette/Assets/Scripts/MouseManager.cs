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

    private bool mouseDown = false;
    public Material drawMat;

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
                mouseDown = true;
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
                mouseDown = false;
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
                mouseDown = false;
                mainTime = 0;
            }
        }
    }

    void OnGUI()
    {
        if (mouseDown)
        {
            Draw();
        }
    }
    void Draw()
    {
        drawMat.SetPass(0);
        GL.PushMatrix();//保存摄像机变换矩阵  
        GL.LoadPixelMatrix();//设置用屏幕坐标绘图  
                             //透明框  
        Color clr = new Color (1f, 0.1f, 0.1f, 0.5f);

        GL.Begin(GL.LINES);
        GL.Color(clr);
        GL.Vertex3(mouseStartPos.x, mouseStartPos.y, 0);
        GL.Vertex3(mouseCurrentPos.x, mouseStartPos.y, 0);
        GL.End();

        //下  
        GL.Begin(GL.LINES);
        GL.Color(clr);
        GL.Vertex3(mouseStartPos.x, mouseCurrentPos.y, 0);
        GL.Vertex3(mouseCurrentPos.x, mouseCurrentPos.y, 0);
        GL.End();

        //左  
        GL.Begin(GL.LINES);
        GL.Color(clr);
        GL.Vertex3(mouseStartPos.x, mouseStartPos.y, 0);
        GL.Vertex3(mouseStartPos.x, mouseCurrentPos.y, 0);
        GL.End();

        //右  
        GL.Begin(GL.LINES);
        GL.Color(clr);
        GL.Vertex3(mouseCurrentPos.x, mouseStartPos.y, 0);
        GL.Vertex3(mouseCurrentPos.x, mouseCurrentPos.y, 0);
        GL.End();

        GL.Begin(GL.LINES);
        GL.Color(clr);
        GL.Vertex3(mouseCurrentPos.x+1, mouseStartPos.y, 0);
        GL.Vertex3(mouseCurrentPos.x+1, mouseCurrentPos.y, 0);
        GL.End();
        GL.Begin(GL.LINES);
        GL.Color(clr);
        GL.Vertex3(mouseCurrentPos.x + 2, mouseStartPos.y, 0);
        GL.Vertex3(mouseCurrentPos.x + 2, mouseCurrentPos.y, 0);
        GL.End();

        GL.PopMatrix();//还原  
    }

}
