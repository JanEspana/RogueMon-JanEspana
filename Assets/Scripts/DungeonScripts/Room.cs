using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isStartRoom;
    public List<Tuple<Directions, Room>> neighbourRooms;
    public int xPos, yPos;
    public List<GameObject> doors, enemies;
    public DungeonGen dungeonGen;
    public bool isOpen = false;
    public Room(int x, int y)
    {
        xPos = x;
        yPos = y;
    }

    internal void AddNeighbour(Room otherRoom, Directions up)
    {
        if (neighbourRooms == null)
        {
            neighbourRooms = new List<Tuple<Directions, Room>>();
        }
        neighbourRooms.Add(new Tuple<Directions, Room>(up, otherRoom));
    }
    internal void AddEnemy(GameObject enemy)
    {
        if (enemies == null)
        {
            enemies = new List<GameObject>();
        }
        enemies.Add(enemy);
    }
    internal Directions GetDirection(Room previousRoom)
    {
        if (previousRoom.xPos == xPos)
        {
            if (previousRoom.yPos < yPos)
            {
                return Directions.UP;
            }
            else
            {
                return Directions.DOWN;
            }
        }
        else
        {
            if (previousRoom.xPos < xPos)
            {
                return Directions.RIGHT;
            }
            else
            {
                return Directions.LEFT;
            }
        }
    }
}