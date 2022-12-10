using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool startPlaying;
    private bool startCountDown;
    public static GameManager instance;

    private static int currentScore = 0;

    private int startScore;
    public int scorePerNote = 100;
    private int gemsCollected = 0;
    public GameObject endOfItem;
    public int gemsReq;
    [SerializeField] bool missionCleared;

    //레벨 시작 스테이지
    public GameObject startup;
    public Text countdown;
    public Text mission;
    public Text almission;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        startCountDown = false;
        missionCleared = false;
        almission.text = " ";
        almission.color = new Color32(255, 0, 0, 255);
        startScore = currentScore;
    }
    private float starttime;
    void Update()
    {
        if (!startPlaying)
        {
            if (!startCountDown)
            {
                starttime = Time.time;
                startup.SetActive(true);
                countdown.text = "3";
                mission.text = "Collect " + gemsReq + " or more gems!";
                startCountDown = true;
            }
            else
            {
                if (Time.time - starttime >= 4)
                {
                    startup.SetActive(false);
                    almission.text = mission.text;
                    startPlaying = true;
                    startCountDown = false;
                }
                else if (Time.time - starttime >= 3)
                {
                    countdown.text = "Start!";
                }
                else if (Time.time - starttime >= 2)
                {
                    countdown.text = "1";
                }
                else if (Time.time - starttime >= 1)
                {
                    countdown.text = "2";
                }
            }
        }
        else
        {
            if (gemsCollected >= gemsReq)
            {
                missionCleared = true;
                almission.color = new Color32(0, 255, 20, 200);
                almission.text = "Collected " + gemsReq + " gems";
            }
            if (endOfItem.transform.position.y <= -2)
            {
                if (missionCleared)
                {
                    starttime = Time.time;
                    startup.SetActive(true);
                    if (starttime >= 1)
                    {
                        startup.SetActive(false);
                    }
                    else
                    {
                        countdown.text = "Level Cleared!";
                        mission.text = " ";
                    }
                }
                else
                {
                    Debug.Log("Not enough gems!");
                    currentScore = startScore;
                }
            }
        }

    }

    public void GemHIt()
    {
        Debug.Log("Gem Acquired!");
        currentScore += scorePerNote;
        gemsCollected += 1;
    }

    public void GameReset()
    {
        Debug.Log("Score Resetted");
        currentScore = 0;
    }
}
