using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;

public class RoomSettingTemp : MonoBehaviour
{
    [SerializeField] RoomBase _base;

    [Header("Door")]
    [SerializeField] GameObject _top;
    [SerializeField] GameObject _bottom;
    [SerializeField] GameObject _left;
    [SerializeField] GameObject _right;

    [Header("Attribute")]
    [SerializeField] SpriteRenderer _type;
    [SerializeField] SpriteRenderer _difficulty;

    private void OnEnable()
    {
        SetRoom();
    }

    private void SetRoom()
    {
        _top.SetActive(((byte)_base.DoorPos & (byte)DoorPosition.DP_Top) != 0);
        _bottom.SetActive(((byte)_base.DoorPos & (byte)DoorPosition.DP_Bottom) != 0);
        _left.SetActive(((byte)_base.DoorPos & (byte)DoorPosition.DP_Left) != 0);
        _right.SetActive(((byte)_base.DoorPos & (byte)DoorPosition.DP_Right) != 0);

        switch (_base.RoomType) 
        {
            case RoomType.RT_Start: _type.color = Color.white; break;
           // case RoomType.RT_End: _type.color = Color.gray; break;
            case RoomType.RT_Boss: _type.color = Color.black; break;
            case RoomType.RT_Normal: _type.color = Color.yellow; break;
            case RoomType.RT_Special: _type.color = Color.cyan; break;
            default: _type.color = Color.red; break;
        }

        switch(_base.Difficulty)
        {
            case RoomDifficulty.RD_Easy:
            case RoomDifficulty.RD_Mix_Easy:
            case RoomDifficulty.RD_Long_Easy:
            case RoomDifficulty.RD_Melee_Easy: _difficulty.color = Color.blue; break;

            case RoomDifficulty.RD_Mid:
            case RoomDifficulty.RD_Mix_Mid:
            case RoomDifficulty.RD_Long_Mid:
            case RoomDifficulty.RD_Melee_Mid: _difficulty.color = Color.yellow; break;

            case RoomDifficulty.RD_Hard:
            case RoomDifficulty.RD_Mix_Hard:
            case RoomDifficulty.RD_Long_Hard:
            case RoomDifficulty.RD_Melee_Hard: _difficulty.color = Color.red; break;

            default: _difficulty.color = Color.black; break;
        }
    }
}