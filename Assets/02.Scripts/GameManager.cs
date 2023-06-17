using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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
    public enum StateMode { Title, Playing, Shop, Stop, GameOver, GameClear }

    [Header("Game Control")]
    public StateMode state;
    //public bool isPlaying;
    public float gameTime;
    public float maxGameTime = 6 * 10f;
    public List<WaveData> waveDatas;
    public WaveData currentWave;
    public int waveIndex = 0;
    public VideoHandler videoHandler;
    [Header("Player Info")]
    public float health;
    public float maxHealth = 500;
    public int money = 300;
    public int kill;
    public int wave;
    [Header("Game Object")]
    public Player player;
    public PoolManager poolEnemy;
    public PoolManager poolBullet;
    public GameObject poolEnemyGO;
    public Shop shop;
    public Text myText;
    public Text waveTxt;
    public Text killTxt;
    public Slider hpBar;
    public GameObject deadUI;
    public GameObject videoObj;

    #endregion

    #region Unity Methods
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        videoHandler = GetComponent<VideoHandler>();
    }

    void Start()
    {
        GameStart();
    }

    void FixedUpdate()
    {
        //if (state == StateMode.GameOver)
        //{
        //    currentWave = waveDatas[0];
        //    maxHealth = 100;
        //    health = 100;
        //    money = 300;
        //    SceneManager.LoadScene("Title");
        //}
    }

    void Update()
    {
        if (state == StateMode.Title) return;

        myText.text = string.Format("GOLD : {0:F0}", money);
        if (currentWave.waveNumber <= 0) waveTxt.text = string.Format("Boss");
        else waveTxt.text = string.Format("Wave : {0}", currentWave.waveNumber);
        killTxt.text = string.Format("Kill\n{0} / {1}", kill, currentWave.killAmount);
        hpBar.value = health / maxHealth;

        //if (state != StateMode.Playing) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

        if (kill >= currentWave.killAmount && !shop.isOpen)
        {
            if (currentWave.waveNumber == -1)
            {
                videoObj.SetActive(true);
                GameClear();
            }
            else
            {
                shop.OpenShop();
                state = StateMode.Shop;
                player.inputVec = Vector2.zero;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            shop.OpenShop();
            state = StateMode.Shop;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(kill);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Hi");
            videoObj.SetActive(true);
            GameClear();
        }
    }

    void LateUpdate()
    {
        
    }

    #endregion

    #region Game Progress Methods

    public void SetWaveData()
    {
        waveDatas = new List<WaveData>();

        // Wave 1
        waveDatas.Add(new WaveData
        {
            waveNumber = 1,
            killAmount = 15,
            health = 200,
            attackDamage = 5,
            enemySpeed = GameManager.instance.player.speed * 0.5f,
            dropGoldMax = 35,
            dropGoldMin = 30
        });
        // Wave 2
        waveDatas.Add(new WaveData
        {
            waveNumber = 2,
            killAmount = 15,
            health = 250,
            attackDamage = 5,
            enemySpeed = GameManager.instance.player.speed * 0.5f,
            dropGoldMax = 40,
            dropGoldMin = 35
        });
        // Wave 3
        waveDatas.Add(new WaveData
        {
            waveNumber = 3,
            killAmount = 20,
            health = 250,
            attackDamage = 8,
            enemySpeed = GameManager.instance.player.speed * 0.5f,
            dropGoldMax = 45,
            dropGoldMin = 40
        });
        // Wave 4
        waveDatas.Add(new WaveData
        {
            waveNumber = 4,
            killAmount = 20,
            health = 300,
            attackDamage = 8,
            enemySpeed = GameManager.instance.player.speed * 0.5f,
            dropGoldMax = 50,
            dropGoldMin = 45
        });
        // Wave 5
        waveDatas.Add(new WaveData
        {
            waveNumber = 5,
            killAmount = 20,
            health = 300,
            attackDamage = 10,
            enemySpeed = GameManager.instance.player.speed * 0.5f,
            dropGoldMax = 55,
            dropGoldMin = 50
        });
        // Wave 6 - SubBoss
        waveDatas.Add(new WaveData
        {
            waveNumber = 0,
            killAmount = 3,
            health = 2000,
            attackDamage = 25,
            enemySpeed = GameManager.instance.player.speed * 0.6f,
            dropGoldMax = 300,
            dropGoldMin = 300
        });
        // Wave 7
        waveDatas.Add(new WaveData
        {
            waveNumber = 6,
            killAmount = 25,
            health = 350,
            attackDamage = 15,
            enemySpeed = GameManager.instance.player.speed * 0.7f,
            dropGoldMax = 70,
            dropGoldMin = 50
        });
        // Wave 8
        waveDatas.Add(new WaveData
        {
            waveNumber = 7,
            killAmount = 25,
            health = 350,
            attackDamage = 18,
            enemySpeed = GameManager.instance.player.speed * 0.7f,
            dropGoldMax = 80,
            dropGoldMin = 50
        });
        // Wave 9
        waveDatas.Add(new WaveData
        {
            waveNumber = 8,
            killAmount = 30,
            health = 400,
            attackDamage = 20,
            enemySpeed = GameManager.instance.player.speed * 0.7f,
            dropGoldMax = 90,
            dropGoldMin = 50
        });
        // Wave 10
        waveDatas.Add(new WaveData
        {
            waveNumber = 9,
            killAmount = 30,
            health = 450,
            attackDamage = 25,
            enemySpeed = GameManager.instance.player.speed * 0.7f,
            dropGoldMax = 100,
            dropGoldMin = 50
        });
        // Wave 11 - Fianl Boss
        waveDatas.Add(new WaveData
        {
            waveNumber = -1,
            killAmount = 1,
            health = 10000,
            attackDamage = 40,
            enemySpeed = GameManager.instance.player.speed * 0.6f,
            dropGoldMax = 0,
            dropGoldMin = 0
        });
    }

    public void GetMoney()
    {
        if (state != StateMode.Playing) return;

        money += Random.Range(currentWave.dropGoldMin, currentWave.dropGoldMax);
    }
    
    public void GameStart()
    {
        health = maxHealth;
        state = StateMode.Playing;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        state = StateMode.GameOver;
        Time.timeScale = 0;
        deadUI.SetActive(true);
    }

    public void GameClear()
    {
        state = StateMode.GameClear;
        videoHandler.mScreen.gameObject.SetActive(true);
        videoHandler.PlayVideo();
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
        GameManager.instance.kill = 0;
        state = StateMode.Playing;
        Debug.Log(currentWave + ", " + waveIndex);

    }

    public void EnemyClean()
    {
        foreach (List<GameObject> pool in poolEnemy.pools)
        {
            foreach (GameObject enemy in pool)
            {
                enemy.SetActive(false);
            }
        }
    }
    #endregion
}
