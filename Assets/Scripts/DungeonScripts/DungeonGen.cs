using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
    public GameObject player;
    public static DungeonGen instance;
    private int maxRooms;
    private Queue<Room> roomsPending;
    private List<Room> dungeonRooms;
    private List<GameObject> roomInstances;
    public List<GameObject> roomPrefabs, enemyPrefabs, propPrefabs;
    public List<GameObject> enemyInstances;
    public GameObject endRoomPrefab;

    public GameObject waterBG;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        instance = this;
        GenerateDungeon();
        CreateEndRoom();
        CheckPositions();
        AssignNeighbours();
        GenerateEnemies();
        EnableDoors();
    }

    public void GenerateDungeon()
    {
        roomInstances = new List<GameObject>();
        enemyInstances = new List<GameObject>();

        dungeonRooms = new List<Room>();
        roomsPending = new Queue<Room>();
        maxRooms = UnityEngine.Random.Range(7, 10) + 1;
        for (int i = 0; i < maxRooms; i++)
        {
            Transform roomPosition = null;

            Room newRoom = new Room(0, 0);
            Room previousRoom = new Room(0, 0);

            if (i == 0)
            {
                roomInstances.Add(new GameObject());
                roomInstances[0] = Instantiate(SearchStartRoom().gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                newRoom = roomInstances[0].GetComponent<Room>();
                roomsPending.Enqueue(newRoom);
                dungeonRooms.Add(newRoom);
                newRoom.isStartRoom = true;
            }
            else
            {
                previousRoom = dungeonRooms[dungeonRooms.Count - 1];
                roomInstances.Add(new GameObject());
                roomPosition = previousRoom.transform;
                roomInstances[i] = Instantiate(SearchRandomRoom().gameObject, roomPosition.position, Quaternion.identity);
                newRoom = roomInstances[i].GetComponent<Room>();
                roomsPending.Enqueue(newRoom);
                dungeonRooms.Add(newRoom);
                newRoom.isStartRoom = false;

                Directions direction;
                if (previousRoom.isStartRoom)
                {
                    direction = Directions.UP;
                    MoveRoom(newRoom, direction);
                }
                else
                {
                    direction = (Directions)UnityEngine.Random.Range(0, 4);
                    if (IsRoomImposible(newRoom))
                    {
                        Debug.Log("Imposible room");
                        Destroy(roomInstances[i]);
                        roomInstances.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        MoveRoom(newRoom, direction);
                        while (IsOverlaping(newRoom))
                        {
                            direction = (Directions)UnityEngine.Random.Range(0, 4);
                            MoveRoom(newRoom, direction);
                        }
                    }
                }
            }
        }
        Debug.Log(dungeonRooms.Count);
    }
    bool IsRoomImposible(Room room)
    {
        bool up = false, down = false, left = false, right = false;
        foreach (Room otherRoom in dungeonRooms)
        {
            if (room != otherRoom)
            {
                if (room.transform.position + new Vector3(0, 10, 0) == otherRoom.transform.position)
                {
                    up = true;
                }
                if (room.transform.position + new Vector3(0, -10, 0) == otherRoom.transform.position)
                {
                    down = true;
                }
                if (room.transform.position + new Vector3(-19, 0, 0) == otherRoom.transform.position)
                {
                    left = true;
                }
                if (room.transform.position + new Vector3(19, 0, 0) == otherRoom.transform.position)
                {
                    right = true;
                }
            }
            if (up == down == left == right == false)
            {
                return false;
            }
        }
        return true;
    }
    bool IsOverlaping(Room room)
    {
        foreach (Room otherRoom in dungeonRooms)
        {
            if (room != otherRoom)
            {
                if (room.transform.position == otherRoom.transform.position)
                {
                    Debug.Log("Overlaping");
                    return true;
                }
            }
        }
        return false;
    }
    Room SearchStartRoom()
    {
        bool found = false;
        Room thisRoom = null;
        for (int i = 0; i < roomPrefabs.Count && !found; i++)
        {
            thisRoom = roomPrefabs[i].GetComponent<Room>();
            if (thisRoom.isStartRoom)
            {
                found = !found;
            }
        }
        return thisRoom;
    }
    Room SearchRandomRoom()
    {
        bool found = false;
        Room thisRoom = null;
        while (!found)
        {
            int randomIndex = UnityEngine.Random.Range(0, roomPrefabs.Count);
            thisRoom = roomPrefabs[randomIndex].GetComponent<Room>();
            if (!thisRoom.isStartRoom)
            {
                found = !found;
            }
        }
        return thisRoom;
    }
    void MoveRoom(Room room, Directions direction)
    {
        switch (direction)
        {
            case Directions.UP:
                room.transform.position += new Vector3(0, 10, 0);
                break;
            case Directions.DOWN:
                room.transform.position += new Vector3(0, -10, 0);
                break;
            case Directions.LEFT:
                room.transform.position += new Vector3(-19, 0, 0);
                break;
            case Directions.RIGHT:
                room.transform.position += new Vector3(19, 0, 0);
                break;
        }
    }
    public void CheckPositions()
    {
        foreach (Room room in dungeonRooms)
        {
            foreach (Room otherRoom in dungeonRooms)
            {
                if (room != otherRoom)
                {
                    if (room.transform.position == otherRoom.transform.position)
                    {
                        Directions direction = (Directions)UnityEngine.Random.Range(0, 4);
                        MoveRoom(room, direction);
                    }
                }
            }
        }
    }
    public void AssignNeighbours()
    {
        for (int i = 0; i < dungeonRooms.Count; i++)
        {
            for (int j = 0; j < dungeonRooms.Count; j++)
            {
                if (i != j)
                {
                    if (dungeonRooms[i].transform.position + new Vector3(19, 0, 0) == dungeonRooms[j].transform.position)
                    {
                        dungeonRooms[i].AddNeighbour(dungeonRooms[j], Directions.RIGHT);
                    }
                    if (dungeonRooms[i].transform.position + new Vector3(-19, 0, 0) == dungeonRooms[j].transform.position)
                    {
                        dungeonRooms[i].AddNeighbour(dungeonRooms[j], Directions.LEFT);
                    }
                    if (dungeonRooms[i].transform.position + new Vector3(0, 10, 0) == dungeonRooms[j].transform.position)
                    {
                        dungeonRooms[i].AddNeighbour(dungeonRooms[j], Directions.UP);
                    }
                    if (dungeonRooms[i].transform.position + new Vector3(0, -10, 0) == dungeonRooms[j].transform.position)
                    {
                        dungeonRooms[i].AddNeighbour(dungeonRooms[j], Directions.DOWN);
                    }
                }
            }
        }
    }
    public void EnableDoors()
    {
        //enable doors if there is a room in that direction
        foreach (Room room in dungeonRooms)
        {
            foreach (GameObject door in room.doors)
            {
                door.GetComponent<Door>().CheckRoom(door);
            }
        }
    }
    public void GenerateEnemies()
    {
        //generate enemies in the dungeon
        for (int i = 0; i < dungeonRooms.Count; i++)
        {
            Room room = dungeonRooms[i];
            if (!room.isStartRoom)
            {
                int enemyCount = UnityEngine.Random.Range(1, 4);
                for (int j = 0; j < enemyCount; j++)
                {
                    GameObject newEnemy;
                    Vector3 offset = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-3, 3), 0);
                    Vector3 enemyPosition = room.transform.position + offset;
                    while (Physics2D.OverlapCircle(enemyPosition, 1, LayerMask.GetMask("Void")))
                    {
                        offset = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-3, 3), 0);
                        enemyPosition = room.transform.position + offset;
                    }
                    int randomEnemy = UnityEngine.Random.Range(0, enemyPrefabs.Count);
                    newEnemy = Instantiate(enemyPrefabs[randomEnemy], enemyPosition, Quaternion.identity);
                    enemyInstances.Add(newEnemy);
                    room.AddEnemy(newEnemy);
                }
            }
        }
    }
    public void CreateEndRoom()
    {
        Room lastRoom = dungeonRooms[dungeonRooms.Count - 1];
        GameObject endRoom = Instantiate(endRoomPrefab, lastRoom.transform.position, Quaternion.identity);
        Room endRoomScript = endRoom.GetComponent<Room>();
        roomInstances.Add(endRoom);
        dungeonRooms.Add(endRoomScript);
        endRoomScript.isStartRoom = false;
        //move it to a free position
        Directions direction = (Directions)UnityEngine.Random.Range(0, 4);
        MoveRoom(endRoomScript, direction);
        while (IsOverlaping(endRoomScript))
        {
            direction = (Directions)UnityEngine.Random.Range(0, 4);
            MoveRoom(endRoomScript, direction);
        }
    }
    public void ResetDungeon()
    {
        //black screen

        dungeonRooms.Clear();

        player.transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector3(0.5f, 0, -10);

        foreach (GameObject room in roomInstances)
        {
            Destroy(room);
        }
        foreach (GameObject enemy in enemyInstances)
        {
            Destroy(enemy);
        }
        StartCoroutine(Regenerate());
    }
    IEnumerator Regenerate()
    {
        yield return new WaitForSeconds(0.01f);
        GenerateDungeon();
        CreateEndRoom();
        CheckPositions();
        AssignNeighbours();
        GenerateEnemies();
        EnableDoors();
    }
}