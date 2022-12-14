using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoteManager : MonoBehaviour
{
    public GameObject goUI = null;
    Result theResult;
    Pause ps;
    public bool IsPause;
    double totalTime = 0d;
    public Transform NoteAppear = null;
    public GameObject goNote = null;
    
    // object 생성 timer로 변경 
    private float timer;
    public float waitingTime;
    
    // 생성위치 설정 부분 
    private int rnd = 0;
    private int rnd_y = 0;
    private Vector3 v3;

    void Start()
    {
        // Background Audio Setting 
        Time.timeScale = 1;
        IsPause = false;
        theResult = FindObjectOfType<Result>();
        ps = FindObjectOfType<Pause>();
        
        // note 생성 timer 작성 
        timer = 0f;
        waitingTime = 1.5f;

    }

    void Update()
    {
        rnd = UnityEngine.Random.Range(1, 3);
        rnd_y = UnityEngine.Random.Range(1, 3);
        v3 = new Vector3(NoteAppear.position.x, NoteAppear.position.y +(1.2f)*rnd_y -0.7f ,  rnd * 2.6f - 4.1f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPause == false)
            {
                ps.ShowResult();
                Time.timeScale = 0;
                IsPause = true;
                return;
            }
            if (IsPause == true)
            {
                ps.NoResult();
                Time.timeScale = 1;
                IsPause = false;
                return;
            }
        }
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
            IsPause = false;
            return;
        }
    }

    public void death()
    {
        goUI.SetActive(false);
        // theResult.ShowResult();
        Time.timeScale = 0;
        IsPause = true;        
        return;
    }
    
}
