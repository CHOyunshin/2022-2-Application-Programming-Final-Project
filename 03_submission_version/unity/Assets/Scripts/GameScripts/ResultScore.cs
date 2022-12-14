using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    public Text te;
    public int increase = 300;
    public int currentScore = 0;

    void Start()
    {
        te = GameObject.Find("ResultScore").GetComponent<Text>();
        te.text = "0";
        te.text = string.Format("{0:#,##0}", currentScore);
    }

    public void IncreaseScore()
    {
        currentScore += increase;
        te.text = string.Format("{0:#,##0}", currentScore);
    }
}
