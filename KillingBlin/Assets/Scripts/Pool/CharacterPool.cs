using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : ObjectPool<Character>
{
    public CharacterPool(IPoolable originalObject, int capacity, int initCount = 0) : base(originalObject, capacity, initCount)
    {
        
    }

    /// <summary>
    /// 해당 아이디를 가진 캐릭터가 살아있는지 여부
    /// </summary>
    public bool IsAlive(ulong index)
    {
        foreach (var character in objectOutPool)
        {
            if (character.Index == index) return true;
        }

        return false;
    }
    
    /// <summary>
    /// 해당 아이디를 가진 살아있는 캐릭터를 반환받는다.
    /// 죽어있거나 존재하지 않을 경우 false를 반환한다.
    /// </summary>
    public bool TryGetAliveCharacter(ulong index, out Character character)
    {
        foreach (var ch in objectOutPool)
        {
            if (ch.Index == index)
            {
                character = ch;
                return true;
            }
        }

        character = null;
        return false;
    }
}
