using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;

public abstract class MonsterStateBase : MonoBehaviour
{
    public Defines.FSMDefines.MonsterFSMState State { get; set; }
    protected MonsterFSMManager manager;
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
}
