using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public StatesSO currentState;
    public float HP, dmg;
    public GameObject player;
    public ChaseScript chase;
    public KaboomScript kaboom;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chase = GetComponent<ChaseScript>();
        kaboom = GetComponent<KaboomScript>();
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