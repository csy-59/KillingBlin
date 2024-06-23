using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Defines.FSMDefines;
using UnityEditor.PackageManager;

public class MeleeMonster : MonsterBase
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int maxHealth;

    protected override void Init()
    {
        base.Init();


        Status.MoveSpeed = moveSpeed;
        Status.AttackSpeed = attackSpeed;
        Status.MaxHealth = maxHealth;
        Status.CurrentHealth = maxHealth;

        fsmManager.ChangeState(MonsterFSMState.Idle);
    }
}
