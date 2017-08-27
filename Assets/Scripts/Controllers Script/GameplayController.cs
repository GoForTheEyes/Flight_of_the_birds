using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    [SerializeField]
    private Text scoreText, endScore, bestScore, gameOverText;

    [SerializeField]
    private Button restartGameButton, instructionsButton, pauseButton;

    [SerializeField]
    private GameObject pausePanel, unlockedPanel;

    [SerializeField]
    private GameObject[] birds;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;


    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f;
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if (BirdScript.instance != null)
        {
            if (BirdScript.instance.isAlive)
            {
                pausePanel.SetActive(true);
                gameOverText.gameObject.SetActive(false);
                unlockedPanel.SetActive(false);
                endScore.text = BirdScript.instance.score.ToString();
                bestScore.text = GameController.instance.GetHighscore().ToString();
                Time.timeScale = 0f;
                pauseButton.onClick.RemoveAllListeners();
                pauseButton.onClick.AddListener(() => pauseButton.gameObject.GetComponent<PauseButton>().SwitchPauseButtonImage());
                pauseButton.onClick.AddListener(() => ResumeGame());
            }
        }
    }

    public void GoToMenuButton()
    {
        SceneFader.instance.FadeIn("MainMenu");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(() => pauseButton.gameObject.GetComponent<PauseButton>().SwitchPauseButtonImage());
        pauseButton.onClick.AddListener(() => PauseGame());
    }

    public void RestartGame()
    {
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        unlockedPanel.SetActive(false);
        endScore.text = score.ToString();
        pauseButton.gameObject.SetActive(false);
        if (score > GameController.instance.GetHighscore())
        {
            GameController.instance.SetHighscore(score);
        }
        bestScore.text = GameController.instance.GetHighscore().ToString();

        if (score<=20)
        {
            medalImage.sprite = medals[0];
        }
        else if (score>20 && score <40) {
            medalImage.sprite = medals[1];
            if (GameController.instance.IsGreenBirdUnlocked()==0)
            {
                GameController.instance.UnlockGreenBird();
                unlockedPanel.SetActive(true);
            }
        }
        else if (score>40)
        {
            medalImage.sprite = medals[2];
            if (GameController.instance.IsGreenBirdUnlocked() == 0)
            {
                GameController.instance.UnlockGreenBird();
                unlockedPanel.SetActive(true);
            }
            else
            {
                if (GameController.instance.IsRedBirdUnlocked() == 0)
                {
                    GameController.instance.UnlockRedBird();
                    unlockedPanel.SetActive(true);
                }
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
