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
        animator = GetComponentInChildren<Animator>();

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
            this.animator.SetTrigger("Move");
        }

        // Ȱ ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.animator.SetTrigger("Bow_Attack");
            Debug.Log("�׽�Ʈ");
        }

        // ���� ����
        if (Input.GetKeyDown(KeyCode.X))
        {
            this.animator.SetTrigger("Magic_Attack");
        }

        // �׳� ����
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.animator.SetTrigger("Melee_Attack");
        }

        // Ȱ Ư�� ����
        if (Input.GetKeyDown(KeyCode.V))
        {
            this.animator.SetTrigger("Bow_Skill");
        }

        // ���� Ư�� ����
        if (Input.GetKeyDown(KeyCode.B))
        {
            this.animator.SetTrigger("Magic_Skill");
        }

        // �׳� Ư�� ����
        if (Input.GetKeyDown(KeyCode.N))
        {
            this.animator.SetTrigger("Melee_Skill");
        }

        // ���� �ʿ�
        if (currentHealth == 0)
        {
            this.animator.SetTrigger("Death");
        }


        // �¿� �̵�
        int key = 0;
        if (Input.GetKey(KeyCode.D))
        {
            key = 1;
            transform.Translate(Vector3.right * Time.deltaTime * 5);
        }
        if (Input.GetKey(KeyCode.A))
        {
            key = -1;
            transform.Translate(Vector3.left * Time.deltaTime * 5);
        }

        // �÷��̾��� �ӵ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //// ���ǵ� ����
        //if (speedx < this.maxWalkSpeed)
        //{
        //    this.rigid2D.AddForce(transform.right * key * this.walkForce);
        //}

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