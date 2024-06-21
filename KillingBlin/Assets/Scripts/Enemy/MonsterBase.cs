using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.MonsterDefines;
using Defines.FSMDefines;


public class MonsterBase : MonoBehaviour
{
    [Serializable]
    protected class MonsterStateAndScripts
    {
        [SerializeField] public MonsterFSMState state;
        [SerializeField] public MonsterStateBase script;
    }
    
    [SerializeField] protected MonsterFSMManager fsmManager;
    [SerializeField] protected List<MonsterStateAndScripts> states = new List<MonsterStateAndScripts>();

    [SerializeField] private MonsterAttactType attack;
    [SerializeField] private MonsterDifficulty difficulty;
    [SerializeField] private bool isBoss;
    public byte Type
    {
        get
        {
            byte type = 0;
            type |= (byte)attack;
            type |= (byte)difficulty;
            if (isBoss) type |= (byte)MonsterType.Boss;
            return type;
        }
    }

    [SerializeField] private float attackRadios;
    public float AttackRadios { get => attackRadios; protected set => attackRadios = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; protected set => moveSpeed = value; }

    [SerializeField] private float attackSpeed;
    public float AttackSpeed { get => attackSpeed; protected set => attackSpeed = value; }

    [SerializeField] private Vector2 attackPosition;
    public Vector2 AttackPosition { get => attackPosition; protected set => attackPosition = value; }

    public GameObject Target { get; set; }



    public virtual void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        foreach (var state in states)
        {
            state.script.SetMonsterBase(this);
            fsmManager.AddState(state.state, state.script);
        }
    }
}
