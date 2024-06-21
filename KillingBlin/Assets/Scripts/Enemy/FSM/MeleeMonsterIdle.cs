using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MeleeMonsterIdle : MonsterStateBase
{
    private Animator animator;

    private MonsterBase enemy;

    public override void Init(MonsterFSMManager manager)
    {
        base.Init(manager);
        enemy = gameObject.GetComponentInChildren<MonsterBase>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void OnEnterState()
    {
        // 애니메이션 재생
    }

    public override void OnUpdateState()
    {
        // 주인공 탐지
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }
}
