using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Defines.DungeonDefines;
using System.Linq;
using UnityEngine.UI;

public class DungeonGenerator : MonoSingleton<DungeonGenerator>
{
    private const float _screenX = 1920f, _screenY = 1080f;
    [Header("Map Size")]
    [SerializeField] private int _widthCount;
    [SerializeField] private int _heightCount;

    [Header("Room Count")]
    [SerializeField] private int _totalRoomCount;
    [SerializeField] private int _specialRoomCount;
    [SerializeField] private int _BossRoomCount;

    [Header("btn")]
    [SerializeField] private Button btn;

    private RoomInfo[,] _mapList;
    private List<RoomInfo> _roomInfos;

    public override void Start()
    {
        btn.onClick.AddListener(() => { GenerateDungeon(); });
    }

    public bool GenerateDungeon()
    {
        _mapList = new RoomInfo[_heightCount, _widthCount];

        int roomCount = _totalRoomCount;
        int totalMapSize = _heightCount * _widthCount;

        // 기본 맵 생성
        for (int i = 0; i < _heightCount; ++i)
        {
            int _iValue = i * _widthCount;
            for(int j = 0; j < _widthCount; ++j)
            {
                if (roomCount <= 0) // 모든 맵이 할당 되었다면
                {
                    _mapList[i, j].Type = RoomType.RT_None;
                    continue;
                }

                int nValue = UnityEngine.Random.Range(0, totalMapSize - (_iValue + j));
                _mapList[i, j].Type = nValue < roomCount ? RoomType.RT_Normal : RoomType.RT_None;
                
                if(nValue < roomCount)
                {
                    _mapList[i, j].SetRoom(new Vector3(i, j, 0f));
                    _roomInfos.Add(_mapList[i, j]);
                    --roomCount;
                }

            }
        }

        // 기본 맵 중 중심점 찾기

        // 맵 중심점으로 이으기

        // 각 룸 설렉하기
        // 보스룸: 마지막에 연결된 룸
        // 스페셜 룸: 마지막 직전에 연결된 룸
        // 스타트룸: 가장 중간에 있는 룸


        return true;
    }

    private void GetRandomRoomInfo()
    {
        
    }
}
