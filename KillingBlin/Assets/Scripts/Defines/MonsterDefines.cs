
using UnityEngine;

namespace Defines
{
    namespace MonsterDefines
    {
        public enum MonsterAttactType : byte
        {
            // 몬스터 공격 유형
            Melee = 0b000,
            Magic = 0b001
        }

        public enum MonsterDifficulty : byte
        { 
            // 몬스터 난이도
            Easy = 0b000,
            Hard = 0b010,
        }

        public enum MonsterType : byte
        {
            Normal = 0b000,
            Boss = 0b100,
        }

    }
}
