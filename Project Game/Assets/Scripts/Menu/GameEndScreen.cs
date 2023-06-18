using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    public GameObject gameEndScreen;
    
    private void OnEnable()
    {
        PlayerMovement.OnGameEnd += EnableGameEndScreen;
    }

    private void OnDisable()
    {
        PlayerMovement.OnGameEnd -= EnableGameEndScreen;
    }

    public void EnableGameEndScreen()
    {
        gameEndScreen.SetActive(true);
    }

    public void RestartGame()
    {
        // Restart game
        Debug.Log("Restarting Game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMenu()
    {
        // Load menu
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
