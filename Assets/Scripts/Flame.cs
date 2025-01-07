using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private Vector3 mouse, rotation;
    float rotZ;
    private GameObject spawnPoint;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotation =  mouse - spawnPoint.transform.position;
        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg+180;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ+90);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 6)
        {
            float dmg = Random.Range(0.05f, 0.2f);
            other.GetComponent<EnemyController>().TakeDamage(dmg);
        }
    }
}
