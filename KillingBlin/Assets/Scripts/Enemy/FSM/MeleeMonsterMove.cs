using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterMove : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;
    private Rigidbody2D rb;


    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        enemy = gameObject.GetComponentInChildren<MonsterBase>();
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
    }
    public override void OnEnterState()
    {
        animator.SetTrigger(AnimationID.Move);
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
    }

    public override void OnFixedUpdateState()
    {
        if ((enemy.Target.transform.position - transform.position).magnitude <= enemy.AttackRadios)
        {
            manager.ChangeState(MonsterFSMState.Attack);
        }

        rb.MovePosition(transform.position + (enemy.Target.transform.position - transform.position).normalized * Time.deltaTime * enemy.Status.MoveSpeed);
    }
}
