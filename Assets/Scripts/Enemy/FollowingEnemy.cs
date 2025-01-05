using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : EnemyController
{
    public AudioSource audioSource;
    public AudioClip cry;
    private void OnEnable()
    {
        enemyType = "Weezing";
    }
    void Update()
    {
        currentState.OnStateUpdate(this);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GoToState<ChaseState>();
            lifeBarFrame.GetComponent<SpriteRenderer>().enabled = true;
            lifeBar.GetComponent<SpriteRenderer>().enabled = true;
            audioSource.PlayOneShot(cry);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<AttackState>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<ChaseState>();
        }
    }
}
