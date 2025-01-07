using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText, bestText;
    public int _score;
    public int _bestScore;


    [SerializeField] 
    private Text _gameoverText;
    [SerializeField] 
    private Text _restartText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private GameManager _gameManager;
    private GameObject titleScreen;




    // Start is called before the first frame update
    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        scoreText.text = "Score: " + _score;
        bestText.text = "Best: " + _bestScore;
        _gameoverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }


    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);

    }

        public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";

    }

    public void UpdateScore()
    {
        _score += 10;
        scoreText.text = "Score: " + _score;

    }

    public void CheckForBestScore()
    {
        if (_score > _bestScore)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("BestScore", _bestScore);
            bestText.text = "Best: " + _bestScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    private IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu_Choose");

    }

}
