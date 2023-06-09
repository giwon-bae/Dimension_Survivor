using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
    public float speed;
    public float health;
    public float maxHealth;

    public RuntimeAnimatorController[] animCons;
    public Rigidbody2D target;

    bool isAlive;
    int layerNum;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    #endregion

    #region Unity Methods
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        layerNum = LayerMask.NameToLayer("Enemy");
    }

    void FixedUpdate()
    {
        if (GameManager.instance.state != GameManager.StateMode.Playing) return;

        //if (!isAlive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (GameManager.instance.state != GameManager.StateMode.Playing) return;

        spriter.flipX = target.position.x > rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isAlive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        health = maxHealth;
        gameObject.layer = layerNum;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isAlive)
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        gameObject.layer = layerNum;

        if (health > 0)
        {
            //anim.SetTrigger("Hit");
        }
        else
        {
            isAlive = false;
            coll.enabled = false;
            rigid.simulated = false;
            GameManager.instance.GetMoney();
            GameManager.instance.kill++;
            gameObject.layer = 0;

        }
    }

    #endregion

    public void Init(WaveData data)
    {
        speed = data.enemySpeed;
        maxHealth = data.health;
        health = data.health;
        // attack damage

        //anim.runtimeAnimatorController = animCons[data.spriteType];
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 50, ForceMode2D.Impulse);
    }
}
