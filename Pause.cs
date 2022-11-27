using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject UI;

    public AudioManager audioManager;

    public void PauseGame()
    {
        audioManager.Play("ButtonPress");
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        audioManager.Play("ButtonPress");
        PauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        audioManager.Play("ButtonPress");
        PauseGame();
        SceneManager.LoadScene("MainMenu");
    }
}
