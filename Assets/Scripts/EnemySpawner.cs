using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemySpawnInterval = 1f;
    public List<Enemy> enemyList;

    private void Awake()
    {
        enemyList = new();
    }

    private void Start()
    {
        StartSpawn();
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(enemySpawnInterval);
        enemySpawnInterval = Random.Range(1f, 2f);
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyList.Add(enemy.GetComponent<Enemy>());
        StartCoroutine(Spawn());
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }
}