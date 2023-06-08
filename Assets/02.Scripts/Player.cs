using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    
    public Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    private void Awake()
    {
        scanner = GetComponent<Scanner>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal"); //x축 이동
        inputVec.y = Input.GetAxisRaw("Vertical"); //y축 이동
    }
    void FixedUpdate()
    {
        // 3. 위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isPlaying) return;

        GameManager.instance.health -= GameManager.instance.currentWave.attackDamage;
    }
}
