using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public bool isValid;
    public Room room;
    TilemapRenderer doorSprite;
    BoxCollider2D doorCollider;
    private void Awake()
    {
        mainCamera = Camera.main;
        room = GetComponentInParent<Room>();
        doorSprite = GetComponent<TilemapRenderer>();
        doorSprite.enabled = false;
        doorCollider = GetComponent<BoxCollider2D>();
        doorCollider.enabled = false;
        isValid = false;
    }
    void Update()
    {
        OpenDoor();
        CheckEnemies();
    }
    public void OpenDoor()
    {
        if (isValid)
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
                isValid = true;
            }
        }
    }
    public void CheckEnemies()
    {
        foreach (GameObject enemy in room.enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }
        room.isOpen = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && room.isOpen)
        {
            GameObject player = collision.gameObject;
            Directions doorDirection = DoorDirection();

            if (doorDirection == Directions.UP)
            {
                player.transform.position += new Vector3(0, 4.2f, 0);
                //move also the camera.
                mainCamera.transform.position += new Vector3(0, 10f, 0);
            }
            else if (doorDirection == Directions.DOWN)
            {
                player.transform.position += new Vector3(0, -4.2f, 0);
                mainCamera.transform.position += new Vector3(0, -10f, 0);
            }
            else if (doorDirection == Directions.LEFT)
            {
                player.transform.position += new Vector3(-4.2f, 0, 0);
                mainCamera.transform.position += new Vector3(-19f, 0, 0);
            }
            else if (doorDirection == Directions.RIGHT)
            {
                player.transform.position += new Vector3(4.2f, 0, 0);
                mainCamera.transform.position += new Vector3(19f, 0, 0);
            }
        }
    }
}