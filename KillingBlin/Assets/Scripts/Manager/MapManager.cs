using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defines.DungeonDefines;
using System;


public class MapManager : MonoSingleton<MapManager>
{
    [Serializable]
    private class MapByDifficulty
    {
        public List<GameObject> maps;
    }

    [Serializable]
    private class MapByType
    {
        public RoomType type;
        public MapByDifficulty[] maps = new MapByDifficulty[(int)Difficulty.Boss + 1];
    }


    [SerializeField]
    private MapByType[] maps = new MapByType[(int)RoomType.MAX];

    public GameObject GetRandomMap(RoomType type, Difficulty difficulty)
    {
        GameObject map = null;

        MapByType types = maps[(int)type];
        var mapList = types.maps[(int)difficulty].maps;

        int index = UnityEngine.Random.Range(0, mapList.Count);
        map = mapList[index];

        return map;
    }
}
