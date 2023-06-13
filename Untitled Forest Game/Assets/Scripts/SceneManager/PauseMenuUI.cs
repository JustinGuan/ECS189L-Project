using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUIs;
    [SerializeField] private GameObject player;
    private ScenesManager sm;
    public static bool isPaused = false;

    void Start()
    {
        // Grab the ScenesManager script from empty gameobject Manager.
        sm = GameObject.Find("World Generator").GetComponent<ScenesManager>();
    }

    void Update()
    {
        // Pause and unpause the game when the player hits escape.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused, resume the game.
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Turn off the canvas displaying the pause menu.
        pauseMenuUIs.SetActive(false);
        player.GetComponent<ThirdPersonShooterController>().enabled = true;
        player.GetComponentInChildren<Float>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    private void Pause()
    {
        // Turn on the canvas displaying the pause menu.
        pauseMenuUIs.SetActive(true);
        player.GetComponent<ThirdPersonShooterController>().enabled = false;
        player.GetComponentInChildren<Float>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        sm.LoadMainMenu();
    }

    public void QuitGame()
    {
        sm.QuitGame();
    }
}
