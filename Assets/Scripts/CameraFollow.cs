using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(0.5f, 0, -10);
    }
}
