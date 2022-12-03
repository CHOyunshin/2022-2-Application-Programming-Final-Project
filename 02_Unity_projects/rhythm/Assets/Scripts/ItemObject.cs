using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private bool raised = false;
    public string side;
    public bool canBePressed;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        raised = GameManager.instance.GetJoints(SocketClient.instance, side);
        if (raised)
        {
            if (canBePressed)
            {
                GameManager.instance.GemHIt();
                gameObject.SetActive(false);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
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
