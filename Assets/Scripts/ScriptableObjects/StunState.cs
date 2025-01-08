using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunState", menuName = "States/StunState")]
public class StunState : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        ec.StartCoroutine(EndStun(ec));
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
    }
    public IEnumerator EndStun(EnemyController ec)
    {
        yield return new WaitForSeconds(0.5f);
        ec.rb.velocity = Vector2.zero;
        ec.GoToState<ChaseState>();
    }
}
