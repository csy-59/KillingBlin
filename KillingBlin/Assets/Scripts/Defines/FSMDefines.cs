using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defines
{
    namespace FSMDefines
    {
        public enum MonsterFSMState
        {
            Idle,
            Chase,
            Attack,
            Stanned,
            Skill,
            Dead
        }

        public static class AnimationID
        {
            public static readonly int Idle = Animator.StringToHash("Idle");
            public static readonly int Move = Animator.StringToHash("Move");
            public static readonly int Melee_Attack = Animator.StringToHash("Melee_Attack");
            public static readonly int Magic_Attack = Animator.StringToHash("Magic_Attack");
            public static readonly int Dead = Animator.StringToHash("Dead");
            public static readonly int Stunned = Animator.StringToHash("Stun");
            public static readonly int Melee_Skill = Animator.StringToHash("Melee_Skill");
            public static readonly int Magic_Skill = Animator.StringToHash("Magic_Skill");
        }
    }
}
