using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    public Text message;
    public Text timeScore;
    GameManager gm;
    void OnEnable()
    {
        gm = GameManager.GetInstance();
        if(gm.score >= gm.scoreLimit)
        {
            message.text = "WIN";
            timeScore.text = $"Time: {gm.timer}s";
        }
        else 
        {
            message.text = "GAME OVER";
            timeScore.text = $"You lost in {gm.timer}s";
        }
    }

    public void Voltar()
    {
        gm.changeState(GameManager.GameState.MENU);
    }
}
