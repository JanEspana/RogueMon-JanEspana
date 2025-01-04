using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player;
    TextMeshProUGUI coinText;
    int coinAmount;
    private void Awake()
    {
        coinAmount = Random.Range(1, 5);
        player = GameObject.FindGameObjectWithTag("Player");
        coinText = GameObject.Find("CoinAmount").GetComponent<TextMeshProUGUI>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().coins += coinAmount;
            coinText.text = player.GetComponent<Player>().coins.ToString();
            Destroy(gameObject);
        }
    }
}
