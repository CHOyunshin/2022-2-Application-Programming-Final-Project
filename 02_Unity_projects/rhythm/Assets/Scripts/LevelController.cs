using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private bool levelEnded;
    private static int _nextLevelIndex = 1;

    // Update is called once per frame
    public void LevelStart()
    {
        Debug.Log("Level Start");
        levelEnded = false;
    }
    public void LevelClear()
    {
        if (!levelEnded)
        {
            Debug.Log("Level Cleared!");
            _nextLevelIndex++;
            if(_nextLevelIndex <= 3)
            {
                string nextLevelName = "Level" + _nextLevelIndex;
                SceneManager.LoadScene(nextLevelName);
            }
            levelEnded = true;
        }
    }

    public void LevelFail()
    {
        if (!levelEnded)
        {
            Debug.Log("Level Failed!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            levelEnded = true;
        }
    }
    public void GameOver()
    {
        if (!levelEnded)
        {
            Debug.Log("Game Over");
            _nextLevelIndex = 1;
            SceneManager.LoadScene("MainMenu");
            levelEnded = true;
        }
    }
    public void NextReset()
    {
        _nextLevelIndex = 1;
        levelEnded = false;
    }
}
