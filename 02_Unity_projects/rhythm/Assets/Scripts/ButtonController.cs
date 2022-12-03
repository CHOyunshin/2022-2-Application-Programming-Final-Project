using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer SR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    //이 부분은 나중에 손 위치 인식으로 교체
    public KeyCode keyToPress;
    private bool raised = false;


    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            raised = true;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            raised = false;
        }

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
