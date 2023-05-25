using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float coolDown = 0.5f;

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    Player player;
    
    float timer;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.L))
            LevelUp(20, 5);
        //timer += Time.deltaTime;

        //if (timer > coolDown)
        //{
        //    timer = 0f;
        //    Fire();
        //}
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Batch();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -159;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for(int i=0; i < count; i++)
        {
            Transform bullet;
            
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.right * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is infinity per
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(3, 1);
    }
}
