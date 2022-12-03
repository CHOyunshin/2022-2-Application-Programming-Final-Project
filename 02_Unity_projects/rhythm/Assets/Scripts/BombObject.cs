using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyToPress))
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
