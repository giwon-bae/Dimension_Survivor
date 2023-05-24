using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject[] Panels;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            InitGame();
        }
    }

    private void InitGame()
    {
        Panels[0].SetActive(true);
        for(int i=1; i<Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
    }

    public void OnPlayerSelect()
    {
        Panels[0].SetActive(false);
        Panels[1].SetActive(true);
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
