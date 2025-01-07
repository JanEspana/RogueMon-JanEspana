using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour, InputController.IUIActions
{
    bool isPaused;
    public GameObject pauseUI, gameUI;
    public AudioSource audioSource;
    public AudioClip menuOpenSound;
    void Awake()
    {
        InputController inputController = new InputController();
        inputController.UI.SetCallbacks(this);
        inputController.UI.Enable();
        isPaused = false;

        pauseUI.SetActive(false);
        gameUI.SetActive(true);
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused)
        {
            audioSource.PlayOneShot(menuOpenSound);
            isPaused = true;
            pauseUI.SetActive(true);
            gameUI.SetActive(false);
            List<GameObject> enemies = DungeonGen.instance.enemyInstances;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            player.GetComponent<PlayerMovement>().enabled = false;
            weapon.GetComponent<WeaponManager>().enabled = false;
        }
        else if (context.performed && isPaused)
        {
            isPaused = false;
            pauseUI.SetActive(false);
            gameUI.SetActive(true);
            List<GameObject> enemies = DungeonGen.instance.enemyInstances;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().enabled = true;
            }
            player.GetComponent<PlayerMovement>().enabled = true;
            weapon.GetComponent<WeaponManager>().enabled = true;
        }
    }
}
