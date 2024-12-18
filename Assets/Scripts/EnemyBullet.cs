using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : GenericBullet
{
    public GameObject player;
    public Vector2 dir;
    public override void OnEnable()
    {
        force = 3f;
        lifeTime = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * force;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Octillery"))
        {
            lifeTime = 1.5f;
            EnemyShoot.instance.Push(gameObject);
        }
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
            EnemyShoot.instance.Push(gameObject);
        }
    }
    public override GameObject FindSpawnPoint()
    {
        return gameObject;
    }
}