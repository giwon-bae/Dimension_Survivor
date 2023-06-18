using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject[] Panels;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            TitleSetting();
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            MainSetting();
        }
    }

    private void TitleSetting()
    {
        Panels[0].SetActive(true);
        for(int i=1; i<Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
        GameManager.instance.state = GameManager.StateMode.Title;
    }

    private void MainSetting()
    {
        GameManager.instance.state = GameManager.StateMode.Shop;
        GameManager.instance.shop.OpenShop();
        GameManager.instance.SetWaveData();
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

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Main");
    }
}
