using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public enum SceneType
    {
        MainMenuTesting,
        MapTest,
        VictoryTest,
        GameOverTest
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneType.MainMenuTesting.ToString(), LoadSceneMode.Single);
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadSceneAsync(SceneType.MapTest.ToString(), LoadSceneMode.Single);
    }

    public void LoadVictoryScreen()
    {
        SceneManager.LoadSceneAsync(SceneType.VictoryTest.ToString(), LoadSceneMode.Single);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadSceneAsync(SceneType.GameOverTest.ToString(), LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
