using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.SceneManagement;

public class LeaderboardsController : MonoBehaviour {

    public static LeaderboardsController instance;

    //Leaderboard ID from google services
    private const string LEADERBOARDS_SCORE = "CgkIwpSR2fMfEAIQAQ";

    // Use this for initialization
    void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
    }

    private void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        ReportScore(GameController.instance.GetHighscore());

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

    public void ConnectOrDisconnectOnGooglePlayGames()
    {
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
        }
        else
        {
            Social.localUser.Authenticate((bool success) => {});
        }
    }

    public void OpenLeaderboardsScore()
    {
    if (Social.localUser.authenticated)
            //PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARDS_SCORE);
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(LEADERBOARDS_SCORE);
    }

    void ReportScore(int score)
    {
        if(Social.localUser.authenticated)
        {
            Social.ReportScore(score, LEADERBOARDS_SCORE, (bool success) =>
            { });

        }
    }


}
