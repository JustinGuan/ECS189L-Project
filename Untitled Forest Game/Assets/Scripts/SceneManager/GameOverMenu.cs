using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private ScenesManager sm;
    [SerializeField] private GameObject gameOverMenuUIs;

    private void Start()
    {
        sm = GetComponent<ScenesManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReplayGame()
    {
        sm.LoadGameLevel();
    }

    public void LoadMenu()
    {
        sm.LoadMainMenu();
    }

    public void Quit()
    {
        sm.QuitGame();
    }
}
