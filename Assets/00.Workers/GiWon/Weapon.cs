using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float coolDown = 0.5f;

    Player player;
    
    float timer;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > coolDown)
        {
            timer = 0f;
            Fire();
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
        bullet.GetComponent<Bullet>().Init(3, dir);
    }
}
