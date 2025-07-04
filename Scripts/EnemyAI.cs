using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float _enemySpeed = 2.0f;

    [SerializeField] private GameObject _enemyExplosionPrefab;

    [SerializeField] private AudioClip _SFX_ExplosionClip;

    private UIManager _uiManager;

    private GameManager _gameManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();    
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        EnemyMovement();
        CheckEnemyBoundsAndRespawn();
    }

    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
    }

    private void CheckEnemyBoundsAndRespawn()
    {
        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7.6f, 7.6f);
            transform.position = new Vector3(randomX, 7, 0);
        } else if (_gameManager.gameOver)
        {
            Destroy(this.gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            HandleLaserCollision(other);
        }
        else if (other.tag == "Player")
        {
            HandlePlayerCollision(other);
        }
    }

    private void HandleLaserCollision(Collider2D other)
    {
        if (other.transform.parent != null)
        {
            Destroy(other.transform.parent.gameObject);
        }

        Destroy(other.gameObject);
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        _uiManager.UpdateScore();
        AudioSource.PlayClipAtPoint(_SFX_ExplosionClip, Camera.main.transform.position, 1f);
        Destroy(this.gameObject);
    }

    private void HandlePlayerCollision(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage();
        }
        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_SFX_ExplosionClip, Camera.main.transform.position, 1f);
        Destroy(this.gameObject);
    }
}
