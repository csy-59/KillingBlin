using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.FSMDefines;

public class MonsterFSMManager : MonoBehaviour
{
    private Dictionary<MonsterState, MonsterStateBase> states = new Dictionary<MonsterState, MonsterStateBase>();
    private bool isThereState = false;
    private MonsterStateBase currentState;


    private void AddState(MonsterState state, MonsterStateBase Behaviour)
    {
        states[state] = Behaviour;
    }

    private void DeleteState(MonsterState state)
    {
        if (states.ContainsKey(state)) 
        { 
            states.Remove(state); 
        }
    }

    public bool ChangeState(MonsterState state)
    {
        if (isThereState)
            currentState.OnExitState();

        if(states.TryGetValue(state, out currentState))
        {
            isThereState = true;
            currentState.OnEnterState();
        }
        else 
            isThereState= false;

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
}
