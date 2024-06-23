using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.FSMDefines;


public class BossMeleeMonsterFSMManager : MonoBehaviour
{
    private Dictionary<Defines.FSMDefines.MonsterFSMState, BossMonsterStateBase> states = new Dictionary<Defines.FSMDefines.MonsterFSMState, BossMonsterStateBase>();
    private bool isThereState = false;
    private BossMonsterStateBase currentState;
    [SerializeField] MonsterFSMState _currentState;
    private MonsterFSMState prevState;
    public MonsterFSMState PrevState { get; private set; }

    public void Awake()
    {
        isThereState = false;
    }

    public GameObject GetMyGameObject()
    {
        return gameObject;
    }

    public void AddState(Defines.FSMDefines.MonsterFSMState state, MonsterStateBase Behaviour)
    {
        states[state] = Behaviour;
        Behaviour.Init(this);
    }

    public void DeleteState(Defines.FSMDefines.MonsterFSMState state)
    {
        if (states.ContainsKey(state))
        {
            states.Remove(state);
        }
    }

    public bool ChangeState(Defines.FSMDefines.MonsterFSMState state)
    {
        if (isThereState)
        {
            currentState.OnExitState();
            prevState = _currentState;
        }

        if (states.TryGetValue(state, out currentState))
        {
            currentState.OnEnterState();
            this._currentState = state;
            isThereState = true;
        }
        else
            isThereState = false;

        return isThereState;
    }

    public void FixedUpdate()
    {
        if (isThereState)
            currentState.OnFixedUpdateState();
    }

    public void Update()
    {
        if (isThereState)
            currentState.OnUpdateState();
    }

    public static implicit operator BossMeleeMonsterFSMManager(MonsterFSMManager v)
    {
        throw new NotImplementedException();
    }
}
