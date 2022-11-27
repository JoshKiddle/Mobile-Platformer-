using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public const string Crystals = "Crystals";
    public static int permCrystals = 0;

    public Text shopCurrencyText;
    public Text crystalCurrencyText;

    // Start is called before the first frame update
    void Start()
    {
        permCrystals = PlayerPrefs.GetInt("Crystals");
    }

    // Update is called once per frame
    void Update()
    {
        shopCurrencyText.text = "" + permCrystals;
        crystalCurrencyText.text = "" + permCrystals;
    }

    public static void UpdateCurrency()
    {
        Debug.Log("Currency updated");
        PlayerPrefs.SetInt("Crystals", permCrystals);
        permCrystals = PlayerPrefs.GetInt("Crystals");
        PlayerPrefs.Save();
    }
}
