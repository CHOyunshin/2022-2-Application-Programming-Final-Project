using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{    

    NoteManager n;
    // Start is called before the first frame update

    private void Start()
    {
        n = FindObjectOfType<NoteManager>();
    }

    public void OnClick()
    {
        n.pauseCheck();
    }

}
