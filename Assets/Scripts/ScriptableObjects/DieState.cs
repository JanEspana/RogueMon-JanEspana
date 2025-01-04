using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieState", menuName = "States/DieState")]
public class DieState : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        Debug.Log("Die State");
        DungeonGen.instance.enemyInstances.Remove(ec.gameObject);
        Destroy(ec.gameObject);
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
    }
}