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


    // ü�� ȸ��, ����, �̵� ���� �޼��带 �߰��� �ۼ��մϴ�.

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
        // �޸���
        //if (Input.GetKeyDown())
        //{
        //    this.animator.SetTrigger("RunTrigger");
        //}

        // �����Ѵ�.
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("RunTrigger");
        }

        // Ȱ ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isBowAttacking");
        }

        // ���� ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isMagicAttacking");
        }

        // �׳� ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isNormalAttacking");
        }

        // Ȱ Ư�� ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isBowCriticalAttacking");
        }

        // ���� Ư�� ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isMagicCriticalAttacking");
        }

        // �׳� Ư�� ����
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.animator.SetTrigger("isNormalCriticalAttacking");
        }

        //// ���� �ʿ�
        //if (/*�÷��̾��� ü���� 0�� �Ǿ�����*/)
        //{
        //    this.animator.SetTrigger("Death");
        //}


        // �¿� �̵�
        int key = 0;
        if (Input.GetKey(KeyCode.D))
            key = 1;
        if (Input.GetKey(KeyCode.A))
            key = -1;

        // �÷��̾��� �ӵ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // ���ǵ� ����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // �����̴� ���⿡ ���� �����Ѵ�.
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // �÷��̾� �ӵ��� ���� �ִϸ��̼� �ӵ��� �ٲ۴�.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }


        // �浹 ������ ���� �÷��̾���..
        void OnTriggerEnter2D(Collider other)
        {
            Debug.Log("�浹");
        }
    }
}
