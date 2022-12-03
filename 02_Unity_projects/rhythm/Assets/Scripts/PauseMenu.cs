using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool bpaused = false;
    public void bpause()
    {
        if(bpaused == false)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            bpaused = true;            
        }
        else
        {            
            Time.timeScale = 1;
            bpaused = false;
            gameObject.SetActive(false);
        }
        
    }
    public void bresume()
    {
        Time.timeScale = 1;
        bpaused = false;
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    public void CloseGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    public void MainMenu()
    {
        GameManager.instance.GameReset();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
