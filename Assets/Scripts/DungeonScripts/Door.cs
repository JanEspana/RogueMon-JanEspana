using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public bool isOpen;
    public Room room;
    TilemapRenderer doorSprite;
    BoxCollider2D doorCollider;
    private void Awake()
    {
        room = GetComponentInParent<Room>();
        doorSprite = GetComponent<TilemapRenderer>();
        doorSprite.enabled = false;
        doorCollider = GetComponent<BoxCollider2D>();
        doorCollider.enabled = false;
        isOpen = false;
    }
    void Update()
    {
        OpenDoor();
    }
    public void OpenDoor()
    {
        if (isOpen)
        {
            doorSprite.enabled = true;
            doorCollider.enabled = true;
        }
        else
        {
            doorSprite.enabled = false;
            doorCollider.enabled = false;
        }
    }
    public Directions DoorDirection()
    {
        if (gameObject.name == "DoorN")
        {
            return Directions.UP;
        }
        else if (gameObject.name == "DoorS")
        {
            return Directions.DOWN;
        }
        else if (gameObject.name == "DoorE")
        {
            return Directions.RIGHT;
        }
        else
        {
            return Directions.LEFT;
        }
    }
    public void CheckRoom(GameObject door)
    {
        foreach (Tuple<Directions, Room> neighbour in room.neighbourRooms)
        {
            if (neighbour.Item1 == DoorDirection())
            {
                isOpen = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Directions doorDirection = DoorDirection();

            if (doorDirection == Directions.UP)
            {
                player.transform.position += new Vector3(0, 4.2f, 0);
            }
            else if (doorDirection == Directions.DOWN)
            {
                player.transform.position += new Vector3(0, -4.2f, 0);
            }
            else if (doorDirection == Directions.LEFT)
            {
                player.transform.position += new Vector3(-4.2f, 0, 0);
            }
            else if (doorDirection == Directions.RIGHT)
            {
                player.transform.position += new Vector3(4.2f, 0, 0);
            }
        }
    }
}