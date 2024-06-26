using System;
using System.Collections;
using System.Collections.Generic;
using Defines.FSMDefines;
using UnityEditor.Search;
using UnityEngine;

public class PlayerController : Character
{
    private Character character;
    private PlayerSkill skill;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private PlayerAnimEventDispatcher dispatcher;


    private bool isMove;

    private Vector2 moveDir;

    private bool isWaitAttack;
    private float elapsedTime;

    LayerMask enemeyLayer;

    public enum PlayerAttackType
    {
        Normal,
        SkillOne,
        SkillSec,
        SkillThird,
        SkillForth,
    }
    private PlayerAttackType attackType;

    private void Start()
    {
        character = GetComponentInChildren<Character>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        dispatcher = GetComponentInChildren<PlayerAnimEventDispatcher>();
        skill = GetComponentInChildren<PlayerSkill>();

        enemeyLayer = LayerMask.GetMask("Enemy");

        character.Status.Attack = 3;
        character.Status.AttackSpeed = 1.0f;
        character.Status.MoveSpeed = 5f;

        isMove = false;
        isWaitAttack = false;
        elapsedTime = 0.0f;

        dispatcher.OnAttackCallback = OnAttack;
        dispatcher.OnAttackEndCallback = OnAttackEnd;
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
                if (h < 0)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                if (h > 0)
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

        if (isWaitAttack)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= character.Status.AttackSpeed)
            {
                elapsedTime = 0.0f;
                isWaitAttack = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isWaitAttack == false)
            {
                isWaitAttack = true;
                attackType = PlayerAttackType.Normal;
                animator.SetTrigger(AnimationID.Melee_Attack);
            }
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            skill.SkillUse(PlayerSkill.SkillType.Q);
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            skill.SkillUse(PlayerSkill.SkillType.E);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            skill.SkillUse(PlayerSkill.SkillType.R);
        }

        rigidbody.velocity = Vector3.zero;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            collision.gameObject.GetComponent<Door>().OnMoveToNextDoor(this);
        }
    }


    public void OnAttack()
    {
        switch (attackType)
        {
            case PlayerAttackType.Normal:
                {
                    var collider = Physics2D.OverlapCircleAll(transform.position, 1f, enemeyLayer);
                    if (collider != null && collider.Length > 0)
                    {
                        foreach (var col in collider)
                        {
                            MonsterBase mb = col.GetComponent<MonsterBase>();
                            mb?.TakeDamage(character.Status.Attack);
                        }
                    }
                }
                break;
        }
    }

    public void OnAttackEnd()
    {
        switch (attackType)
        {
            case PlayerAttackType.Normal:
                break;
        }
    }
}
