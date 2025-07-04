using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyShipPrefab;
    [SerializeField] private GameObject[] _powerupsPrefabs;

    private GameManager _gameManager;
    private UIManager _uiManager;

    [SerializeField] private float _spawnTimeInterval = 5;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void StartSpawnRoutine()
    {
        _spawnTimeInterval = 5f;
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            if (_uiManager.score == 50)
            {
                _spawnTimeInterval = 3f;
            }
            float randomX = Random.Range(-7.6f, 7.6f);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 7, 0), Quaternion.identity);
            
            yield return new WaitForSeconds(_spawnTimeInterval);
        }
    }

    private IEnumerator PowerupSpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            int randomPowerup = Random.Range(0, 3);
            float randomX = Random.Range(-7.6f, 7.6f);           
            Instantiate(_powerupsPrefabs[randomPowerup], new Vector3(randomX, 7, 0), Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }        
    }
}
