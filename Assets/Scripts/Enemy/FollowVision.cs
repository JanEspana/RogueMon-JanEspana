using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVision : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public float xDir, yDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        anim.SetFloat("Horizontal", xDir);
        anim.SetFloat("Vertical", yDir);
    }
    void LookAtPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        xDir = direction.x;
        yDir = direction.y;
    }
}
