using Defines.DungeonDefines;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// 방에 대한 정보
/// 한 라운드가 끝나면 바로 파괴 되기 때문에 메모리 단편화 이유로 struct로 세팅
/// </summary>
public struct RoomInfo
{
    public RoomType Type { get; set; }
    public RoomDifficulty Difficulty { get; set; }
    public byte DoorPos { get; set; }


    private RoomBase _room;
    public RoomBase Room { get => _room; private set => _room = value; }

    private Dictionary<DoorPosition, RoomBase> _leafRoom;

    public RoomInfo(RoomType type = RoomType.RT_Start, RoomDifficulty difficulty = RoomDifficulty.RD_MAX, byte doorPosition = (byte)DoorPosition.DP_COUNT)
    {
        Type = type;
        Difficulty = difficulty;
        DoorPos = doorPosition;
        _room = null;
        _leafRoom = new Dictionary<DoorPosition, RoomBase>();
    }

    /// <summary>
    /// 방의 정보를 기반으로 랜덤 방을 세팅함
    /// </summary>
    public bool SetRoom(Vector3 pos)
    {
        if (RoomManager.Instance.GetRandomRoom(in this, out _room) == false)
        {
            Debug.LogError("No Matching Room");
            return false;
        }

        _room = GameObject.Instantiate(_room);
        _room.SetRoomDoor(DoorPos);
        _room.transform.localPosition = pos;
        _room.gameObject.SetActive(false);
        _room.gameObject.SetActive(true);

        return true;
    }

    /// <summary>
    /// 방의 정보 세팅
    /// </summary>
    public void SetRoomInfo(RoomType type, RoomDifficulty difficulty, byte doorPosition)
    {
        Type = type;
        Difficulty = difficulty;
        DoorPos = doorPosition;
        _room = null;
        _leafRoom.Clear();
    }

    /// <summary>
    /// 문에 해당하는 리프 문 세팅
    /// </summary>
    public bool AddLeafRoom(DoorPosition door, in RoomBase leafRoom)
    {
        // 해당 하는 문 없음
        if (((int)Room.DoorPos & (int)door) <= 0)
            return false;

        // 이미 해당 하는 문에 연결 됨
        if (_leafRoom.ContainsKey(door))
            return false;

        _leafRoom[door] = leafRoom;

        return true;
    }

    /// <summary>
    /// 방 문에 해당하는 리프 문 삭제
    /// </summary>
    public void DeleteLeafRoom(DoorPosition door)
    {
        _leafRoom.Remove(door);
    }
}