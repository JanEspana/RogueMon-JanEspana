using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericBullet
{
    public Vector3 mousePos;
    float dmg = 2f;
    private Camera mainCam;

    public override void OnEnable()
    {
        force = 0.5f;
        lifeTime = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = FindSpawnPoint();
        rb.AddForce(spawnPoint.transform.right * force, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        lifeTime = 1.5f;
        collision.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
        Shoot.instance.Push(gameObject);
    }

    public override void EndOfLifeTime()
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
    public override GameObject FindSpawnPoint()
    {
        return GameObject.FindGameObjectWithTag("SpawnPoint");
    }
}