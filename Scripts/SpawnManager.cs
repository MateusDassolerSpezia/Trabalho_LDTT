using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

    private UIManager _uiManager;

    [SerializeField]
    private float _spawnTime = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        /*StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());*/
    }

    public void StartSpawnRoutine()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            if (_uiManager.score == 50)
            {
                _spawnTime = 3;
            } 
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.6f, 7.6f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-7.6f, 7.6f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }        
    }
}
