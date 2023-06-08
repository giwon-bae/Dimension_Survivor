using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] buttons;
    public Transform weaponList;
    private Transform[] weaponListPos;

    void Start()
    {
        weaponListPos = weaponList.GetComponentsInChildren<Transform>();
    }


    public void OpenShop()
    {
        GameObject[] showButtonList;
        // weapon button list 중 4개 선택
        showButtonList = GetRandomButtons(4);
        // replace position
        for(int i=0; i<showButtonList.Length; i++)
        {
            Debug.Log(showButtonList[i].transform.position + " " + weaponListPos[i + 1].position);
            showButtonList[i].transform.position = weaponListPos[i+1].position;
            Debug.Log(showButtonList[i].transform.position);
        }
    }

    private GameObject[] GetRandomButtons(int count)
    {
        GameObject[] result = new GameObject[count];
        int[] indices = new int[count];

        for (int i=0; i<count; i++)
        {
            indices[i] = i;
        }

        for (int i = 0; i < count - 1; i++)
        {
            int randomIndex = Random.Range(i, count);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            result[i] = buttons[indices[i]];
            //Debug.Log(result[i]);
        }

        return result;
    }

    public void CloseShop()
    {

    }
}
