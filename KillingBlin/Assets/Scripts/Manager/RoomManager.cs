using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;

    const int roomWidth = 20;
    const int roomHeight = 12;
    const float roomGenerateFailRate = 0.5f;

    [SerializeField] const int gridSizeX = 10;
    [SerializeField] const int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private bool generationComplete = false;

    private int roomCount;

    private Room startRoom;
    private Room bossRoom;

    private Room specialRoom;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        // 맵 중앙을 시작 방으로 설정
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        // 시작의 방에서부터 방 만들기 시도
        if (roomQueue.Count > 0 && roomCount < maxRooms && generationComplete == false)
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridx = roomIndex.x;
            int gridy = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridx - 1, gridy));
            TryGenerateRoom(new Vector2Int(gridx + 1, gridy));
            TryGenerateRoom(new Vector2Int(gridx, gridy - 1));
            TryGenerateRoom(new Vector2Int(gridx, gridy + 1));

        }
        // 방 수가 부족하다면 다시 시도
        else if(roomCount < minRooms)
        {
            RegenerateRooms();
        }
        // 생성 종료
        else if(generationComplete == false)
        {
            generationComplete = true;
            EndRoomGeneration();
        }
    }

    //  시작의 방 만들기
    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        ++roomCount;
        GameObject initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        startRoom = initialRoom.GetComponent<Room>();
        startRoom.RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    // 방생성 종료 후 방 세팅
    private void EndRoomGeneration()
    {
        foreach (var r in roomObjects)
        {
            r.GetComponent<Room>().RoomType = Defines.DungeonDefines.RoomType.Normal;
        }

        var specialRooms = roomObjects.FindAll(_ => (CountAdjacentRooms(_.GetComponent<Room>().RoomIndex) == 1));

        specialRoom = specialRooms[specialRooms.Count() / 2].GetComponent<Room>();
        specialRoom.RoomType = Defines.DungeonDefines.RoomType.Special;
        startRoom.RoomType = Defines.DungeonDefines.RoomType.Start;
        bossRoom.RoomType = Defines.DungeonDefines.RoomType.Boss;
    }

    // 방 만들기 시도: 이미 방을 다 만들었거나, 유효한 방이 아닐 경우, 혹은 랜덤의 확률로 생성이 이루어지지 않음
    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (roomCount >= maxRooms)
            return false;

        float value = Random.Range(0f, 1f);
        if (value < roomGenerateFailRate && roomIndex != Vector2Int.zero)
            return false;

        if (CountAdjacentRooms(roomIndex) > 1)
            return false;

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        ++roomCount;

        GameObject newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), transform.rotation);
        newRoom.name = $"Room-{roomCount}";
        SetRoomType(newRoom).RoomIndex = roomIndex;
        roomObjects.Add(newRoom);

        OpenDoor(newRoom, x, y);


        return true;
    }

    private Room SetRoomType(GameObject room)
    {
        bossRoom = room.GetComponent<Room>();
        return bossRoom;
    }


    // 방 만들기 다시 시도
    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    // 방의 문 열기
    private void OpenDoor(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();


        if (x - 1 >= 0 && roomGrid[x - 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            GetRoomScriptAt(new Vector2Int(x - 1, y)).OpenDoor(Vector2Int.right);
        }
        if (x + 1 < gridSizeX && roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            GetRoomScriptAt(new Vector2Int(x + 1, y)).OpenDoor(Vector2Int.left);
        }
        if (y - 1 >= 0 && roomGrid[x, y - 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            GetRoomScriptAt(new Vector2Int(x, y - 1)).OpenDoor(Vector2Int.up);
        }
        if (y + 1 < gridSizeY && roomGrid[x, y + 1] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            GetRoomScriptAt(new Vector2Int(x, y + 1)).OpenDoor(Vector2Int.down);
        }
    }

    // 특정 위치의 방 스크립트 가져오기
    private Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(_ => _.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();

        return null;
    }

    // 주변에 항상 방이 하나만 있도록 체크
    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if(x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY)
                return 10;

        if (x - 1 >= 0 && roomGrid[x - 1, y] != 0) ++count;
        if (x + 1 < gridSizeX && roomGrid[x + 1, y] != 0) ++count;
        if (y - 1 >= 0 && roomGrid[x, y - 1] != 0) ++count;
        if (y + 1 < gridSizeY && roomGrid[x, y + 1] != 0) ++count;

        return count;
    }

    // 그리드에서 해당 좌표(인덱스)에서 실제 좌표값 가져오기
    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridx = gridIndex.x;
        int gridy = gridIndex.y;
        return new Vector3(roomWidth * (gridx - gridSizeX / 2),
            roomHeight * (gridy - gridSizeY / 2), 0f);
    }
}
