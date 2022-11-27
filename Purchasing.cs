using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchasing : MonoBehaviour
{
    private string smallCrystals = "com.joshkiddle.sector29.250crystals";
    private string mediumCrystals = "com.joshkiddle.sector29.500crystals";
    private string largeCrystals = "com.joshkiddle.sector29.1000crystals";

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == smallCrystals)
        {
            // add 250 crystals
            Debug.Log("added 250 crystals");

            Currency.permCrystals += 250;
            Currency.UpdateCurrency();

        }
        else if(product.definition.id == mediumCrystals)
        {
            // add 500 crystals
            Debug.Log("added 500 crystals");

            Currency.permCrystals += 500;
            Currency.UpdateCurrency();
        }
        else if(product.definition.id == largeCrystals)
        {
            // add 1000 crystals
            Debug.Log("added 1000 crystals");

            Currency.permCrystals += 1000;
            Currency.UpdateCurrency();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of " + product.definition.id + " failed due to " + reason);
    }

}
