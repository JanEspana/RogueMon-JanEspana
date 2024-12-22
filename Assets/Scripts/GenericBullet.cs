using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;
    public float lifeTime;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    public abstract void OnEnable();
    public abstract GameObject FindSpawnPoint();
    // Update is called once per frame
    public void Update()
    {
        EndOfLifeTime();
    }
    public abstract void OnCollisionEnter2D(Collision2D collision);
    public abstract void EndOfLifeTime();
}
