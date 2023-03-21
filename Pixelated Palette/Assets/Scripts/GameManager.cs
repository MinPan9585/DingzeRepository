using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int strokesNum;
    public float gameTimer;
    public TMP_Text timerText;
    public TMP_Text strokesText;

    void Update()
    {
        gameTimer -= Time.deltaTime;
        timerText.text = "Remaining Time: " + Mathf.Round(gameTimer).ToString();

        if (gameTimer <= 0)
        {
            EndGame();
        }

        strokesText.text = "Strokes: " + strokesNum.ToString();
        if(strokesNum>= 20)
        {
            Fail();
        }
    }

    void EndGame()
    {
        
    }

    void Fail()
    {

    }
}
