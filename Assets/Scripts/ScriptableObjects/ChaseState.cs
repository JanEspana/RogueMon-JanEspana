using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "States/ChaseState")]
public class ChaseState : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        Debug.Log("Chase State");
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
    }
}
