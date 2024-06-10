using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;

public class RoomBase : MonoBehaviour
{
    // 룸 정보 관련
    [Header("Room Info")]
    [SerializeField] private RoomType _roomType = RoomType.RT_Normal;
    public RoomType RoomType { get => _roomType; }

    [SerializeField] private RoomDifficulty _difficulty = RoomDifficulty.RD_Mix_Easy;
    public RoomDifficulty Difficulty { get => _difficulty; }
    [SerializeField] private byte _roomDoor;
    public byte DoorPos
    {
        get => _roomDoor;
    }

    public bool isDoorPositionMatch(DoorPosition position)
    {
        return (DoorPos & (int)position) == (int)position;
    }

    [Header("Inner Info")]
    [SerializeField] private Transform _healPackPosition;
    public Transform HealPackPosition { get => _healPackPosition; }
    [SerializeField] private EnemyBase[] _enemyList;
    public EnemyBase[] EnemyList { get { return _enemyList; } }

    /// <summary>
    /// 방에 다시 진입하였을 때 초기화 진행
    /// </summary>
    public void Restart()
    {

    }

    public void SetRoomDoor(byte doorPos)
    {
        _roomDoor = doorPos;
    }
}
