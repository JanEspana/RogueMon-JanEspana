using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Awake()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Heal()
    {
        TextMeshProUGUI coinText = GameObject.Find("CoinAmount").GetComponent<TextMeshProUGUI>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject lifeBar = player.lifeBar;
        if (player.coins >= 10 && player.HP < player.maxHP)
        {
            player.coins -= 10;
            if (player.HP + 4 > player.maxHP)
            {
                player.HP = player.maxHP;
            }
            else
            {
                player.HP += 4;
            }
            coinText.text = player.coins.ToString();
            lifeBar.GetComponent<UnityEngine.UI.Slider>().value = player.HP;
            if (player.HP > player.maxHP / 2)
            {
                lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            }
            else if (player.HP <= player.maxHP / 2 && player.HP > player.maxHP / 4)
            {
                lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }
            else if (player.HP <= player.maxHP / 4)
            {
                lifeBar.GetComponent<UnityEngine.UI.Slider>().fillRect.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }
        }
    }
    public void UnlockClauncher()
    {
        TextMeshProUGUI coinText = GameObject.Find("CoinAmount").GetComponent<TextMeshProUGUI>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Shoot shoot = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Shoot>();
        if (player.coins >= 15 && !shoot.isObtained)
        {
            player.coins -= 15;
            shoot.isObtained = true;
            coinText.text = player.coins.ToString();
        }
    }
    public void UnlockMagby()
    {
        TextMeshProUGUI coinText = GameObject.Find("CoinAmount").GetComponent<TextMeshProUGUI>();
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Flamethrower flamethrower = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Flamethrower>();
        if (player.coins >= 25 && !flamethrower.isObtained)
        {
            player.coins -= 25;
            flamethrower.isObtained = true;
            coinText.text = player.coins.ToString();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}