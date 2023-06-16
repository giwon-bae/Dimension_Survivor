using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool isOpen = false;

    public GameObject[] weaponButtons;
    public GameObject[] potionButtons;
    public GameObject healButton;
    public Transform showPos;
    public Transform[] showPosList;
    public Transform[] showPosList2;

    private GameObject[] showButtonList;
    private GameObject[] showButtonList2;

    void Start()
    {
        //showPosList = showPos.GetComponentsInChildren<Transform>();
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
        showButtonList = GetRandomButtons(weaponButtons, 2);
        showButtonList2 = GetRandomButtons(potionButtons, 1);
        // replace position
        for (int i=0; i<showButtonList.Length; i++)
        {
            showButtonList[i].SetActive(true);
            showButtonList[i].transform.position = showPosList[i].position;
        }
        showButtonList2[0].SetActive(true);
        showButtonList2[0].transform.position = showPosList2[0].position;
        healButton.SetActive(true);
        healButton.transform.position = showPosList2[1].position;

        isOpen = true;

        GameManager.instance.EnemyClean();
    }

    private GameObject[] GetRandomButtons(GameObject[] buttons, int count)
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
        showButtonList2[0].SetActive(false);
        healButton.SetActive(false);
        gameObject.SetActive(false);
        isOpen = false;

        GameManager.instance.ChangeWave();
    }
}
