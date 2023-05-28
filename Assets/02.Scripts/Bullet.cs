using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int prefabId;
    public int per;

    Rigidbody2D rigid;
    public Scanner scanner;
    public Transform targetTransform;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (prefabId)
        {
            case 2:
                transform.Rotate(Vector3.forward * -240 * Time.deltaTime);
                break;
        }
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per > -1)
        {
            rigid.velocity = dir * 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        if(prefabId == 2)//Axe
        {
            collision.gameObject.layer = LayerMask.NameToLayer("Hitted");
            scanner = GetComponent<Scanner>();
            targetTransform = scanner.GetNearest();
            if (targetTransform != null)
            {
                Vector3 targetPos = targetTransform.position;
                Vector3 dir = targetPos - transform.position;
                dir = dir.normalized;

                rigid.velocity = dir * 5f;
            }
        }

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
