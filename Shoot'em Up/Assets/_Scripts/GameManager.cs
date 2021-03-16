using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager
{
    public enum GameState { MENU, GAME, ENDGAME };

    public GameState gameState { get; private set; }
    public int life;
    public int score;
    public float timer;
    public bool spawn;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;
    
    private static GameManager _instance;
    public int scoreLimit { get; private set; }

    private GameManager()
    {
        life = 3;
        score = 0;
        timer = 0.0f;
        gameState = GameState.MENU;
        scoreLimit = 10000;
    }

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }

        return _instance;
    }

    public void changeState(GameState nextState)
    {
        if (nextState == GameState.MENU) Reset();
        if (nextState == GameState.GAME && gameState == GameState.MENU) {
            spawn = true;
        }
        gameState = nextState;
        changeStateDelegate();
    }

    public void Reset()
    {
        life = 3;
        score = 0;
        timer = 0.0f;
        score = 0;
    }
}