using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [SerializeField] SpriteRenderer map;

    public Vector2Int RoomIndex { get; set; }
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

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
        }
        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
        }
        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
        }
        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
        }
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
        }
    }
}
