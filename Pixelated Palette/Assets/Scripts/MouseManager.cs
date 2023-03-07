using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private Vector3 mouseCurrentPos;
    private Vector3 selectionBoxStart;
    private Vector3 selectionBoxEnd;
    //private Vector3 mouseEndPos;
    private float mainTime;
    public GameObject[] pixelObjects;
    private GameObject closestObj;

    private bool mouseDown = false;
    public Material drawMat;

    //public Vector3[] pixelScreenPos;
    //public List<Vector3> pixelScreenPositions = new List<Vector3>();

    public GameManager gameManager;

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
        if (Input.GetMouseButtonDown(0))
        {
            // Record the start position of the selection box.
            mouseDown = true;
            selectionBoxStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            // Record the end position of the selection box.
            selectionBoxEnd = Input.mousePosition;

            // Determine the left, right, bottom, and top positions of the selection box.
            float percentage = 0.008f;
            float screenPercentageX = Screen.width * percentage;
            float screenPercentageY = Screen.height * percentage;
            float left = Mathf.Min(selectionBoxStart.x, selectionBoxEnd.x) - screenPercentageX;
            float right = Mathf.Max(selectionBoxStart.x, selectionBoxEnd.x) + screenPercentageX;
            float bottom = Mathf.Min(selectionBoxStart.y, selectionBoxEnd.y) - screenPercentageY;
            float top = Mathf.Max(selectionBoxStart.y, selectionBoxEnd.y) + screenPercentageY;

            // Iterate over all the game objects and check if their position is within the selection box.
            for (int i = 0; i < pixelObjects.Length; i++)
            {
                Vector3 objScreenPos = Camera.main.WorldToScreenPoint(pixelObjects[i].transform.position);

                if (objScreenPos.x >= left && objScreenPos.x <= right && objScreenPos.y >= bottom && objScreenPos.y <= top)
                {
                    // The game object is within the selection box, so set its color to the selected color.
                    pixelObjects[i].GetComponent<SpriteRenderer>().color = ColorManager.instance.selectedColor;
                }
            }

            // Increment the number of strokes if the selected color is not white.
            if (ColorManager.instance.selectedColor != Color.white)
            {
                gameManager.strokesNum++;
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
