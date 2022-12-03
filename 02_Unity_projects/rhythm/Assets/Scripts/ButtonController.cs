using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer SR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    //�� �κ��� ���߿� �� ��ġ �ν����� ��ü
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
