using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP;
    public GameObject lifeBar;
    public float maxHP = 10;
    public int weaponsObtained;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        weaponsObtained = 0;
        score = 0;
        lifeBar.GetComponent<UnityEngine.UI.Slider>().maxValue = maxHP;
        lifeBar.GetComponent<UnityEngine.UI.Slider>().value = HP;
        lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;

        lifeBar.GetComponent<UnityEngine.UI.Slider>().value = HP;

        if (HP <= maxHP / 2 && HP > maxHP / 4)
        {
            lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        }
        else if (HP <= maxHP / 4)
        {
            lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        
        
        if (HP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("U dumbass nigga");
        transform.position = new Vector3(0, 0, 0);
        Start();
    }
}
