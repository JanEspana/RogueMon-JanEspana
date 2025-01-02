using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform room;
    Vector3 offset;
    float smoothSpeed = 0.125f;
    private void Start()
    {
        //set the 
        /*player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;*/
    }
    private void LateUpdate()
    {
        /*Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player);*/
    }
}
