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
    public List<GameObject> roomPrefabs, enemyPrefabs;
    private List<GameObject> enemyInstances;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        instance = this;
        GenerateDungeon();
        CheckPositions();
        AssignNeighbours();
        EnableDoors();
        GenerateEnemies();
    }

    void GenerateDungeon()
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
                roomInstances[i] = Instantiate(SearchStartRoom().gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                newRoom = roomInstances[i].GetComponent<Room>();
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
                }
                else
                {
                    do
                    {
                        direction = (Directions)UnityEngine.Random.Range(0, 4);
                    } while (newRoom.GetDirection(previousRoom) == direction);
                }
                roomPosition = newRoom.transform;
                MoveRoom(newRoom, direction);
            }

        }
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
    void CheckPositions()
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
                    GameObject newEnemy, previousEnemy;
                    Vector3 offset = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-3, 3), 0);
                    Debug.Log("Generating enemies in room " + room);
                    int randomEnemy = UnityEngine.Random.Range(0, enemyPrefabs.Count);
                    newEnemy = Instantiate(enemyPrefabs[randomEnemy], room.transform.position + offset, Quaternion.identity);
                    enemyInstances.Add(newEnemy);
                }
            }
        }
    }
}