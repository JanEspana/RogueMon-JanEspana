using UnityEngine;

public class StaticEnemy : EnemyController
{
    public Transform octillery;
    public AudioSource audioSource;
    public AudioClip cry;
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
}
