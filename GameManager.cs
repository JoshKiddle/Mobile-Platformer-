using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public GameObject levelWinUI;
    public GameObject gameOverUI;
    public Text currencyText;

    public AudioManager audioManager;
    public Player player;

    public static void PlayerDeath(Player player)
    {
        Destroy(player.gameObject);
    }  

    public void WinLevel()
    {
        levelWinUI.SetActive(!levelWinUI.activeSelf);
        audioManager.Play("GameWon");

        Currency.permCrystals += player.currentCurrency;
        Currency.UpdateCurrency();

        //Debug.Log(Currency.permCrystals);

        if(levelWinUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
        audioManager.Play("GameOver");

        if (gameOverUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
