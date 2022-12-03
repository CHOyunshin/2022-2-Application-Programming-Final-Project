using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public bool startPlaying;
    private bool startCountDown;
    public ItemScroller theIS;
    public static GameManager instance;
    public Text scoreText;
    public Text lifeText;
    public Text gemText;

    public LevelController LCscript;

    private static int currentScore = 0;
    private static int remainingLife = 3;

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
        scoreText.text = "Score: " + currentScore;
        if (remainingLife == 3)
        {
            lifeText.text = "Life: ❤❤❤";
        }
        else if (remainingLife == 2)
        {
            lifeText.text = "Life: ❤❤";
        }
        else if (remainingLife == 1)
        {
            lifeText.text = "Life: ❤";
        }
        gemText.text = "Gems Collected: " + gemsCollected;
        LCscript.LevelStart();
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
                    theIS.hasStarted = true;
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
            if(gemsCollected >= gemsReq)
            {
                missionCleared = true;
                almission.color = new Color32(0,255,20,200);
                almission.text = "Collected " + gemsReq + " gems";
            }
            if(remainingLife == 0)
            {
                Debug.Log("No remaining life!");
                currentScore = 0;
                remainingLife = 3;
                LCscript.GameOver();
            }
            if(endOfItem.transform.position.y <= -2)
            {
               if(missionCleared)
                {
                    starttime = Time.time;
                    startup.SetActive(true);
                    if (starttime >= 1)
                    {
                        if (remainingLife <= 2)
                        {
                            remainingLife++;
                        }
                        startup.SetActive(false);
                        LCscript.LevelClear();
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
                    LCscript.LevelFail();
                }
            }
        }
        
    }

    public void GemHIt()
    {
        Debug.Log("Gem Acquired!");
        currentScore += scorePerNote;
        gemsCollected += 1;
        scoreText.text = "Score: " + currentScore;
        gemText.text = "Gems Collected: " + gemsCollected;
    }

    public void BombHit()
    {
        Debug.Log("Bomb Hit!");
        remainingLife -= 1;
        if(remainingLife == 2)
        {
            lifeText.text = "Life: ❤❤";
        }
        else if (remainingLife == 1)
        {
            lifeText.text = "Life: ❤";
        }
    }
    
    public void GameReset()
    {
        Debug.Log("Score Resetted");
        LCscript.NextReset();
        currentScore = 0;
        remainingLife = 3;
    }
    public bool GetJoints(SocketClient sc, string side)
    {
        if (sc.jointList.joint.Length > 0)
        {
            Joint[] joints = sc.jointList.joint;
            if (side == "left")
            {
                if (joints[11].y > joints[15].y)
                {
                    return true;
                }
                else
                { return false; }
            }
            else
            {
                if (joints[12].y > joints[16].y)
                {
                    return true;
                }
                else
                { return false; }
            }
        }
        else
        {
            return false;
        }
    }
}
