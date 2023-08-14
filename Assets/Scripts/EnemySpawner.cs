using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemySpawnInterval = 1f;

    private AudioManager audioManager;
    
    public List<Enemy> enemyList;
    
    private void Awake()
    {
        enemyList = new();
    }
    
    public void Initialize(AudioManager manager)
    {
        audioManager = manager;
    }

    private void Start()
    {
        StartSpawn();
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(enemySpawnInterval);
        enemySpawnInterval = Random.Range(1f, 2f);
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Initialize(audioManager);
        enemyList.Add(enemy);
        StartCoroutine(Spawn());
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }
}