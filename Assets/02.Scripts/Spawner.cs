using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variables
    public GameObject spawnPointGroup;
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;
    int spawnBoss = 0;

    #endregion

    #region Unity Methods
    void Awake()
    {
        spawnPoint = spawnPointGroup.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (GameManager.instance.state != GameManager.StateMode.Playing) return;

        timer += Time.deltaTime;
        if (GameManager.instance.currentWave.waveNumber == 0)
        {
            level = 1;
            if (spawnBoss < 3)
            {
                Spawn();
                spawnBoss++;
            }
            timer = 0;
        }
        else if (GameManager.instance.currentWave.waveNumber == -1)
        {
            level = 2;
            if (spawnBoss < 1)
            {
                Spawn();
                spawnBoss++;
            }
            timer = 0;
        }
        else
        {
            level = 0;
            spawnBoss = 0;
            if (timer > spawnData[level].spawnTime)
            {
                Spawn();
                timer = 0;

            }
        }
        
    }

    #endregion

    void Spawn()
    {
        GameObject enemy = null;
        if (GameManager.instance.currentWave.waveNumber == 0)
        {
            enemy = GameManager.instance.poolEnemy.Get(1);
        }
        else if (GameManager.instance.currentWave.waveNumber == -1)
        {
            enemy = GameManager.instance.poolEnemy.Get(2);
        }
        else
        {
            enemy = GameManager.instance.poolEnemy.Get(0);
        }
        
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(GameManager.instance.currentWave);
    }

    
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}