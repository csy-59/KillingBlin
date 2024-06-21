using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonserAttack : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;

    private float elapsedTime = 0.0f;
    private int playerLayer;
    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        playerLayer = LayerMask.NameToLayer("Player");
        enemy = gameObject.GetComponentInChildren<MonsterBase>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void OnEnterState()
    {
        animator.SetTrigger(AnimationID.Melee_Attack);
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= enemy.AttackSpeed)
        {
            animator.SetTrigger(AnimationID.Melee_Attack);
            elapsedTime -= enemy.AttackSpeed;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        manager.ChangeState(MonsterFSMState.Chase);
    }

    public void OnAttackAnimationEnd()
    {
        animator.SetTrigger(AnimationID.Idle);
    }

    public void OnAttack()
    {
        Physics2D.OverlapCircle(enemy.AttackPosition, 0.5f, playerLayer);
    }
}
