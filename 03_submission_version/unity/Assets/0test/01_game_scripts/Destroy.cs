using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{

    public float HP = 100;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("miss");
        if (other.CompareTag("Note"))
        {
            Note hh = other.GetComponent<Note>();
            if (!hh.hit)
            {
                HP -= 10;
            }
            Destroy(other.gameObject);
            
        }
    }

}
