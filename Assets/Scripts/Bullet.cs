using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericBullet
{
    public Vector3 mousePos;
    private Camera mainCam;

    // Update is called once per frame
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        lifeTime = 1.5f;
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