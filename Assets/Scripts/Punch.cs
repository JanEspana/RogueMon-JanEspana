using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    public float lifeTime;
    public Transform transformObj;
    private GameObject spawnPoint;
    private GameObject player;
    private float playerForce;
    //direction of the player
    private Vector2 playerDir;

    float dmg = 3f;

    private Camera cam;
    private Vector2 mousePos;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        transformObj = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        force = 3f;
        lifeTime = 0.25f;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(spawnPoint.transform.right * force, ForceMode2D.Impulse);
        transform.rotation = transformObj.rotation;

    }
    // Update is called once per frame
    void Update()
    {
        EndOfLifeTime();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lifeTime = 0.5f;
        Melee.instance.Push(gameObject);
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(dmg);
        }
    }
    void EndOfLifeTime()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            lifeTime = 0.5f;
            Melee.instance.Push(gameObject);
        }
    }
}
