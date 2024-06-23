using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;
using System;

public class MeleeMonsterStunned : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;
    private float elapsedTime = 0.0f;


    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        enemy = gameObject.GetComponentInChildren<MonsterBase>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }
    public override void OnEnterState()
    {
        animator.SetTrigger(AnimationID.Stunned);
        elapsedTime = 0.0f;
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < enemy.StunnedTime)
        {
            manager.ChangeState(manager.PrevState);
        }
    }
}
