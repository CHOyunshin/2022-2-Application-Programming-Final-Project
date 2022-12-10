using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteManager : MonoBehaviour
{
    public GameObject goUI = null;
    public AudioClip[] bgm = null;
    public AudioSource myAudio;
    Result theResult;
    Pause ps;
    NewStartMenu NSM;
    public bool IsPause;
    public int bpm = 0;
    double currentTime = 0d;
    int blockcnt = 0;
    int blockcnt1 = 0;
    double cnt = 0d;
    double cnt1 = 0d;
    double totalTime = 0d;
    public static bool p = true;
    public static bool q = true;
    public static bool p1 = true;
    public static bool q1 = true;
    public static bool z = true;
    bool musicStart = false;
    public Transform NoteAppear = null;
    public GameObject goNote = null;
    
    // object 생성 timer로 변경 
    private float timer;
    public float waitingTime;
    
    // 생성위치 설정 부분 
    int rnd = 0;
    private int rnd_y = 0;
    Vector3 v3;
    public static bool p2 = true;
    int blockcnt2 = 0;
    double cnt2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Background Audio Setting 
        Time.timeScale = 1;
        IsPause = false;
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = bgm[1];
        myAudio.Play();
        theResult = FindObjectOfType<Result>();
        ps = FindObjectOfType<Pause>();
        
        // note 생성 timer 작성 
        timer = 0f;
        waitingTime = 1.5f;

    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * 2;        
        rnd = UnityEngine.Random.Range(1, 3);
        rnd_y = UnityEngine.Random.Range(1, 3);
        v3 = new Vector3(NoteAppear.position.x, NoteAppear.position.y +(1.2f)*rnd_y -0.7f ,  rnd * 2.6f - 4.1f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause == false)
            {
                ps.ShowResult();
                Time.timeScale = 0;
                myAudio.Pause();
                IsPause = true;
                return;
            }
            if (IsPause == true)
            {
                ps.NoResult();
                Time.timeScale = 1;
                myAudio.Play();
                IsPause = false;
                return;
            }
        }
        
        // totalTime += Time.deltaTime;
        
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            GameObject t_note = Instantiate(goNote, v3, Quaternion.Euler(0, 90, 0));
            timer = 0;
        }

        if (totalTime >= 190d)
        {
            death();
        }
    }
    public void pauseCheck()
    {
        if (IsPause == true)
        {
            //goUI.SetActive(false);
            ps.NoResult();
            Time.timeScale = 1;
            myAudio.Play();
            IsPause = false;
            return;
        }
    }

    public void death()
    {
        goUI.SetActive(false);
        theResult.ShowResult();
        Time.timeScale = 0;
        myAudio.Stop();
        IsPause = true;        
        return;
    }

}
