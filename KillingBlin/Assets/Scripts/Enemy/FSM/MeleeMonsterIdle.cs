using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Lumin;

public class MeleeMonsterIdle : MonsterStateBase
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

        manager.ChangeState(MonsterFSMState.Chase);
        (enemy as MeleeMonster).Target = collision.gameObject;
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
    }
}
