using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Control")]
    public bool isPlaying;
    public float gameTime;

    [Header("Game Object")]
    public Player player;
    public PoolManager pool;

    void Awake()
    {
        instance = this;
    }
}
