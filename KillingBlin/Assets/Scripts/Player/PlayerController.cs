using System;
using System.Collections;
using System.Collections.Generic;
using Defines.FSMDefines;
using UnityEngine;

public class PlayerController : Character
{
    private Character character;
    private Rigidbody2D rigidbody;
    private Animator animator;

    private bool isMove;

    private Vector2 moveDir;

    private void Start()
    {
        character = GetComponentInChildren<Character>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        character.Status.MoveSpeed = 5f;

        isMove = false;
    }

    private void FixedUpdate()
    {
        if (isMove == false) return;

        rigidbody.MovePosition(rigidbody.position + moveDir * character.Status.MoveSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveForce = Mathf.Abs(h) + Mathf.Abs(v);

        if (isMove)
        {
            if (moveForce == 0.0f)
            {
                isMove = false;
                animator.SetTrigger(AnimationID.Idle);
            }
            else
            {
                if (h < 0 && transform.eulerAngles.y == 180f)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                if (h > 0 && transform.eulerAngles.y == 0f)
                {
                    transform.eulerAngles = new Vector3(0f, 180f, 0f);
                }

                moveDir = new Vector2(h, v);
                if (moveForce >= 1.4f)
                {
                    moveDir.Normalize();
                }
            }
        }

        if (!isMove && moveForce > 0f)
        {
            isMove = true;
            animator.SetTrigger(AnimationID.Move);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.animator.SetTrigger("isBowAttacking");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            this.animator.SetTrigger("Magic_Attack");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.animator.SetTrigger("Melee_Attack");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            this.animator.SetTrigger("Bow_Skill");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            this.animator.SetTrigger("Magic_Skill");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            this.animator.SetTrigger("Melee_Skill");
        }


        void OnTriggerEnter2D(Collider other)
        {
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            collision.gameObject.GetComponent<Door>().OnMoveToNextDoor(this);
        }
    }
}
