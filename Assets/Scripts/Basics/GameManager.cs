using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static Action OnGameStart;
    public static Action OnGameWin;
    public static Action OnGameLose;

    public void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void StartGame()
    {
        OnGameStart?.Invoke();
        //Elephant level started
    }

    public void WinGame()
    {
        OnGameWin?.Invoke();
        //Elephant level finished
    }

    public void LoseGame()
    {
        OnGameLose?.Invoke();
        //Elephant level failed
    }
}