using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int strokesNum;
    public float gameTimer;
    public TMP_Text timerText;
    public TMP_Text strokesText;
    public GameObject winPanel;
    public int sceneIndex = 0;

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

    public void EndGame()
    {
        winPanel.SetActive(true);

        Invoke("NextLevel", 3f);
    }

    public void NextLevel()
    {
        sceneIndex++;
        if (sceneIndex >= 3)
        {
            return;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    void Fail()
    {

    }
}
