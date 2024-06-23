using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterAttack : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;

    private float elapsedTime = 0.0f;
    private int playerLayer;
    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        playerLayer = LayerMask.GetMask("Player");
        enemy = gameObject.GetComponentInChildren<MonsterBase>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void OnEnterState()
    {
        animator.SetTrigger(AnimationID.Melee_Attack);
        elapsedTime = 0.0f;
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= enemy.Status.AttackSpeed)
        {
            animator.SetTrigger(AnimationID.Melee_Attack);
            elapsedTime -= enemy.Status.AttackSpeed;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == enemy.Target)
        {
            manager.ChangeState(MonsterFSMState.Chase);
        }

    }

    public void OnAttackAnimationEnd()
    {
        animator.SetTrigger(AnimationID.Idle);
    }

    public void OnAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(enemy.AttackPosition, enemy.AttackRadios, playerLayer);
        if(collider != null)
        {
            collider.gameObject.GetComponent<PlayerController>().TakeDamage(enemy.Status.Attack);
        }
    }
}
