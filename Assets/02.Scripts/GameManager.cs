using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public bool isPlaying;
    public float gameTime;
    public float maxGameTime = 6 * 10f;
    [Header("Player Info")]
    public float health;
    public float maxHealth = 100;
    public int money;
    [Header("Game Object")]
    public Player player;
    public PoolManager pool;

    #region Unity Methods
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isPlaying) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    #endregion

    #region Game Progress Methods

    public void GetMoney()
    {
        if (!isPlaying) return;

        money++;
    }
    
    public void GameStart()
    {
        health = maxHealth;

    }

    public void GameOver()
    {

    }

    public void Stop()
    {
        isPlaying = false;
        Time.timeScale = 0;
    }


    #endregion
}
