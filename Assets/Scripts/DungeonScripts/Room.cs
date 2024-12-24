using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool isStartRoom;
    public List<Tuple<Directions, Room>> neighbourRooms;
    public int xPos, yPos;
    public List<GameObject> doors;
    public DungeonGen dungeonGen;
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