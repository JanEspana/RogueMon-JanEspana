using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    Player player;
    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }
    IEnumerator AnimateText()
    {
        for (int i = 0; i < gameOverText.text.Length; i++)
        {
            gameOverText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Restart()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.SetPlayerValues();
        player.SetWeaponValues();
        DungeonGen.instance.ResetDungeon();
        gameObject.SetActive(false);
    }
}
