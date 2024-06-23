using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStrongMonsterAttack : MonsterStateBase
{
    private Animator animator;
    private Rigidbody2D rb;

    private MeleeStrongMonster enemy;

    private Vector2 targetPosition;
 
    private float elapsedTime = 0.0f;
    private int playerLayer;
    private bool isCollied = false;
    private bool isAttackEnd = true;

    private Coroutine skill;
    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        playerLayer = LayerMask.NameToLayer("Player");
        enemy = gameObject.GetComponentInChildren<MeleeStrongMonster>();
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
    }

    public override void OnEnterState()
    {
        animator.SetTrigger(AnimationID.Melee_Skill);
        elapsedTime = 0.0f;
        isCollied = false;
        isAttackEnd = false;
    }

    public override void OnExitState()
    {
        animator.speed = 1.0f;
        StopAllCoroutines();
    }

    public override void OnUpdateState()
    {
        if (isAttackEnd)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= enemy.Status.AttackSpeed)
            {
                animator.SetTrigger(AnimationID.Melee_Skill);
                isAttackEnd = false;
                elapsedTime = 0.0f;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(enemy.Target != null && collision.gameObject == enemy.Target)
            manager.ChangeState(MonsterFSMState.Chase);
    }

    public void OnAttackAnimationEnd()
    {
        animator.SetTrigger(AnimationID.Move);
        animator.speed = 2f;
        skill = StartCoroutine(UseSkill());
    }

    IEnumerator UseSkill()
    {
        float skillTime = 0.0f;
        Vector3 dir = enemy.Target.transform.position - transform.position;

        while(skillTime < enemy.SkillTime)
        {
            skillTime += Time.deltaTime;
            rb.MovePosition(transform.position + dir.normalized * Time.deltaTime * enemy.Status.MoveSpeed * 2f);
            yield return null;
        }

        animator.SetTrigger(AnimationID.Idle);
        isAttackEnd = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.AddForce(Vector3.down);
        if(collision.gameObject.layer != playerLayer)
            manager.ChangeState(MonsterFSMState.Stanned);
        else
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(enemy.Status.Attack);
        }
    }
}
