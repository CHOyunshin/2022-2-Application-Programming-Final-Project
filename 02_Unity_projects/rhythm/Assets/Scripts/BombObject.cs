using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    private bool raised = false;
    public string side;
    public bool canBePressed;

    // Update is called once per frame
    void Update()
    {
        raised = GameManager.instance.GetJoints(SocketClient.instance, side);
        if (raised)
        {
            if (canBePressed)
            {
                GameManager.instance.BombHit();
                gameObject.SetActive(false);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
