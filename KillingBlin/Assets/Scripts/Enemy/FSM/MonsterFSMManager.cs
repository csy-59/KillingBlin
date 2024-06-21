using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.FSMDefines;

public class MonsterFSMManager : MonoBehaviour
{
    private Dictionary<Defines.FSMDefines.MonsterFSMState, MonsterStateBase> states = new Dictionary<Defines.FSMDefines.MonsterFSMState, MonsterStateBase>();
    private bool isThereState = false;
    private MonsterStateBase currentState;
    [SerializeField] MonsterFSMState state;
    
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
        }

        if(states.TryGetValue(state, out currentState))
        {
            isThereState = true;
            currentState.OnEnterState();
            this.state = state;
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
