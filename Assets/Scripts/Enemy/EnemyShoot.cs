using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletPrefab;
    public float totalBulletCooldown;
    float bulletCooldown;
    Transform bulletSpawn;
    GameObject player;
    Vector3 rotation;
    float rotZ;
    StatesSO octState;

    public static EnemyShoot instance;
    public Stack<GameObject> enemyStack;
    // Start is called before the first frame update
    void Start()
    {
        bulletCooldown = totalBulletCooldown;
        player = GameObject.FindWithTag("Player");
        bulletSpawn = transform;
    }
    private void Awake()
    {
        if (enemyStack == null)
        {
            enemyStack = new Stack<GameObject>();
        }
        instance = this;
    }
    private void Update()
    {
    }
    // Update is called once per frame
    public GameObject Pop()
    {
        GameObject obj = enemyStack.Pop();
        obj.SetActive(true);
        obj.transform.position = bulletSpawn.transform.position;
        return obj;
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        enemyStack.Push(obj);
    }
    public GameObject Peek()
    {
        return enemyStack.Peek();
    }
    public void Shoot()
    {
        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.deltaTime;
        }
        else
        {
            bulletCooldown = totalBulletCooldown;
            if (enemyStack.Count != 0)
            {
                Pop();
            }
            else
            {
                Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            }
        }
    }
}
