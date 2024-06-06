using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField] private RoomBase[] _roomList;

    public bool GetRandomRoom(in RoomInfo info, out RoomBase roomBase)
    {
        RoomInfo room = info;
        var potionalRoom = _roomList.Where(
            _ => _.RoomType == room.Type && // 방의 타입
            _.Difficulty == room.Difficulty && // 방의 난이도
            _.isDoorPositionMatch(room.DoorPos) // 방의 문 위치
            ).Select(o => o);

        if (potionalRoom.Count() <= 0)
        {
            roomBase = null;
            return false;
        }    

        int roomNum = Random.Range(0, potionalRoom.Count());
        roomBase = potionalRoom.ElementAt(roomNum);
        return true;
    }
}
