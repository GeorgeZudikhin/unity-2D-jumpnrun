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
        DisableCanvasWithTag("PlayerUI");
    }

    public void RestartGame()
    {
        // Restart game
        Debug.Log("Restarting Game...");
        //EnableCanvasWithTag("PlayerUI");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMenu()
    {
        // Load menu
        Debug.Log("Loading menu...");
        //EnableCanvasWithTag("PlayerUI");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void DisableCanvasWithTag(string tag) 
    {
        GameObject obj = GameObject.FindWithTag(tag);
        
        if (obj != null) 
        {
            Canvas canvas = obj.GetComponent<Canvas>();
            if (canvas != null) 
            {
                canvas.enabled = false;
            }
        }
    }

    void EnableCanvasWithTag(string tag) 
    {
        GameObject obj = GameObject.FindWithTag(tag);
        
        if (obj != null) 
        {
            Canvas canvas = obj.GetComponent<Canvas>();
            if (canvas != null) 
            {
                canvas.enabled = true;
            }
        }
    }
}
