using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private GameObject[] topDoor = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private GameObject[] bottomDoor = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private GameObject[] leftDoor = new GameObject[(int)Difficulty.Boss + 1];
    [SerializeField] private GameObject[] rightDoor = new GameObject[(int)Difficulty.Boss + 1];

    [Header("SpwanPosition")]
    [SerializeField] private GameObject[] SpawnPosition = new GameObject[(int)DoorPosition.MAX];
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

    [Header("Map")]
    [SerializeField] private Transform mapPosition;
    private Map map;


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
        go.transform.parent = mapPosition;
        this.map = go.GetComponent<Map>();
        go.transform.localPosition = Vector3.zero;
        go.GetComponent<Map>().SetRoom(this);
    }

    public void MoveToNextRoom(DoorPosition position, PlayerController player)
    {
        if(map.IsClear == false)
        {
            return;
        }

        switch (position)
        {
            case DoorPosition.Top:
                nextRooms[(int)position]?.MoveFormNextRoom(DoorPosition.Bottom, player); break;
            case DoorPosition.Bottom:
                nextRooms[(int)position]?.MoveFormNextRoom(DoorPosition.Top, player); break;
            case DoorPosition.Left:
                nextRooms[(int)position]?.MoveFormNextRoom(DoorPosition.Right, player); break;
            case DoorPosition.Right:
                nextRooms[(int)position]?.MoveFormNextRoom(DoorPosition.Left,player); break;
        }
    }

    public void MoveFormNextRoom(DoorPosition position, PlayerController player)
    {
        player.transform.position = SpawnPosition[(int)position].transform.position;

        if (this.roomType == RoomType.Boss)
            SceneManager.LoadScene("Ending");

        Vector3 CameraOffset = Vector3.zero;
        switch (position)
        { 
            case DoorPosition.Top: CameraOffset = Vector3.down * 12; break;
            case DoorPosition.Bottom: CameraOffset = Vector3.up * 12; break;
            case DoorPosition.Left: CameraOffset = Vector3.right * 20; break;
            case DoorPosition.Right:CameraOffset = Vector3.left * 20; break;
        }

        Camera.main.transform.position = Camera.main.transform.position + CameraOffset;
    }
}
