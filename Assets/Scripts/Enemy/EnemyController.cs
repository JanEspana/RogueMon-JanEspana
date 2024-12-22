using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public string enemyType;
    public StatesSO currentState;
    public float HP, dmg;
    public GameObject player;
    public ChaseScript chase;
    public KaboomScript kaboom;
    public EnemyShoot shoot;

    public void Start()
    {
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
            GoToState<DieState>();
        }
    }
}