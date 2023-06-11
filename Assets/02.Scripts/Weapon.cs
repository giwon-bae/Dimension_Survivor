using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int prefabId;
    public float damage;
    public int count;
    public int pnt;
    public float speed;
    public float delayTime = 0.5f;

    Player player;
    
    float timer;

    private void Awake()
    {
        player = GameManager.instance.player;
        //Init();
    }

    private void Update()
    {
        if (GameManager.instance.state != GameManager.StateMode.Playing) return;

        switch (prefabId)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float damage, int count, int pnt)
    {
        this.damage = damage;
        this.count = count;
        this.pnt = pnt;

        if(prefabId == 0)
        {
            Batch();
        }
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Property Set
        prefabId = data.itemId;
        damage = data.damages[0];
        count = data.counts[0];
        pnt = data.pnts[0];

        switch (prefabId)
        {
            case 0:
                speed = -159;
                Batch();
                break;
            default:
                speed = 3f;
                break;
        }
    }

    void Batch()
    {
        for(int i=0; i<count; i++)
        {
            Transform bullet;
            
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.poolBullet.Get(prefabId).transform;
                
            }
            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            Debug.Log(bullet.localPosition + " " + bullet.localRotation);

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.right * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, pnt, Vector3.zero); // -1 is infinity per
        }
    }

    private void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        if(prefabId == 4)
        {
            StartCoroutine(IFireSpread());
            return;
        }
        StartCoroutine(IFire());
    }

    IEnumerator IFire()
    {
        Vector3 latestTargetPos = player.scanner.nearestTarget.position;
        for (int i = 0; i < count; i++)
        {
            Vector3 targetPos;
            if (player.scanner.nearestTarget == null)
                targetPos = latestTargetPos;
            else
            {
                targetPos = player.scanner.nearestTarget.position;
                latestTargetPos = targetPos;
            }
                
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.instance.poolBullet.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            bullet.GetComponent<Bullet>().Init(damage, pnt, dir);
            yield return new WaitForSeconds(delayTime / count);
        }
    }

    [SerializeField]
    private float spreadAngle = 45f;

    IEnumerator IFireSpread()
    {
        Vector3 targetPos = player.scanner.nearestTarget.position;
        targetPos.z = 0f;

        Vector3 direction = targetPos - transform.position;
        float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startAngle = centerAngle - spreadAngle / 2f;
        float angleStep = spreadAngle / (count - 1);

        for (int i = 0; i < count; i++)
        {
            float currentAngle = startAngle + angleStep * i;

            Vector2 dir = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            Transform bullet = GameManager.instance.poolBullet.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            bullet.GetComponent<Bullet>().Init(damage, pnt, dir);
        }
        yield return null;
    }
}
