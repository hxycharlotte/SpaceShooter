using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int _lives = 3;
    public int _score = 0;
    public int _bestScore;


    public bool _isTripleShotActive = false;
    public bool _isSpeedBoostActive = false;
    public bool _isShieldsActive = false;

    public bool _isPlayerOne = false;
    public bool _isPlayerTwo = false;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;


    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;

    private SpawnManager _spawnManager;
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    [SerializeField]
    private UIManager _uiManager;
    private Animator _anim;


    // Audio
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {



        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        if (_gameManager._CoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source on the player is NULL.");
        }

        else
        {
            _audioSource.clip = _laserSoundClip;
        }

    }
        

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._CoOpMode == true)
        {
            if (_isPlayerOne == true)
            {

                CalculateMovement();

                if (Input.GetKeyDown(KeyCode.F) && Time.time > _canFire && _isPlayerOne == true)
                {
                    FireLaser();
                }
            }

            else if (_isPlayerTwo == true)
            {
                CoopMovement();

                if (Input.GetKeyDown(KeyCode.H) && Time.time > _canFire && _isPlayerTwo == true)
                {
                    CoopFireLaser();
                }
            }
        }

        else
        {
            CalculateMovement();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
        { 
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);

        }

        else
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5f), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11, 11), transform.position.y, 0);


    }

    void CoopMovement()
    {
        if (Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }


        if (_isSpeedBoostActive == false)
        {
            if (Input.GetKey(KeyCode.I))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.L))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.J))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.K))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

        }

        else if (_isSpeedBoostActive == true)
        {
            if (Input.GetKey(KeyCode.I))
            {
                transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.L))
            {
                transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.J))
            {
                transform.Translate(Vector3.left * _speed * 1.5f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.K))
            {
                transform.Translate(Vector3.down * _speed * 1.5f * Time.deltaTime);
            }
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 5f), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11, 11), transform.position.y, 0);


    }

    void FireLaser()
    {
        // visual
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
        }

        //audio
        _audioSource.Play();

    }
    void CoopFireLaser()
    {
        // visual
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
        }

        //audio
        _audioSource.Play();

    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        
        _lives --;

        if (_lives == 2)
        {
            Debug.Log("Activating left engine damage visual.");
            _leftEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            Debug.Log("Activating right engine damage visual.");
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            //Communicate with Spawn Manager:
            _spawnManager.OnPlayerDeath();
            _uiManager.ShowTitleScreen();
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(false);
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 1.8f);

        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore();
        _uiManager.CheckForBestScore();

    }


}




