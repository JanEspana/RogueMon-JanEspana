using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    DungeonGen dungeonGen;
    Room room;
    private void Awake()
    {
        dungeonGen = DungeonGen.instance;
        room = GetComponentInParent<Room>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (room.isOpen)
        {
            dungeonGen.ResetDungeon();
        }
    }
}
