using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public SocketClient socketClient;
    private SpriteRenderer SR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public string side;
    private bool raised = false;


    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        raised = GameManager.instance.GetJoints(socketClient, side);

        if (raised)
        {
            SR.sprite = pressedImage;
        }
        else
        {
            SR.sprite = defaultImage;
        }
    }

    
}
