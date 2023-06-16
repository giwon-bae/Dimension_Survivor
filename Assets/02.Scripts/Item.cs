using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public SpriteRenderer weaponSprite;
    public GameObject visualObj;
    public Button purchaseButton;

    public Image icon;
    Text levelTxt;
    //Text nameTxt;
    Text costTxt;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
            case ItemData.ItemType.Potion:
                levelTxt = texts[0];
                //nameTxt = texts[1];
                costTxt = texts[1];
                break;
            case ItemData.ItemType.Heal:
                costTxt = texts[0];
                break;
        }
    }

    private void LateUpdate()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
            case ItemData.ItemType.Potion:
                levelTxt.text = "" + level;
                if (level >= data.costs.Length)
                    costTxt.text = "-";
                else
                    costTxt.text = "" + data.costs[level];
                break;
            case ItemData.ItemType.Heal:
                costTxt.text = "" + data.costs[level];
                break;
        }
        
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
        if (GameManager.instance.money < data.costs[level])
            return;
        
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                
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
                level++;

                if (level == data.damages.Length - 1)
                {
                    icon.sprite = data.maxLevelIcon;
                }
                break;
            case ItemData.ItemType.Potion:
                switch (data.itemId)
                {
                    case 11:
                        GameManager.instance.player.damage += data.damages[level];
                        break;
                    case 12:
                        GameManager.instance.maxHealth += data.damages[level];
                        break;
                    case 13:
                        GameManager.instance.player.coolDown = data.damages[level];
                        break;
                    case 14:
                        GameManager.instance.player.speed = data.damages[level];
                        break;
                }
                GameManager.instance.money -= data.costs[level];
                level++;
                break;
            case ItemData.ItemType.Heal:
                if (GameManager.instance.health == GameManager.instance.maxHealth)
                    return;

                    int healAmount = (int)(GameManager.instance.maxHealth * 0.3f);
                if (GameManager.instance.health + healAmount > GameManager.instance.maxHealth)
                    GameManager.instance.health = GameManager.instance.maxHealth;
                else
                    GameManager.instance.health += healAmount;

                GameManager.instance.money -= data.costs[level];
                break;
        }

        if (level == data.damages.Length)
        {
            purchaseButton.interactable = false;
            if(data.maxLevelIcon != null)
                weaponSprite.sprite = data.maxLevelIcon;
        }
    }
}
