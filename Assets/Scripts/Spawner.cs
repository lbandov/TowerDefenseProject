using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;


    [Header("Poolers")]
    [SerializeField] private ObjectPooler enemyWave1Pooler;

    private float _spawnTimer;
    private int _enemiesSpawned;

    private ObjectPooler _pooler;
    private Waypoint _waypoint;
    // Start is called before the first frame update
    void Start()
    {
        _waypoint = GetComponent<Waypoint>();
        _pooler = GetComponent<ObjectPooler>();
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

        newInstance.SetActive(true);
        
    }

    private ObjectPooler GetPooler()
    {
        return enemyWave1Pooler;
    }
}
