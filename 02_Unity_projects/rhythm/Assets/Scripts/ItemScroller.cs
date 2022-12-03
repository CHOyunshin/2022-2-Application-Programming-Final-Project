using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScroller : MonoBehaviour
{
    public float Tempo;

    public bool hasStarted;


    // Start is called before the first frame update
    void Start()
    {
        Tempo = Tempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStarted)
        {
            transform.position -= new Vector3(0f, Tempo * Time.deltaTime, 0f);
        }
    }
}
