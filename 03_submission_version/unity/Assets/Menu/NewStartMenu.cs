using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewStartMenu : MonoBehaviour
{
    public static int current_song_num = 0;
    public Text txtSongName = null;
    public string[] SongName = null;
    NoteManager NM;
    public GameObject goUI = null;
    bool temp = false;

    void Start()
    {
        NM = FindObjectOfType<NoteManager>();
    }

    public void BtnNext()
    {
        //Debug.Log(SongName.Length);
        if (current_song_num == SongName.Length-1)
        {
            current_song_num = 0;
            txtSongName.text = SongName[current_song_num];
        }
        else
        {
            current_song_num += 1;
            txtSongName.text = SongName[current_song_num];
        }
        
    }
    public void BtnPrior()
    {
        if (current_song_num == 0)
        {
            current_song_num = SongName.Length-1;
            
            txtSongName.text = SongName[current_song_num];
        }
        else
        {
            current_song_num -= 1;
            txtSongName.text = SongName[current_song_num];
        }

    }

    public void BtnMainScene()
    {
        SceneManager.LoadScene("1201_scene", LoadSceneMode.Single);
    }
    public void BtnStartScene()
    {
        //ps.NoResult();
        NM.myAudio.Stop();
        NM.IsPause = false;
        SceneManager.LoadScene("start_scene2", LoadSceneMode.Single);
        current_song_num = 0;
    }

    public void QuitEvent()
    {
        Application.Quit();
    }

    public void KeyShow()
    {
        if (temp == false)
        {
            goUI.SetActive(true);
            temp = true;
        }
        else
        {
            goUI.SetActive(false);
            temp = false;
        }

    }
}
