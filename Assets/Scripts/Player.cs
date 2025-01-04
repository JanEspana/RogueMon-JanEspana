using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP;
    public GameObject lifeBar;
    public float maxHP = 10;
    public int weaponsObtained;
    public int coins;
    TextMeshProUGUI coinText;
    public GameObject blackOutScreen;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerValues();
        SetWeaponValues();
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
            GameOverScreen();
        }
    }
    void GameOverScreen()
    {
        blackOutScreen.SetActive(true);
    }
    public void SetPlayerValues()
    {
        transform.position = new Vector3(0, 0, 0);
        Camera.main.transform.position = new Vector3(0.5f, 0, -10);
        HP = maxHP;
        weaponsObtained = 0;
        coins = 0;
        coinText = GameObject.Find("CoinAmount").GetComponent<TextMeshProUGUI>();
        coinText.text = coins.ToString();
        lifeBar.GetComponent<UnityEngine.UI.Slider>().maxValue = maxHP;
        lifeBar.GetComponent<UnityEngine.UI.Slider>().value = HP;
        lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }
    public void SetWeaponValues()
    {
        Shoot shoot = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Shoot>();
        Flamethrower flamethrower = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Flamethrower>();
        shoot.isObtained = false;
        flamethrower.isObtained = false;
    }
}
