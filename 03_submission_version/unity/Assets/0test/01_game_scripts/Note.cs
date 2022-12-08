using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    //GameObject child;
    public float noteSpeed = 400;

    private void Start()
    {
        //child = transform.Find("CFX4 Sparks Explosion B").gameObject;
        //Debug.Log(child.name);
       



    }
    //public GameObject FindChild()
   // {
        //return child;
    //}
    // Update is called once per frame
    void Update()
    {
        
        
        transform.position += Vector3.right * noteSpeed * Time.deltaTime;

    }
    
    
    
}
