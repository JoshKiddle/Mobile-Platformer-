using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainUI, characterUI, shopUI, crystalUI, levelUI;
    public AudioManager audioManager;

    public void Start()
    {
        ChangeAudio();
        audioManager.Play("MenuMusic");
    }

    public void Play()
    {
        mainUI.SetActive(!mainUI.activeSelf);
        levelUI.SetActive(!levelUI.activeSelf);
    }

    public void Characters()
    {
        // open character bios screen and play UI sound
        Debug.Log("Character Bios Screen");
        mainUI.SetActive(!mainUI.activeSelf);
        characterUI.SetActive(!characterUI.activeSelf);
    }

    public void Shop()
    {
        Debug.Log("Opened shop");
        mainUI.SetActive(!mainUI.activeSelf);
        shopUI.SetActive(!shopUI.activeSelf);
    }

    public void CrystalShop()
    {
        crystalUI.SetActive(!crystalUI.activeSelf);
        shopUI.SetActive(!shopUI.activeSelf);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void CharacterBack()
    {
        mainUI.SetActive(!mainUI.activeSelf);
        characterUI.SetActive(!characterUI.activeSelf);
    }

    public void ShopBack()
    {
        mainUI.SetActive(!mainUI.activeSelf);
        shopUI.SetActive(!shopUI.activeSelf);
    }

    public void CrystalShopBack()
    {
        shopUI.SetActive(!shopUI.activeSelf);
        crystalUI.SetActive(!crystalUI.activeSelf);
    }

    public void LevelSelectBack()
    {
        mainUI.SetActive(!mainUI.activeSelf);
        levelUI.SetActive(!levelUI.activeSelf);
    }

    public void ToggleMute()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }

        ChangeAudio();
    }

    private void ChangeAudio()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void UISound()
    {
        audioManager.Play("ButtonPress");
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
}
