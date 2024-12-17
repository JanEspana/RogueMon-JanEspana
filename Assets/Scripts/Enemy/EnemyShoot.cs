using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletPrefab;
    float bulletCooldown = 3;
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
        player = GameObject.FindWithTag("Player");
        enemyStack = new Stack<GameObject>();
        instance = this;
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 rotation = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
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
        if (enemyStack.Count != 0)
        {
            Pop();
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
}
