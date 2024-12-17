using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "States/AttackState")]
public class AttackState : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        Debug.Log("Attack State");
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        switch (ec.enemyType)
        {
            case "Weezing":
                ec.kaboom.Explode();
                break;
            case "Octillery":
                ec.shoot.Shoot();
                break;
        }
    }
}
