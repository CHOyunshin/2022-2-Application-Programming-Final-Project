using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_Area : MonoBehaviour
{
    
    ScoreManager SC;
    MainController MC;
    Healthbar HC;
    public GameObject CH;
    double tick,EnterTick,ExitTick;
    bool is_stay = false;
    bool is_enter = false;
    float WDcnt = 0;
    float ts = 0;
    bool stayOver = false;
    float stayTime = 0;
    float stayTime2 = 0;
    public bool fever = false;
    public bool fever2 = false;
    GameObject t1;
    GameObject t2;
    JudgeAnimController JAC;
    ResultScore RS;
    FeverAnim FA;
    ChkAlive CA;
    int Qgauge=30000;
    bool b_fever = true;
    private void Start()
    {
        
        SC = FindObjectOfType<ScoreManager>();
        MC = FindObjectOfType<MainController>();
        HC = FindObjectOfType<Healthbar>();
        JAC = FindObjectOfType<JudgeAnimController>();
        RS = FindObjectOfType<ResultScore>();
        FA = FindObjectOfType<FeverAnim>();
        CA=FindObjectOfType<ChkAlive>();
    }
    private void Update()
    {
        stayTime += Time.deltaTime;
        

        if (is_enter) is_stay = true;

        
        if (RS.currentScore > Qgauge && b_fever)
        {
            try
            {
                GameObject NoF = GameObject.Find("Nofever");
                NoF.SetActive(false);
            }
            catch { }
            FA.FeverStart();
            b_fever = false;
        }
        
        try
        {
            if (Input.GetKeyDown(KeyCode.Q) && fever == false && RS.currentScore>Qgauge)
            {
                Debug.Log("try");
                CA.WakeUp();
                b_fever = true;
                FA.FeverEnd();
                t1 = Instantiate(CH, new Vector3(1, 0, 1.7f), Quaternion.Euler(0, -90, 0));
                t2 = Instantiate(CH, new Vector3(1, 0, -1.7f), Quaternion.Euler(0, -90, 0));
                stayTime2 = stayTime;
                stayOver = true;
                fever = true;
                Qgauge += 30000;
            }
        }
        catch{ }

        if (Input.GetKeyDown(KeyCode.D) && fever == false)
        {
            WDcnt = 1.7f;
            stayTime2 = stayTime;
            stayOver = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && fever == false)
        {
            WDcnt = 0;
            stayTime2 = stayTime;
            stayOver = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && fever == false)
        {
            WDcnt = -1.7f;
            stayTime2 = stayTime;
            stayOver = true;
        }


        if (stayTime2 + 0.1 < stayTime)
            stayOver = false;

        if(stayTime2 +5 < stayTime)
        {
            Destroy(t1);
            Destroy(t2);
            fever = false;
            fever2 = false;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.transform.position.z == WDcnt)
        {
            EnterTick = tick;
            is_enter = true;
        }        
        
    }
    public void OnTriggerExit(Collider other)
    {
        MC.animator.Play("DamageDown", -1, 0);  //박스가 매치에어리어를 빠져나온다면 대미지다운 애니메이션 재생
        HC.TakeDamage(7);
        JAC.JudgeStart(1);
        SC.currentScore = 0;
        SC.ViewScore();
        if (other.gameObject.transform.position.z == WDcnt)
        {
            ExitTick = tick;
            is_enter = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        //Debug.Log("fever:" + fever);
        if (fever)
        {
            //Debug.Log("chk1");
            RS.IncreaseScore();
            SC.IncreaseScore();
            JAC.JudgeStart(0);

            GameObject child = transform.Find("CFX4 Sparks Explosion B").gameObject;
            child.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);

            is_enter = false;
            is_stay = false;
        }

        else if (is_stay && other.gameObject.transform.position.z == WDcnt && stayOver && fever==false)
        {
            Debug.Log("chk2");
            RS.IncreaseScore();
            SC.IncreaseScore();
            JAC.JudgeStart(0);

            GameObject child = transform.Find("CFX4 Sparks Explosion B").gameObject;
            child.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            
            is_enter = false;
            is_stay = false;
        }

    }

}
