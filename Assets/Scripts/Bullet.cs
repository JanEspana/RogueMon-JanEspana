using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float lifeTime;
    private GameObject spawnPoint;
    // Start is called before the first frame update

    void OnEnable()
    {
        force = 5f;
        lifeTime = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        rb.AddForce(spawnPoint.transform.right * force, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        EndOfLifeTime();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lifeTime = 1.5f;
        Shoot.instance.Push(gameObject);
    }
    void EndOfLifeTime()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            lifeTime = 1.5f;
            Shoot.instance.Push(gameObject);
        }
    }
}