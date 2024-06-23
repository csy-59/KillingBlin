using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Lumin;

public class BossMeleeMonsterIdle : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;
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
        // 애니메이션 재생
        animator.SetTrigger(AnimationID.Idle);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
        {
            return;
        }

        (enemy as MeleeMonster).Target = collision.gameObject;
        manager.ChangeState(MonsterFSMState.Chase);
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
    }
}
