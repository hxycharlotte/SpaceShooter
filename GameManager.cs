using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _GameOver = true;
    public bool _CoOpMode = false;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _coopPlayer;


    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _pauseMenuPanel;


    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (_GameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                if (_CoOpMode == false)
                {
                    Instantiate(_player, Vector3.zero, Quaternion.identity);

                }

                else
                {
                    Instantiate(_coopPlayer, Vector3.zero, Quaternion.identity);
                }

                _GameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.StartSpawning();
                _audioSource.Play();

            }

            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(1); // Game Menu Scene
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R) && _GameOver == true)
        {
            // Restart the game
            if (_CoOpMode == true) 
            {
                SceneManager.LoadScene(3); // Co-op Game Scene
            }
            else
            {
                SceneManager.LoadScene(2); // Single Game Scene
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }


    }

    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() Called");
        _GameOver = true;
    }   

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
