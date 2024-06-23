using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeMonster : MeleeMonster
{
    [SerializeField] private float health;
    public float Health { get => health; set => health = value; }

    [SerializeField] private float move;
    public float Move { get => move; set => move = value; }

    [SerializeField] private float damage;
    public float Damage { get  => damage; set => damage = value; }

    protected override void Init()
    {
        base.Init();
    }
}
