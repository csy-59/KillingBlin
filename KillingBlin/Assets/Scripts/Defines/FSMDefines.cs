using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defines
{
    namespace FSMDefines
    {
        public enum MonsterState
        {
            Idle,
            Chase,
            Attack,
            Damaged,
            Stanned,
            Skill,
            Dead
        }

        public static class AnimationID
        {
            public static readonly int Idle = Animator.StringToHash("Idle");
            public static readonly int Move = Animator.StringToHash("Move");
            public static readonly int Melee_Attack = Animator.StringToHash("Attack_Melee");
            public static readonly int Magic_Attack = Animator.StringToHash("Attack_Magic");
            public static readonly int Dead = Animator.StringToHash("Dead");
            public static readonly int Stunned = Animator.StringToHash("Stun");
        }
    }
}