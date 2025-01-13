using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject background;
    public GameObject player;
    public float speedOffset;
    // Start is called before the first frame update
    private void Update()
    {
        MoveRoomScrolling();
    }
    void MoveRoomScrolling()
    {
        Vector3 offset = new Vector3(player.transform.position.x * speedOffset, player.transform.position.y * speedOffset, 0);
        background.transform.position = offset;
    }
}
