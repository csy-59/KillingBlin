using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Defines.DungeonDefines;
using UnityEditor.Search;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RoomManager : MonoSingleton<RoomManager>
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms = 20;
    [SerializeField] private int minRooms = 15;

    const int roomWidth = 20;
    const int roomHeight = 12;
    const float roomGenerateFailRate = 0.5f;

    const int gridSizeX = 15;
    const int gridSizeY = 15;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private bool generationComplete = false;

    private int roomCount;

    private Room startRoom;
    private Room bossRoom;

    private Room specialRoom;

    [SerializeField] Image image;

    public void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        // �� �߾��� ���� ������ ����
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    public void Update()
    {
        // ������ �濡������ �� ����� �õ�
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
        // �� ���� �����ϴٸ� �ٽ� �õ�
        else if(roomCount < minRooms)
        {
            RegenerateRooms();
        }
        // ���� ����
        else if(generationComplete == false)
        {
            generationComplete = true;
            EndRoomGeneration();
        }
    }

    //  ������ �� �����
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

    // ����� ���� �� �� ����
    private void EndRoomGeneration()
    {
        SetRoomType();
        SetRoomDifficulty();
        SetRooms();
        image.gameObject.SetActive(false);
    }

    // ���� Ÿ��(����, ����, �����, ���) ����
    private void SetRoomType()
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

    // �� ���� ���̵� ����(����, �߰�, �����
    private void SetRoomDifficulty()
    {
        bool[,] isRoomVisited = new bool[gridSizeX, gridSizeY];
        List<List<Room>> roomsByDepth = new List<List<Room>>();
        Queue<Room> queue = new Queue<Room>();
        int count = 1; int depth = 0;

        queue.Enqueue(startRoom);
        roomsByDepth.Add(new List<Room>());

        do
        {
            if (count <= 0)
            {
                count = queue.Count;
                ++depth;
                roomsByDepth.Add(new List<Room>());
            }

            Room r = queue.Peek();
            queue.Dequeue();
            --count;

            if (isRoomVisited[r.RoomIndex.x, r.RoomIndex.y] == true)
                continue;

            isRoomVisited[r.RoomIndex.x, r.RoomIndex.y] = true;
            roomsByDepth[depth].Add(r);

            Room nRoom = null;
            if(r.TryGetNextRoom(DoorPosition.Top, ref nRoom) == true && isRoomVisited[nRoom.RoomIndex.x, nRoom.RoomIndex.y] == false)
            {
                queue.Enqueue(nRoom);
            }
            if(r.TryGetNextRoom(DoorPosition.Bottom, ref nRoom) && isRoomVisited[nRoom.RoomIndex.x, nRoom.RoomIndex.y] == false)
            {
                queue.Enqueue(nRoom);
            }
            if(r.TryGetNextRoom(DoorPosition.Left, ref nRoom) && isRoomVisited[nRoom.RoomIndex.x, nRoom.RoomIndex.y] == false)
            {
                queue.Enqueue(nRoom);
            }
            if(r.TryGetNextRoom(DoorPosition.Right, ref nRoom) && isRoomVisited[nRoom.RoomIndex.x, nRoom.RoomIndex.y] == false)
            {
                queue.Enqueue(nRoom);
            }

        } while (queue.Count > 0);

        // ������ ���� ���̵� ����
        int depthCount = roomsByDepth.Count;
        int roomsCount = 0; int bossDifficulty = (int)Difficulty.Boss;
        for(int i = 0; i < depthCount; ++i)
        {
            roomsCount = roomsByDepth[i].Count;
            var rooms = roomsByDepth[i];
            for(int j = 0; j< roomsCount; ++j)
            {
                rooms[j].Difficulty = (i < bossDifficulty ? (Difficulty)i : Difficulty.Hard);
            }
        }

        // ����� ��� ���� �� �ٷ� ���� ���� ������ ���̵� �ϵ�
        SetAroundRoomDifficulty(specialRoom, Difficulty.Hard);
        SetAroundRoomDifficulty(bossRoom, Difficulty.Hard);

        // �߿� �� ���̵� ����
        startRoom.Difficulty = Difficulty.None;
        specialRoom.Difficulty = Difficulty.None;
        bossRoom.Difficulty = Difficulty.Boss;
    }

    private void SetRooms()
    {
        foreach (var r in roomObjects)
        {
            r.GetComponent<Room>().SetRoom();
        }
    }

    private void SetAroundRoomDifficulty(Room originRoom, Difficulty difficulty)
    {
        Room nRoom = null;
        if (originRoom.TryGetNextRoom(DoorPosition.Top, ref nRoom))
        {
            nRoom.Difficulty = difficulty;
        }
        if (originRoom.TryGetNextRoom(DoorPosition.Bottom, ref nRoom))
        {
            nRoom.Difficulty = difficulty;
        }
        if (originRoom.TryGetNextRoom(DoorPosition.Left, ref nRoom))
        {
            nRoom.Difficulty = difficulty;
        }
        if (originRoom.TryGetNextRoom(DoorPosition.Right, ref nRoom))
        {
            nRoom.Difficulty = difficulty;
        }
    }

    // �� ����� �õ�: �̹� ���� �� ������ų�, ��ȿ�� ���� �ƴ� ���, Ȥ�� ������ Ȯ���� ������ �̷������ ����
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


    // �� ����� �ٽ� �õ�
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

    // ���� �� ����
    private void OpenDoor(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();
        Room nextRoomScript;

        if (x - 1 >= 0 && roomGrid[x - 1, y] != 0)
        {
            nextRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
            nextRoomScript.OpenDoor(DoorPosition.Right, newRoomScript);
            newRoomScript.OpenDoor(DoorPosition.Left, nextRoomScript);
        }
        if (x + 1 < gridSizeX && roomGrid[x + 1, y] != 0)
        {
            nextRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
            nextRoomScript.OpenDoor(DoorPosition.Left, newRoomScript);
            newRoomScript.OpenDoor(DoorPosition.Right, nextRoomScript);
        }
        if (y - 1 >= 0 && roomGrid[x, y - 1] != 0)
        {
            nextRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));
            nextRoomScript.OpenDoor(DoorPosition.Top, newRoomScript);
            newRoomScript.OpenDoor(DoorPosition.Bottom, nextRoomScript);
        }
        if (y + 1 < gridSizeY && roomGrid[x, y + 1] != 0)
        {
            nextRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
            nextRoomScript.OpenDoor(DoorPosition.Bottom, newRoomScript);
            newRoomScript.OpenDoor(DoorPosition.Top, nextRoomScript);
        }
    }

    // Ư�� ��ġ�� �� ��ũ��Ʈ ��������
    private Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject roomObject = roomObjects.Find(_ => _.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject.GetComponent<Room>();

        return null;
    }

    // �ֺ��� �׻� ���� �ϳ��� �ֵ��� üũ
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

    // �׸��忡�� �ش� ��ǥ(�ε���)���� ���� ��ǥ�� ��������
    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridx = gridIndex.x;
        int gridy = gridIndex.y;
        return new Vector3(roomWidth * (gridx - gridSizeX / 2),
            roomHeight * (gridy - gridSizeY / 2), 0f);
    }
}
