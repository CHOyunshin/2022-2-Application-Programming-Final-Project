using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{    
    public GameObject goUI = null;
    public Text score;
    public Text combo;

    ScoreManager theScore;
    ResultScore RS;
    // Start is called before the first frame update
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        RS = FindObjectOfType<ResultScore>();
    }

    public void ShowResult()
    {
        goUI.SetActive(true);
        int t_currentScore = RS.currentScore;
        score.text = string.Format("{0:#,##0}", t_currentScore);
        combo.text = string.Format("{0:#,##0}", theScore.maxCombo);
    }
    // Update is called once per frame
}