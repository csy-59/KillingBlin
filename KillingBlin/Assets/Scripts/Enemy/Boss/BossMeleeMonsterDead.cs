using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;

public class BossMeleeMonsterDead : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;
    private CircleCollider2D collider;

    private float elapsedTime = 0f;
    private float DieTime = 2f;

    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        enemy = gameObject.GetComponentInChildren<MonsterBase>();
        animator = gameObject.GetComponentInChildren<Animator>();
        collider = gameObject.GetComponentInChildren<CircleCollider2D>();
    }

    public override void OnEnterState()
    {
        animator.SetTrigger(AnimationID.Dead);
        collider.enabled = false;
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > DieTime)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
