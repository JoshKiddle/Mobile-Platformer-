using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{

    public string gameID;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameID);
        Advertisement.AddListener(this);
    }

    public void PlayRewardedAd()
    {
        if(Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
        else
        {
            Debug.Log("Couldn't play ad");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            Debug.Log("Give a reward");

            Currency.permCrystals += 75;
            Currency.UpdateCurrency();
        }
    }

}
