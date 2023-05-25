using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    //public Rigidbody2D rigid;

    private void Awake()
    {
        //rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
        //rigid.velocity = dir * 3f;
    }
}
