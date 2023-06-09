using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public int waveNumber;
    public int killAmount;
    public float health;
    public float attackDamage;
    public float enemySpeed;
    public int dropGoldMax, dropGoldMin;
}

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;
    public enum StateMode { Playing, Shop, Stop, GameOver }

    [Header("Game Control")]
    public StateMode state;
    //public bool isPlaying;
    public float gameTime;
    public float maxGameTime = 6 * 10f;
    public List<WaveData> waveDatas;
    public WaveData currentWave;
    public int waveIndex = 1;
    [Header("Player Info")]
    public float health;
    public float maxHealth = 100;
    public int money;
    public int kill;
    public int wave;
    [Header("Game Object")]
    public Player player;
    public PoolManager poolEnemy;
    public PoolManager poolBullet;
    public Shop shop;

    #endregion

    #region Unity Methods
    void Awake()
    {
        instance = this;

        health = maxHealth;

        waveDatas = new List<WaveData>();

        // Wave 1
        waveDatas.Add(new WaveData
        {
            waveNumber = 1,
            killAmount = 15,
            health = 200,
            attackDamage = 5,
            enemySpeed = GameManager.instance.player.speed * 0.9f,
            dropGoldMax = 35,
            dropGoldMin = 30
        });
        // Wave 2
        waveDatas.Add(new WaveData
        {
            waveNumber = 2,
            killAmount = 30,
            health = 250,
            attackDamage = 5,
            enemySpeed = GameManager.instance.player.speed * 0.9f,
            dropGoldMax = 40,
            dropGoldMin = 35
        });
        // Wave 3
        waveDatas.Add(new WaveData
        {
            waveNumber = 3,
            killAmount = 50,
            health = 250,
            attackDamage = 8,
            enemySpeed = GameManager.instance.player.speed * 0.9f,
            dropGoldMax = 45,
            dropGoldMin = 40
        });
        // Wave 4
        waveDatas.Add(new WaveData
        {
            waveNumber = 4,
            killAmount = 70,
            health = 300,
            attackDamage = 8,
            enemySpeed = GameManager.instance.player.speed * 0.9f,
            dropGoldMax = 50,
            dropGoldMin = 45
        });
        // Wave 5
        waveDatas.Add(new WaveData
        {
            waveNumber = 5,
            killAmount = 90,
            health = 300,
            attackDamage = 10,
            enemySpeed = GameManager.instance.player.speed * 0.9f,
            dropGoldMax = 55,
            dropGoldMin = 50
        });
        // Wave 6
        waveDatas.Add(new WaveData
        {
            waveNumber = 6,
            killAmount = 115,
            health = 350,
            attackDamage = 15,
            enemySpeed = GameManager.instance.player.speed * 0.95f,
            dropGoldMax = 70,
            dropGoldMin = 50
        });
        // Wave 7
        waveDatas.Add(new WaveData
        {
            waveNumber = 7,
            killAmount = 140,
            health = 350,
            attackDamage = 18,
            enemySpeed = GameManager.instance.player.speed * 0.95f,
            dropGoldMax = 80,
            dropGoldMin = 50
        });
        // Wave 8
        waveDatas.Add(new WaveData
        {
            waveNumber = 8,
            killAmount = 170,
            health = 400,
            attackDamage = 20,
            enemySpeed = GameManager.instance.player.speed * 0.95f,
            dropGoldMax = 90,
            dropGoldMin = 50
        });
        // Wave 9
        waveDatas.Add(new WaveData
        {
            waveNumber = 9,
            killAmount = 200,
            health = 450,
            attackDamage = 25,
            enemySpeed = GameManager.instance.player.speed * 0.95f,
            dropGoldMax = 100,
            dropGoldMin = 50
        });

        currentWave = waveDatas[0];
    }

    void Update()
    {
        if (state != StateMode.Playing) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }


        if (kill == currentWave.killAmount && !shop.isOpen)
        {
            shop.OpenShop();
            state = StateMode.Shop;
            //ChangeWave();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            shop.OpenShop();
            state = StateMode.Shop;
        }
    }

    #endregion

    #region Game Progress Methods

    public void GetMoney()
    {
        if (state != StateMode.Playing) return;

        money += Random.Range(currentWave.dropGoldMin, currentWave.dropGoldMax);
    }
    
    public void GameStart()
    {
        health = maxHealth;
        state = StateMode.Playing;
    }

    public void GameOver()
    {
        state = StateMode.GameOver;
    }

    public void Stop()
    {
        state = StateMode.Stop;
        Time.timeScale = 0;
    }

    public void ChangeWave()
    {
        if (waveIndex < 0 || waveIndex >= waveDatas.Count) return;

        currentWave = waveDatas[waveIndex];
        waveIndex++;
        state = StateMode.Playing;
    }
    #endregion
}
