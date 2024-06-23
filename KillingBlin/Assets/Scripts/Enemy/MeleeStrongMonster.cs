using Defines.FSMDefines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStrongMonster : MeleeMonster
{
    [SerializeField] private float skillTime;
    public float SkillTime { get=>skillTime; set => skillTime = value; }

    protected override void Init()
    {
        base.Init();
    }
}
