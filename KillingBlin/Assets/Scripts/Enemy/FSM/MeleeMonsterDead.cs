using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;

public class MeleeMonsterDead : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;
    private CircleCollider2D collider;

    private float elapsedTime = 0f;
    private float DieTime = 1f;

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
        elapsedTime = 0f;
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= DieTime)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
