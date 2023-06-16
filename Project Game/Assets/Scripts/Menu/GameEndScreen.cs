using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
