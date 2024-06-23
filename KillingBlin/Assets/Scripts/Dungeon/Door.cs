using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;

public class Door : MonoBehaviour
{
    [SerializeField] Room room;
    public Room MyRoom { get => room; set => room = value; }
    [SerializeField] DoorPosition myPosition;
    public DoorPosition MyPosition { get => myPosition; private set => myPosition = value; }

    public void OnMoveToNextDoor(PlayerController player)
    {
        MyRoom?.MoveToNextRoom(MyPosition, player);
    }
}
