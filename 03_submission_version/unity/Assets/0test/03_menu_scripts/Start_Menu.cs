using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
}
public class Start_Menu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;
    [SerializeField] Text txtSongName = null;
    public int currentsong = 0;

    NoteManager n;
    Pause ps;

    // Start is called before the first frame update
    void Start()
    {
        n = FindObjectOfType<NoteManager>();
        ps = FindObjectOfType<Pause>();

    }
    public void BtnNext()
    {
        if (++currentsong > songList.Length - 1)
        {
            currentsong = 0;
        }
        SettingSong();
    }
    public void BtnPrior()
    {
        if (--currentsong < 0)
        {
            currentsong = songList.Length-1;
        }
        SettingSong();
    }

    void SettingSong()
    {
        txtSongName.text = songList[currentsong].name;
    }

    public void OnClick()
    {
        SceneManager.LoadScene("1201_scene",  LoadSceneMode.Single);
    }

    public void OnClick2()
    {                
        //ps.NoResult();
        n.myAudio.Stop();
        n.IsPause = false;
        SceneManager.LoadScene("start_scene2", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
