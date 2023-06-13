using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public GameObject visualObj;

    public Image icon;
    Text levelTxt;
    Text nameTxt;
    Text costTxt;

    private void Awake()
    {
        //icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        levelTxt = texts[0];
        nameTxt = texts[1];
        costTxt = texts[2];
    }

    private void LateUpdate()
    {
        levelTxt.text = "" + level;
        nameTxt.text = data.itemName;
        costTxt.text = "" + data.costs[level];
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (GameManager.instance.money < data.costs[level])
                    return;
                //show 
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                    visualObj.SetActive(true);
                }
                else
                {
                    float nextDamage = data.damages[level];
                    int nextCount = data.counts[level];
                    int nextPnt = data.pnts[level];

                    weapon.LevelUp(nextDamage, nextCount, nextPnt);
                }
                GameManager.instance.money -= data.costs[level];
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health += 20; //수치 변경 필요
                break;
        }

        level++;

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
