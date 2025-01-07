using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaboomScript : MonoBehaviour
{
    EnemyController weezing;
    float dmg = 2.5f;
    public GameObject player;
    Animator anim;
    float animLength;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        weezing = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Explode()
    {
        anim.SetTrigger("Explode");
        animLength = anim.GetCurrentAnimatorStateInfo(0).length - 0.3f;
        StartCoroutine(WaitForAnimation());

    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animLength);
        player.GetComponent<Player>().TakeDamage(dmg);
        weezing.GoToState<DieState>();
    }
}
