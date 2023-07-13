using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public enum SceneType
    {
        MAINSCENE,
        Opening,
        Map,
        VictoryTest,
        GameOverTest,
        BossCutscene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneType.MAINSCENE.ToString(), LoadSceneMode.Single);
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadSceneAsync(SceneType.Map.ToString(), LoadSceneMode.Single);
    }

    public void LoadVictoryScreen()
    {
        SceneManager.LoadSceneAsync(SceneType.VictoryTest.ToString(), LoadSceneMode.Single);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadSceneAsync(SceneType.GameOverTest.ToString(), LoadSceneMode.Single);
    }

    public void LoadBossCutscene()
    {
        SceneManager.LoadSceneAsync(SceneType.BossCutscene.ToString(), LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
