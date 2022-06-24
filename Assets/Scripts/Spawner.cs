using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Action OnWaveCompleted;


    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;


    [Header("Poolers")]
    [SerializeField] private ObjectPooler enemyWave1Pooler;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;

    private Waypoint _waypoint;
    // Start is called before the first frame update
    void Start()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
        
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave <= 1) // 1- 10
        {
            return enemyWave1Pooler;
        }

        return null;
    }

    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0)
        {
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }
}
