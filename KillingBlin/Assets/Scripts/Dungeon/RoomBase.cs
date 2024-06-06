using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;

public class RoomBase : MonoBehaviour
{
    // �� ���� ����
    [Header("Room Info")]
    [SerializeField] private RoomType _roomType = RoomType.RT_Normal;
    public RoomType RoomType { get => _roomType; }

    [SerializeField] private RoomDifficulty _difficulty = RoomDifficulty.RD_Mix_Easy;
    public RoomDifficulty Difficulty { get => _difficulty; }
    [SerializeField] private DoorPosition[] _roomDoor;
    public DoorPosition DoorPosition
    {
        get
        {
            int roomPosition = 0;
            foreach(var position in _roomDoor)
            {
                roomPosition |= (int)position;
            }
            return (DoorPosition) roomPosition;
        }
    }

    public bool isDoorPositionMatch(DoorPosition position)
    {
        return ((int)DoorPosition & (int)position) == (int)position;
    }

    [Header("Inner Info")]
    [SerializeField] private Transform _healPackPosition;
    public Transform HealPackPosition { get => _healPackPosition; }
    [SerializeField] private EnemyBase[] _enemyList;
    public EnemyBase[] EnemyList { get { return _enemyList; } }

    /// <summary>
    /// �濡 �ٽ� �����Ͽ��� �� �ʱ�ȭ ����
    /// </summary>
    public void Restart()
    {

    }
}
