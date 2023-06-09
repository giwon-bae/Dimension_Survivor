using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float damage = 0;
    public float speed;
    public float coolDown = 0;
    public FloatingJoystick variableJoystick;

    public Scanner scanner;
    public GameObject hands;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;


    private void Awake()
    {
        scanner = GetComponent<Scanner>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //inputVec.x = Input.GetAxisRaw("Horizontal"); //x축 이동
        //inputVec.y = Input.GetAxisRaw("Vertical"); //y축 이동
    }
    void FixedUpdate()
    {
        if (GameManager.instance.state != GameManager.StateMode.Playing) return;

        // 3. 위치 이동
        inputVec.x = variableJoystick.Horizontal;
        inputVec.y = variableJoystick.Vertical;
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x > 0;
            float sc = inputVec.x > 0 ? 1 : -1;
            hands.transform.localScale = new Vector3(sc, 1, 1);
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.instance.state != GameManager.StateMode.Playing) return;

        GameManager.instance.health -= GameManager.instance.currentWave.attackDamage;
        if (GameManager.instance.health <= 0)
            GameManager.instance.GameOver();
    }
}
