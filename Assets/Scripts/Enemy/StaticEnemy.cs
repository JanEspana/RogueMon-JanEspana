using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : EnemyController
{
    public Transform octillery;
    private void OnEnable()
    {
        enemyType = "Octillery";
        octillery = GetComponent<Transform>();
    }
    void Update()
    {
        currentState.OnStateUpdate(this);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GoToState<AttackState>();
            lifeBarFrame.GetComponent<SpriteRenderer>().enabled = true;
            lifeBar.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GoToState<IdleState>();
            lifeBarFrame.GetComponent<SpriteRenderer>().enabled = false;
            lifeBar.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
