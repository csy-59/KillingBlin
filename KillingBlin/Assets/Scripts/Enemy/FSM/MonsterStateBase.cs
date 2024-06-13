using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;

public abstract class MonsterStateBase : MonoBehaviour
{
    public MonsterState State { get; set; }
    private MonsterFSMManager manager;

    public void SetFSMManager(MonsterFSMManager manager)
    {
        this.manager = manager;
    }

    public abstract void OnEnterState();
    public virtual void OnFixedUpdateState() { }
    public abstract void OnUpdateState();
    public abstract void OnExitState();
}
