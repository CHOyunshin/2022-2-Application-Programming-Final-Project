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
    int rnd = 0;
    Vector3 v3;
    public static bool p2 = true;
    int blockcnt2 = 0;
    double cnt2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        IsPause = false;

        int x = NewStartMenu.current_song_num;
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = bgm[x];
        if (x == 0) bpm = 164;
        else if (x == 1) bpm = 114;
        else if (x == 2) bpm = 120;
        else if (x == 3) bpm = 120;

        myAudio.Play();



        theResult = FindObjectOfType<Result>();
        ps = FindObjectOfType<Pause>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;        
        rnd = UnityEngine.Random.Range(1, 3);
        v3 = new Vector3(NoteAppear.position.x, NoteAppear.position.y,  rnd * 3f - 4.45f);

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

        totalTime += Time.deltaTime;


        //////////////////////////////////
        if (totalTime >= 36d && totalTime <= 50d || totalTime >= 110d && totalTime < 123d || totalTime >= 170d && totalTime < 182d)
        {
            if (currentTime >= 49d / bpm && p1 == true)
            {
                GameObject t_note = Instantiate(goNote, v3, Quaternion.Euler(0, 90, 0));
                blockcnt1++;
                currentTime -= 49d / bpm;
                if (blockcnt1 == 3)
                {
                    p1 = false;
                }
            }

            if (!p1)
            {
                cnt1 += Time.deltaTime;

                if (cnt1 >= 100d / bpm)
                {
                    blockcnt1 = 0;
                    cnt1 = 0d;
                    currentTime = 0d;
                    p1 = true;
                }

            }
        
        }else if (totalTime >= 180d)
        {
            p = false;            
        }       
        else if (totalTime >= 123d && totalTime < 136d)
        {
            if (currentTime >= 164d / bpm)
            {
                GameObject t_note = Instantiate(goNote, v3, Quaternion.Euler(0, 90, 0));
                currentTime -= 164d / bpm;                
            }
        }
        else if (totalTime >= 136d && totalTime < 148d)
        {
            if (currentTime >= 49d / bpm)
            {
                GameObject t_note = Instantiate(goNote, v3, Quaternion.Euler(0, 90, 0));
                currentTime -= 49d / bpm;
            }
        }
        else if (totalTime >= 75d && totalTime < 99d)
        {
            if (currentTime >= 49d / bpm && p2 == true)
            {
                GameObject t_note = Instantiate(goNote, v3, Quaternion.Euler(0, 90, 0));
                blockcnt2++;
                currentTime -= 49d / bpm;
                if (blockcnt2 == 4)
                {
                    p2 = false;
                }
            }
            if (!p2)
            {
                cnt2 += Time.deltaTime;

                if (cnt2 >= 100d / bpm)
                {
                    blockcnt2 = 0;
                    cnt2 = 0d;
                    currentTime = 0d;
                    p2 = true;
                }
 
            }
        }
        else
        {
            if (currentTime >= 49d / bpm && p == true)
            {
                GameObject t_note = Instantiate(goNote, v3, Quaternion.Euler(0, 90, 0));
                blockcnt++;
                currentTime -= 49d / bpm;
                if (blockcnt == 7)
                {
                    p = false;
                }
            }
            if (!p && q)
            {
                cnt += Time.deltaTime;

                if (cnt >= 80d / bpm)
                {
                    blockcnt = 0;
                    cnt = 0d;
                    currentTime = 0d;
                    p = true;
                }
            }
        }
        ////////////////////////////


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
