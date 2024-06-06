using Defines.DungeonDefines;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Defines.DungeonDefines;

/// <summary>
/// �濡 ���� ����
/// �� ���尡 ������ �ٷ� �ı� �Ǳ� ������ �޸� ����ȭ ������ struct�� ����
/// </summary>
public struct RoomInfo
{
    public RoomType Type { get; set; }
    public RoomDifficulty Difficulty { get; set; }
    public DoorPosition DoorPos { get; set; }


    private RoomBase _room;
    public RoomBase Room { get => _room; private set => _room = value; }

    private Dictionary<DoorPosition, RoomBase> _leafRoom;

    public RoomInfo(RoomType type = RoomType.RT_Start, RoomDifficulty difficulty = RoomDifficulty.RD_MAX, DoorPosition doorPosition = DoorPosition.DP_COUNT)
    {
        Type = type;
        Difficulty = difficulty;
        DoorPos = doorPosition;
        _room = null;
        _leafRoom = new Dictionary<DoorPosition, RoomBase>();
    }

    /// <summary>
    /// ���� ������ ������� ���� ���� ������
    /// </summary>
    public bool SetRoom()
    {
        if (RoomManager.Instance.GetRandomRoom(in this, out _room) == false)
        {
            Debug.LogError("No Matching Room");
            return false;
        }

        return true;
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void SetRoomInfo(RoomType type, RoomDifficulty difficulty, DoorPosition doorPosition)
    {
        Type = type;
        Difficulty = difficulty;
        DoorPos = doorPosition;
        _room = null;
        _leafRoom.Clear();
    }

    /// <summary>
    /// ���� �ش��ϴ� ���� �� ����
    /// </summary>
    public bool AddLeafRoom(DoorPosition door, in RoomBase leafRoom)
    {
        // �ش� �ϴ� �� ����
        if (((int)Room.DoorPosition & (int)door) <= 0)
            return false;

        // �̹� �ش� �ϴ� ���� ���� ��
        if (_leafRoom.ContainsKey(door))
            return false;

        _leafRoom[door] = leafRoom;

        return true;
    }

    /// <summary>
    /// �� ���� �ش��ϴ� ���� �� ����
    /// </summary>
    public void DeleteLeafRoom(DoorPosition door)
    {
        _leafRoom.Remove(door);
    }
}