using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    /// <summary> 캐릭터 인덱스 생성을 위한 값</summary>
    private ulong characterIndex;

    /// <summary> 플레이어와 몬스터의 베이스인 캐릭터를 관리하는 오브젝트 풀 </summary>
    private CharacterPool characterPool;

    protected override void OnInit()
    {
        base.OnInit();
        characterPool = new CharacterPool(new Character(), 50);
    }

    /// <summary>
    /// 캐릭터 고유의 아이디를 생성한다.
    /// </summary>
    public ulong GenerateCharacterIndex()
    {
        return ++characterIndex;
    }
    
    public Character CreateMonster()
    {
        return characterPool.Pop();
    }
}
