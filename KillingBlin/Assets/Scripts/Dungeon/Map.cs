using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map : MonoBehaviour
{
    [SerializeField] private Room roomInfo;
    [SerializeField] private List<MonsterBase> enemy = new List<MonsterBase>();

    public bool IsClear 
    {
        get
        {
            bool isClear = true;
            foreach(Character e in enemy)
            {
                isClear = e.Status.IsDead == true;
            }
            return isClear;
        }
    }

    public void SetRoom(Room room)
    {
        roomInfo = room;
    }

    public void OnReset()
    {
        
    }
}
