using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menubtn : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject ps;

    public void Menu_button()
    {
        Time.timeScale = 0; //게임 일시정지
        
        menuPanel.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}