using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.FSMDefines;

public class MonsterFSMManager : MonoBehaviour
{
    private Dictionary<Defines.FSMDefines.MonsterState, MonsterStateBase> states = new Dictionary<Defines.FSMDefines.MonsterState, MonsterStateBase>();
    private bool isThereState = false;
    private MonsterStateBase currentState;
    
    public GameObject GetMyGameObject()
    {
        return gameObject;
    }

    private void AddState(Defines.FSMDefines.MonsterState state, MonsterStateBase Behaviour)
    {
        states[state] = Behaviour;
        Behaviour.Init(this);
    }

    private void DeleteState(Defines.FSMDefines.MonsterState state)
    {
        if (states.ContainsKey(state)) 
        { 
            states.Remove(state); 
        }
    }

    public bool ChangeState(Defines.FSMDefines.MonsterState state)
    {
        if (isThereState)
        {
            currentState.OnExitState();
            currentState.gameObject.SetActive(false);
        }

        if(states.TryGetValue(state, out currentState))
        {
            isThereState = true;
            currentState.gameObject.SetActive(true);
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
