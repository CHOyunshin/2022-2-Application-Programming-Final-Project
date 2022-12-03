using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public SocketClient socketClient;
    private SpriteRenderer SR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    //�� �κ��� ���߿� �� ��ġ �ν����� ��ü
    public string side;
    private bool raised = false;


    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        raised = GetJoints(socketClient, side);

        if (raised)
        {
            SR.sprite = pressedImage;
        }
        else
        {
            SR.sprite = defaultImage;
        }
    }

    private bool GetJoints(SocketClient sc, string side)
    {
        if(sc.jointList.joint.Length > 0)
        {
            Joint[] joints = sc.jointList.joint;
            if(side == "left")
            {
                if(joints[11].y > joints[15].y)
                {
                    return true;
                }
                else 
                { return false; }
            }
            else
            {
                if(joints[12].y > joints[16].y)
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
