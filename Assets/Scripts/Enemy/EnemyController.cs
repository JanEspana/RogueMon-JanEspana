using System;
using TMPro;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public string enemyType;
    public Rigidbody2D rb;
    public GameObject lifeBar, lifeBarFrame;
    public StatesSO currentState;
    public float HP;
    public GameObject player;
    public ChaseScript chase;
    public KaboomScript kaboom;
    public EnemyShoot shoot;
    public GameObject coin;
    TextMeshProUGUI scoreText;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeBarFrame = transform.GetChild(0).gameObject;
        lifeBar = transform.GetChild(1).gameObject;
        lifeBarFrame.GetComponent<SpriteRenderer>().enabled = false;
        lifeBar.GetComponent<SpriteRenderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        chase = GetComponent<ChaseScript>();
        switch (enemyType)
        {
            case "Weezing":
                kaboom = GetComponent<KaboomScript>();
                break;
            case "Octillery":
                shoot = GetComponent<EnemyShoot>();
                break;
        }
    }
    public void GoToState<T>() where T : StatesSO
    {
        if (currentState.statesToGo.Find(state => state is T))
        {
            currentState.OnStateExit(this);
            currentState = currentState.statesToGo.Find(state => state is T);
            currentState.OnStateEnter(this);
        }
    }
    public void CheckIfAlive()
    {
        if (HP <= 0)
        {
            DropCoin();
            GoToState<DieState>();
        }
    }

    internal void TakeDamage(float dmg)
    {
        HP -= dmg;
        lifeBar.transform.localScale = new Vector3(HP / 40, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        lifeBar.transform.localPosition = new Vector3(-lifeBarFrame.transform.localScale.x / 2 + lifeBar.transform.localScale.x / 2, lifeBar.transform.localPosition.y, lifeBar.transform.localPosition.z);
        CheckIfAlive();
    }
    void DropCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }
}