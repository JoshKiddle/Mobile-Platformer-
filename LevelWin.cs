using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    public AudioManager audioManager;

    public void Continue()
    {
        audioManager.Play("ButtonPress");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        // restart the level
        audioManager.Play("ButtonPress");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    
    public void Menu()
    {
        // go to main menu
        audioManager.Play("ButtonPress");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
