using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsController : MonoBehaviour {

    public static AdsController instance;

    // Use this for initialization
    void Awake()
    {
        MakeSingleton();
    }

    // Use this for initialization
    void Start()
    {
        AppLovin.InitializeSdk();
        AppLovin.SetUnityAdListener(this.gameObject.name);
    }

    private void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            int random = Random.Range(0, 20);
            if (random == 19)
            {
                ShowInterstitial();
            } else if (random == 20)
            {
                ShowVideo();
            }
        }

    }

    //Delegate that listens when scene is loaded
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    //Delegate that stops listing when scene is unloaded
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }


    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadInterstitial()
    {
        AppLovin.PreloadInterstitial();
    }

    public void ShowInterstitial()
    {
        if (AppLovin.HasPreloadedInterstitial())
        {
            AppLovin.ShowInterstitial();
        } 
        else
        {
            LoadInterstitial();
        }
    }

    public void LoadVideo()
    {
        AppLovin.LoadRewardedInterstitial();
    }

    public void ShowVideo()
    {
        AppLovin.ShowRewardedInterstitial();
    }


    void OnAppLovinEventReceived(string ev)
    {
        if (ev.Contains("DISPLAYEDINTER"))
        {
            // An ad was shown.  Pause the game.
        }
        else if (ev.Contains("HIDDENINTER"))
        {
            // Ad ad was closed.  Resume the game.
            // If you're using PreloadInterstitial/HasPreloadedInterstitial, make a preload call here.
            AppLovin.PreloadInterstitial();
        }
        else if (ev.Contains("LOADEDINTER"))
        {
            // An interstitial ad was successfully loaded.
        }
        else if (string.Equals(ev, "LOADINTERFAILED"))
        {
            // An interstitial ad failed to load.

        }
        else if (ev.Contains("REWARDAPPROVEDINFO"))
        {

            //// The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY" so "REWARDAPPROVEDINFO|10|Coins" for example
            //string delimeter = "|";

            //// Split the string based on the delimeter
            //string[] split = ev.Split(delimeter);

            //// Pull out the currency amount
            //double amount = double.Parse(split[1]);

            //// Pull out the currency name
            //string currencyName = split[2];

            //// Do something with the values from above.  For example, grant the coins to the user.
            //updateBalance(amount, currencyName);
        }
        else if (ev.Contains("LOADEDREWARDED"))
        {
            // A rewarded video was successfully loaded.
        }
        else if (ev.Contains("LOADREWARDEDFAILED"))
        {
            // A rewarded video failed to load.
            LoadVideo();
        }
        else if (ev.Contains("HIDDENREWARDED"))
        {
            // A rewarded video was closed.  Preload the next rewarded video.
            LoadVideo();
        }
    }

}
