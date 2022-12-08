using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Animator animator;
    public static bool s_canPresskey = true;
    Match_Area MA;
    ResultScore RS;
    public bool Qchk = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        MA = FindObjectOfType<Match_Area>();
        RS = FindObjectOfType<ResultScore>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && MA.fever2==false && MA.fever)
        {
            Vector3 v = new Vector3(1, 0, 0);
            transform.position = v;
            Debug.Log("Qchk: " + Qchk);
            MA.fever2 = true;
        }
        if (MA.fever2 && Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play("Spinkick", -1, 0);
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))&&s_canPresskey)
        {
            animator.Play("Hikick 0", -1, 0);
        }

        if (Input.GetKeyDown(KeyCode.D) && s_canPresskey && MA.fever==false)
        {
            Vector3 tx = new Vector3(1, 0, 1.7f);
            transform.position = tx;
        }
        if (Input.GetKeyDown(KeyCode.S) && s_canPresskey && MA.fever == false)
        {
            Vector3 tx = new Vector3(1, 0, 0);
            transform.position = tx;
        }
        if (Input.GetKeyDown(KeyCode.A) && s_canPresskey && MA.fever == false)
        {
            Vector3 tx = new Vector3(1, 0, -1.7f);
            transform.position = tx;
        }
            
    }

    
}
