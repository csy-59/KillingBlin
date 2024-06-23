using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;
using System;

public abstract class BossMonsterStateBase : MonoBehaviour
{
    public Defines.FSMDefines.MonsterFSMState State { get; set; }
    protected BossMeleeMonsterFSMManager manager;
    protected MonsterBase monster;

    public void SetFSMManager(MonsterFSMManager manager)
    {
        this.manager = manager;
    }

    public virtual void Init(MonsterFSMManager manager)
    {
        SetFSMManager(manager);
    }

    public abstract void OnEnterState();
    public virtual void OnFixedUpdateState() { }
    public abstract void OnUpdateState();
    public abstract void OnExitState();

    public void SetMonsterBase(MonsterBase monster)
    {
        this.monster = monster;
    }

    public static implicit operator BossMonsterStateBase(MonsterStateBase v)
    {
        throw new NotImplementedException();
    }
}
