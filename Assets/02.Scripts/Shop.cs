using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool isOpen = false;

    public GameObject[] buttons;
    public Transform showPos;
    private Transform[] showPosList;
    
    private GameObject[] showButtonList;

    void Start()
    {
        showPosList = showPos.GetComponentsInChildren<Transform>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //OpenShop();
    }


    public void OpenShop()
    {
        gameObject.SetActive(true);
        // weapon button list 중 4개 선택
        showButtonList = GetRandomButtons(4);
        // replace position
        for(int i=0; i<showButtonList.Length; i++)
        {
            showButtonList[i].SetActive(true);
            showButtonList[i].transform.position = showPosList[i+1].position;
        }
        
        isOpen = true;

        GameManager.instance.EnemyClean();
    }

    private GameObject[] GetRandomButtons(int count)
    {
        GameObject[] result = new GameObject[count];
        int[] indices = new int[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            indices[i] = i;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int randomIndex = Random.Range(i, buttons.Length);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            result[i] = buttons[indices[i]];
        }

        return result;
    }

    public void CloseShop()
    {
        for (int i = 0; i < showButtonList.Length; i++)
        {
            showButtonList[i].SetActive(false);
        }
        gameObject.SetActive(false);
        isOpen = false;

        GameManager.instance.ChangeWave();
    }
}
