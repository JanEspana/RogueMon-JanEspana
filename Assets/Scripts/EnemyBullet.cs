using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : GenericBullet
{
    public GameObject thisOctillery;
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
        return thisOctillery;
    }
}