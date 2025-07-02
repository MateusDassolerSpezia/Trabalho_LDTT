using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Public or private identify
    // Data type (int, floats, bool, strings)
    // Every variable has a NAME
    // Optional value assigned

    public int lives = 3;

    public bool shieldActive = false;
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private float _hitCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Current pos = new position
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine();
        }

        _audioSource = GetComponent<AudioSource>();

        _hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) /*Input.GetKeyDown(KeyCode.Mouse0))*/)
        {
            Shoot();
        }
    }


    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        // Y position limits
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        // X position limits
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot == true)
            {
                /*
                // Center
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
                // Right
                Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0, 0), Quaternion.identity);
                // Left
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0, 0), Quaternion.identity);
                */

                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            } 
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }                
    }

    public void Damage()
    { 
        if (shieldActive == true)
        {
            shieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        _hitCount++;

        if (_hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            //Vector3 position = transform.position;
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _spawnManager.StartSpawnRoutine();
            _uiManager.ShowTitleScream();
            Destroy(this.gameObject);
        }       
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldPowerupOn()
    {
        shieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

}
