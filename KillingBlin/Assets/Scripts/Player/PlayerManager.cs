using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public int Exp { get; private set; }

    public const int MaxExp = 100;
    
    public int CurrentMaxExp => Level * MaxExp;
    public int Level { get; private set; }

    public System.Action OnLevelUp { get; set; } 

    public void Init()
    {
        Level = 1;
        Exp = 0;
    }
    
    public void AddExp(int exp)
    {
        Exp += exp;
        if (Exp >= CurrentMaxExp)
        {
            Exp -= CurrentMaxExp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        OnLevelUp?.Invoke();
        Level += 1;
    }
}
