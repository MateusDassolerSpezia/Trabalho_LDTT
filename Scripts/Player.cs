using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 3;

    public bool isShieldActive = false;
    public bool isTripleShotActive = false;
    public bool isSpeedBoostActive = false;

    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldGameObject;

    [SerializeField] private GameObject[] _enginesDamage;

    [SerializeField] private float _fireRate = 0.25f;

    private float _canFire = 0.0f;

    [SerializeField] private float _moveSpeed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private float _hitCount;

    void Start()
    {
        transform.position = Vector3.zero;

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

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) /*Input.GetKeyDown(KeyCode.Mouse0))*/)
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
            transform.Translate(Vector3.right * _moveSpeed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _moveSpeed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _moveSpeed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _moveSpeed * verticalInput * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

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

            if (isTripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            } 
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }                
    }

    public void TakeDamage()
    { 
        if (isShieldActive)
        {
            isShieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        _hitCount++;

        if (_hitCount == 1)
        {
            _enginesDamage[0].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _enginesDamage[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _spawnManager.StartSpawnRoutine();
            _uiManager.ShowTitleScream();
            Destroy(this.gameObject);
        }       
    }

    public void TripleShotPowerupOn()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerupOffRoutine(5));
    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerupOffRoutine(5));
    }

    public void ShieldPowerupOn()
    {
        isShieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerupOffRoutine(float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        isTripleShotActive = false;
    }

    public IEnumerator SpeedBoostPowerupOffRoutine(float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        isSpeedBoostActive = false;
    }

}
