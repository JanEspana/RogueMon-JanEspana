using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBG : MonoBehaviour
{
    public float scrollSpeed;
    [SerializeField]
    private Renderer bgRenderer;
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);
        bgRenderer.material.mainTextureOffset = offset;
    }
}
