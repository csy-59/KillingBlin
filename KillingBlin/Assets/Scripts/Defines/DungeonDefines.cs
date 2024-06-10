using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defines
{
    namespace DungeonDefines
    {
        public enum RoomType
        {
            RT_Start,
            RT_Boss,
            RT_Normal,
            RT_Special,
            RT_None
        }

        public enum RoomDifficulty
        {
            RD_Easy,
            RD_Mid,
            RD_Hard,

            RD_Mix_Easy,
            RD_Mix_Mid,
            RD_Mix_Hard,

            RD_Melee_Easy,
            RD_Melee_Mid,
            RD_Melee_Hard,

            RD_Long_Easy,
            RD_Long_Mid,
            RD_Long_Hard,

            RD_MAX
        }

        public enum DoorPosition : byte
        {
            DP_Top1 =       0b00000001,
            DP_Top2 =       0b00000010,
            DP_Bottom1 =    0b00000100,
            DP_Bottom2 =    0b00001000,
            DP_Left1 =      0b00010000,
            DP_Left2 =      0b00100000,
            DP_Rigth1 =     0b01000000,
            DP_Rigth2 =     0b10000000,

            DP_Top =        0b00000011,
            DP_Bottom =     0b00001100,
            DP_Left =       0b00110000,
            DP_Right =      0b11000000,

            DP_COUNT = 4
        }
    }

}