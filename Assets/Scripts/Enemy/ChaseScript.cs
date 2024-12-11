using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Chase(Transform target, Transform myself)
    {
        rb.velocity = (target.position - myself.position).normalized * speed;
    }
    public void StopChase()
    {
        rb.velocity = Vector2.zero;
    }
}
