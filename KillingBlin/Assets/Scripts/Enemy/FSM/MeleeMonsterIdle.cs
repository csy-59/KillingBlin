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
        // �ִϸ��̼� ���
    }

    public override void OnUpdateState()
    {
        // ���ΰ� Ž��
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }
}
