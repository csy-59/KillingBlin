using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 플레이어와 몬스터의 베이스가 되는 클래스
/// </summary>
public class Character : IPoolable
{
    /// <summary> 캐릭터가 가지고 있는 고유한 인덱스 </summary>
    public ulong Index { get; private set; }
    
    /// <summary> 스테이터스 상태 </summary>
    public Status Status { get; private set; }
    public bool InPool { get; set; }
    
    private Action<IPoolable> cbReturnToPool;

    /// <summary>
    /// 피해를 입는다.
    /// </summary>
    private void TakeDamage(int attack)
    {
        Status.Damage(attack);
    }
    public IPoolable Clone(Action<IPoolable> returnToPool)
    {
        var newCharacter = new Character();
        newCharacter.cbReturnToPool = returnToPool;
        newCharacter.Status = new Status();
        newCharacter.Index = BattleManager.Instance.GenerateCharacterIndex();
        return newCharacter;
    }

    public void Sleep()
    {
        
    }

    public void Wakeup()
    {
        
    }

    public void ReturnPool()
    {
        cbReturnToPool(this);
    }
}
