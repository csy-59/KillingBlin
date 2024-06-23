using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    /// <summary> 공격력 </summary>
    public int Attack { get; set; }
    
    /// <summary> 최대 체력 </summary>
    public int MaxHealth { get; set; }

    /// <summary> 현재 체력 </summary>
    public int CurrentHealth { get; set; }
    
    /// <summary> 공격속도. 공격하는데 드는 속도. </summary>
    public float AttackSpeed { get; set; }
    
    /// <summary> 이동 속도. 1초간 움직일 속도. </summary>
    public float MoveSpeed { get; set; }

    /// <summary> 죽었는지 여부 </summary>
    public bool IsDead => CurrentHealth <= 0;
    
    /// <summary>
    /// 체력을 회복한다.
    /// </summary>
    public void AddHealth(int health)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + health, MaxHealth);
    }

    /// <summary>
    /// 체력을 깎는다.
    /// </summary>
    public void Damage(int attack)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - attack, 0);
    }
}
