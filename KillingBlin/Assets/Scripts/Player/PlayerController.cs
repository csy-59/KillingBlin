using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    Animator animator;

    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 10;


    // 체력 회복, 공격, 이동 등의 메서드를 추가로 작성합니다.

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // 달린다
        //if (Input.GetKeyDown())
        //{
        //    this.animator.SetTrigger("RunTrigger");
        //}

        // 점프한다.
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("RunTrigger");
        }

        // 활 공격
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isBowAttacking");
        }

        // 마법 공격
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isMagicAttacking");
        }

        // 그냥 공격
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isNormalAttacking");
        }

        // 활 특수 공격
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isBowCriticalAttacking");
        }

        // 마법 특수 공격
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isMagicCriticalAttacking");
        }

        // 그냥 특수 공격
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isNormalCriticalAttacking");
        }

        //// 수정 필요
        //if (/*플레이어의 체력이 0이 되었을때*/)
        //{
        //    this.animator.SetTrigger("Death");
        //}


        // 좌우 이동
        int key = 0;
        if (Input.GetKey(KeyCode.D))
            key = 1;
        if (Input.GetKey(KeyCode.A))
            key = -1;

        // 플레이어의 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 스피드 제한
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 움직이는 방향에 따라 반전한다.
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 플레이어 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }


        // 충돌 판정에 의해 플레이어의..
        void OnTriggerEnter2D(Collider other)
        {
            Debug.Log("충돌");
        }
    }
}
