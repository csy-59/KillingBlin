using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;
using System.Runtime.InteropServices.WindowsRuntime;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private GameObject[] topDoor = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private GameObject[] bottomDoor = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private GameObject[] leftDoor = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private GameObject[] rightDoor = new GameObject[(int)Difficulty.Boss + 1];

    private Room[] nextRooms = new Room[(int)DoorPosition.MAX];

    [Header("RoomType")]
    [SerializeField] private GameObject[] maps = new GameObject[(int)RoomType.MAX];
    private RoomType roomType = RoomType.Normal;
    public RoomType RoomType
    {
        get => roomType;
        set
        {
            roomType = value;

            foreach (var map in maps)
                map.SetActive(false);

            if (roomType == RoomType.MAX)
                maps[0].SetActive(true);
            else
                maps[(int)roomType].SetActive(true);
        }
    }

    [Header("Difficulty")]
    [SerializeField] private GameObject[] walls = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private Difficulty difficulty = Difficulty.None;
    public Difficulty Difficulty
    {
        get => difficulty;
        set
        {
            difficulty = value;

            foreach (var wall in walls)
                wall.SetActive(false);

            walls[(int)difficulty].SetActive(true);
        }
    }

    public Vector2Int RoomIndex { get; set; }


    public void OpenDoor(DoorPosition direction, Room nextRoom)
    {
        GameObject door = topDoor[(int)difficulty];
        int nextRoomIndex = 0;
        switch (direction)
        {
            case DoorPosition.Top: { door = topDoor[(int)difficulty]; nextRoomIndex = (int)DoorPosition.Top; break; }
            case DoorPosition.Bottom: { door = bottomDoor[(int)difficulty]; nextRoomIndex = (int)DoorPosition.Bottom; break; }
            case DoorPosition.Left: { door = leftDoor[(int)difficulty]; nextRoomIndex = (int)DoorPosition.Left; break; }
            case DoorPosition.Right: { door = rightDoor[(int)difficulty]; nextRoomIndex = (int)DoorPosition.Right; break; }
        }

        door.SetActive(true);
        nextRooms[nextRoomIndex] = nextRoom;
    }

    public bool TryGetNextRoom(DoorPosition direction, ref Room nextRoom)
    {
        switch (direction)
        {
            case DoorPosition.Top:
            case DoorPosition.Bottom:
            case DoorPosition.Left:
            case DoorPosition.Right:
                nextRoom = nextRooms[(int)direction]; break;
            default: nextRoom = null; break;
        }

        return (nextRoom == null ? false : true);
    }

    public void SetRoom()
    {
        GameObject map = MapManager.Instance.GetRandomMap(RoomType, Difficulty);

        GameObject go = Instantiate(map);
        go.transform.position = Vector3.zero;
        go.GetComponent<Map>().SetRoom(this);
    }
}
