using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 플레이어와 몬스터의 베이스가 되는 클래스
/// </summary>
public class Character : MonoBehaviour 
{
    /// <summary> 스테이터스 상태 </summary>
    public Status Status { get; protected set; } = new Status();
    public bool InPool { get; set; }
    
    private Action<IPoolable> cbReturnToPool;

    /// <summary>
    /// 피해를 입는다.
    /// </summary>
    public virtual void TakeDamage(int attack)
    {
        Status.Damage(attack);
    }
}
