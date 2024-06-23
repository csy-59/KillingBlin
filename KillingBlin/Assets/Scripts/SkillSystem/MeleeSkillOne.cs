using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.FSMDefines;

public class MeleeSkillOne : SkillBase
{
    Animator animator;

    public override void Init()
    {
        animator = GetComponent<Animator>();
    }

    public override void Skill()
    {
        animator.SetTrigger(AnimationID.Melee_Attack);
    }


}
