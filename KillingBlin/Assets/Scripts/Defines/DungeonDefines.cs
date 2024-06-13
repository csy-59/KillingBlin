using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defines
{
    namespace DungeonDefines
    {
        public enum RoomType
        {
            Start,
            Boss,
            Normal,
            Special
        }

        public enum Difficulty
        {
            None,
            Easy,
            Mid_1,
            Mid_2,
            Hard,
            Boss
        }

        public enum DoorPosition : byte
        {
            Top,
            Bottom,
            Left,
            Right,
            MAX
        }
    }
}
