using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text tm;
    public int maxCombo = 0;
    int increaseScore = 1;
    public int currentScore = 0;
    ResultScore RS;

    // Start is called before the first frame update
    void Start()
    {
        RS = FindObjectOfType<ResultScore>();
        tm = GameObject.FindWithTag("Score").GetComponent<Text>();
        tm.text = "0";
    }


    public int getScore()
    {
        return currentScore;
    }

    public void ViewScore()
    {
        tm.text = string.Format("{0:#,##0}", currentScore);
    }
    public void IncreaseScore()
    {
        if (currentScore >= 100)
            RS.increase = 600;
        else if (currentScore >= 90)
            RS.increase = 570;
        else if (currentScore >= 80)
            RS.increase = 540;
        else if (currentScore >= 70)
            RS.increase = 510;
        else if (currentScore >= 60)
            RS.increase = 480;
        else if (currentScore >= 50)
            RS.increase = 450;
        else if (currentScore >= 40)
            RS.increase = 420;
        else if (currentScore >= 30)
            RS.increase = 390;
        else if (currentScore >= 20)
            RS.increase = 360;
        else if (currentScore >= 10)
            RS.increase = 330;
        else
            RS.increase = 300;
        
        currentScore += increaseScore;
        if (maxCombo < currentScore)
        {
            maxCombo += increaseScore;
        }        
        tm.text = string.Format("{0:#,##0}", currentScore);
    }
}
