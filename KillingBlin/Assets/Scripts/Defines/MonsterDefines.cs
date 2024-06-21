
using UnityEngine;

namespace Defines
{
    namespace MonsterDefines
    {
        public enum MonsterAttactType : byte
        {
            // ���� ���� ����
            Melee = 0b000,
            Magic = 0b001
        }

        public enum MonsterDifficulty : byte
        { 
            // ���� ���̵�
            Easy = 0b000,
            Hard = 0b010,
        }

    }
}