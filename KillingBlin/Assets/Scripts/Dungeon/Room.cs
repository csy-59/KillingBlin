using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;
using System.Runtime.InteropServices.WindowsRuntime;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject bottomDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    private Room[] nextRooms = new Room[(int)DoorPosition.MAX];

    [Header("RoomType")]
    [SerializeField] private SpriteRenderer map;
    private RoomType roomType = RoomType.Normal;
    public RoomType RoomType
    {
        get => roomType;
        set
        {
            roomType = value;
            switch(roomType)
            {
                case RoomType.Start: map.color = Color.white; break;
                case RoomType.Boss: map.color = Color.black; break;
                case RoomType.Normal: map.color = Color.green; break;
                case RoomType.Special: map.color = Color.cyan; break;
            }
        }
    }

    [Header("Difficulty")]
    [SerializeField] private SpriteRenderer difficultySprite;
    [SerializeField] private Difficulty difficulty = Difficulty.None;
    public Difficulty Difficulty
    {
        get => difficulty;
        set
        {
            difficulty = value;
            switch (difficulty)
            {
                case Difficulty.None: difficultySprite.color = Color.white; break;
                case Difficulty.Easy: difficultySprite.color = Color.blue; break;
                case Difficulty.Mid_1: difficultySprite.color = Color.yellow; break;
                case Difficulty.Mid_2: difficultySprite.color = Color.yellow; break;
                case Difficulty.Hard: difficultySprite.color = Color.red; break;
                case Difficulty.Boss: difficultySprite.color = Color.black; break;
            }
        }
    }

    public Vector2Int RoomIndex { get; set; }
    

    public void OpenDoor(DoorPosition direction, Room nextRoom)
    {
        GameObject door = topDoor;
        int nextRoomIndex = 0;
        switch (direction)
        {
            case DoorPosition.Top:      { door = topDoor; nextRoomIndex = (int)DoorPosition.Top; break; }
            case DoorPosition.Bottom:   { door = bottomDoor; nextRoomIndex = (int)DoorPosition.Bottom; break; }
            case DoorPosition.Left:     { door = leftDoor; nextRoomIndex = (int)DoorPosition.Left; break; }
            case DoorPosition.Right:    { door = rightDoor; nextRoomIndex = (int)DoorPosition.Right; break; }
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
}
