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

    Rigidbody2D rigidbody2;

    protected override void Init()
    {
        base.Init();

        rigidbody2 = GetComponent<Rigidbody2D>();

        Status.MoveSpeed = moveSpeed;
        Status.AttackSpeed = attackSpeed;
        Status.MaxHealth = maxHealth;
        Status.CurrentHealth = maxHealth;

        fsmManager.ChangeState(MonsterFSMState.Idle);
    }

    private void Update()
    {
        rigidbody2.velocity = Vector3.zero;
    }
}
