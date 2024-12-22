using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : GenericBullet
{
    public GameObject player;
    public Vector3 dir;
    public override void OnEnable()
    {

        force = 0.3f;
        lifeTime = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        spawnPoint = FindSpawnPoint();
        dir = (player.transform.position - spawnPoint.transform.position).normalized;
        rb.AddForce(dir * force, ForceMode2D.Impulse);
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

    IEnumerator TryAgain()
    {
        yield return new WaitForSeconds(0.1f);
        OnEnable();
    }
}