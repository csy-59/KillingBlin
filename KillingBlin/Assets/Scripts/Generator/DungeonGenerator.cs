using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.DungeonDefines;
using System.Linq;

public class DungeonGenerator : MonoSingleton<DungeonGenerator>
{
    [Serializable]
    private struct RoomRatio
    {
        [Header("RoomType")]
        [SerializeField] private int _start;
        public int Start    { get => _start; }

        [SerializeField] private int _end;
        public int End    { get => _end; }

        [SerializeField] private int _boss;
        public int Boss { get => _boss; }

        [SerializeField] private int _normal;
        public int Normal { get => _normal; }

        [SerializeField] private int _special;
        public int Special { get => _special; }

        // 기본 방일 경우의 난이도 확률
        [Header("Difficulty")]
        [SerializeField] private int _easy;
        public int Easy { get => _easy; }

        [SerializeField] private int _mid;
        public int Mid { get => _mid; }

        [SerializeField] private int _hard;
        public int Hard { get => _hard; }

        // 기타 유틸
        public int TotalRoomTypeRatio { get => Start + End + Boss + Normal + Special; }
        public int TotalNormalDifficulty { get => Easy + Mid + Hard; }

    }
    // 기본 던전 생성 시 방 생성 확률
    [SerializeField] private RoomRatio[] _roomRatio;

    // 던전 구성의 인포
    [SerializeField] private List<RoomInfo> _roomInfo = new List<RoomInfo>();

    private Queue<RoomInfo> _queue = new Queue<RoomInfo>();

    public bool GenerateDungeon()
    {
        _roomInfo.Clear();
        int ratioIndex = 0;
        int levelCount = 0;

        // BFS로 기본 던전 구성
        _queue.Clear();
        RoomInfo info = new RoomInfo();
        RoomInfo newInfo;
        RoomRatio ratio = _roomRatio[ratioIndex];
        GetRandomRoomInfo(in ratio, 0, ref info);
        _queue.Enqueue(info);
        _roomInfo.Add(info);

        do
        {
            if(levelCount <= 0)
            {
                levelCount = _queue.Count;
                ++ratioIndex;
                ratio = _roomRatio[ratioIndex];
            }

            info = _queue.Dequeue();
            --levelCount;

            bool isFoward = true;
            for (int i = 0; i < (int)DoorPosition.DP_COUNT; ++i)
            {
                byte dPos = (byte)((byte)info.DoorPos | ((byte)DoorPosition.DP_Top << (i * 2)));
                if(dPos != 0) // 해당 위치에 문이 있다면
                {
                    newInfo = new RoomInfo();
                    GetRandomRoomInfo(in ratio, (byte)(isFoward ? dPos << 2 : dPos >> 2), ref newInfo);
                    _roomInfo.Add(newInfo);
                }
                isFoward = !isFoward;
            }

        } while(_queue.Count > 0);

        return true;
    }

    private void GetRandomRoomInfo(in RoomRatio ratio, byte openDoorPos, ref RoomInfo info)
    {
        RoomType type = RoomType.RT_Start;
        int value = UnityEngine.Random.Range(0, ratio.TotalRoomTypeRatio);
        if (value < ratio.Start) { type = RoomType.RT_Start; }
        else if (value < ratio.End) { type = RoomType.RT_End; }
        else if (value < ratio.Boss) { type = RoomType.RT_Boss; }
        else if (value < ratio.Normal) { type = RoomType.RT_Normal; }
        else if (value < ratio.Special) { type = RoomType.RT_Special; }

        RoomDifficulty difficulty = RoomDifficulty.RD_MAX;
        value = UnityEngine.Random.Range(0, ratio.TotalNormalDifficulty);
        if (value < ratio.Easy) { difficulty = RoomDifficulty.RD_Easy; }
        else if (value < ratio.Mid) { difficulty = RoomDifficulty.RD_Mid; }
        else if (value < ratio.Hard) { difficulty = RoomDifficulty.RD_Hard; }

        byte dPos = 0;
        bool[] doorPos = {
            (openDoorPos | (byte)DoorPosition.DP_Top) != 0,
            (openDoorPos | (byte)DoorPosition.DP_Bottom) != 0,
            (openDoorPos | (byte)DoorPosition.DP_Left) != 0,
            (openDoorPos | (byte)DoorPosition.DP_Right) != 0
        };

        int count = doorPos.Where(_ => _ == false).Select(o => o).Count();
        value = type == RoomType.RT_End ? 0 :UnityEngine.Random.Range(0, count);
        for (int i = 0; i <= value; ++i)
        {
            int index = -1;
            do
            {
                index = UnityEngine.Random.Range(0, (int)DoorPosition.DP_COUNT);
            } while (doorPos[index] == false);
            doorPos[index] = true;

            dPos = (byte)(dPos | ((byte)DoorPosition.DP_Top << 2 * index));
        }

        info.SetRoomInfo(type, difficulty, (DoorPosition)dPos);
    }
}
