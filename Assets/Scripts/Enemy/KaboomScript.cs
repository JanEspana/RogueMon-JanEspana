using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaboomScript : MonoBehaviour
{
    EnemyController weezing;
    Animator anim;
    float animLength;
    // Start is called before the first frame update
    void Start()
    {
        weezing = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode()
    {
        anim.SetTrigger("Explode");
        animLength = anim.GetCurrentAnimatorStateInfo(0).length - 0.3f;
        //el 0,3 es para ajustar la animación
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animLength);
        weezing.GoToState<DieState>();
    }
}
