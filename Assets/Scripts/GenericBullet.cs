using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    public float lifeTime;
    private GameObject spawnPoint;

    // Start is called before the first frame update
    public void OnEnable()
    {
        force = 0.5f;
        lifeTime = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = FindSpawnPoint();
        rb.AddForce(spawnPoint.transform.right * force, ForceMode2D.Impulse);
    }
    public abstract GameObject FindSpawnPoint();
    // Update is called once per frame
    public void Update()
    {
        EndOfLifeTime();
    }
    public abstract void OnCollisionEnter2D(Collision2D collision);
    public abstract void EndOfLifeTime();
}
