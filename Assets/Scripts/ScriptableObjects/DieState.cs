using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DieState", menuName = "States/DieState")]
public class DieState : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        TextMeshProUGUI scoreText = GameObject.Find("ScoreAmount").GetComponent<TextMeshProUGUI>();
        Debug.Log("Die State");
        DungeonGen.instance.enemyInstances.Remove(ec.gameObject);
        player.score += 100;
        scoreText.text = player.score.ToString();
        Destroy(ec.gameObject);
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
    }
}