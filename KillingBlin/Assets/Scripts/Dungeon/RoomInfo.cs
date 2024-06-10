using Defines.DungeonDefines;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// �濡 ���� ����
/// �� ���尡 ������ �ٷ� �ı� �Ǳ� ������ �޸� ����ȭ ������ struct�� ����
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
    /// ���� ������ ������� ���� ���� ������
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
    /// ���� ���� ����
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
    /// ���� �ش��ϴ� ���� �� ����
    /// </summary>
    public bool AddLeafRoom(DoorPosition door, in RoomBase leafRoom)
    {
        // �ش� �ϴ� �� ����
        if (((int)Room.DoorPos & (int)door) <= 0)
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