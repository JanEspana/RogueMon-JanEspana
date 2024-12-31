using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float hp;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hp = gameObject.GetComponentInParent<EnemyController>().HP / 40;
        //change the X scale of the life bar. The other values are not changed
        transform.localScale = new Vector3(hp, 0.175f, 1);
    }
}
