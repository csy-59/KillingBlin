using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    [SerializeField] private int _roomID = -1;
    public int RoomID { get => _roomID; protected set { _roomID = value; } }


}
