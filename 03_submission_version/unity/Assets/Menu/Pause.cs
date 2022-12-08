using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject goUI = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResult()
    {
        goUI.SetActive(true);
        
    }

    public void NoResult()
    {
        goUI.SetActive(false);

    }
}
