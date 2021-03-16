using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    GameManager gm;
    private void OnEnable()
    {
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    public void Comecar()
    {
        gm.changeState(GameManager.GameState.GAME);
    }
}
