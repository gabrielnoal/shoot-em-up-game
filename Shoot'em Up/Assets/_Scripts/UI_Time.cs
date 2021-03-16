using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour
{
    Text textComp;
    GameManager gm;

    void Start()
    {
        textComp = GetComponent<Text>();
        gm = GameManager.GetInstance();
    }

    void FixedUpdate()
    {   
        if (gm.gameState == GameManager.GameState.GAME)
        {   
            gm.timer += Time.deltaTime;
            textComp.text = $"Time: {gm.timer.ToString()}";
        }
    }
}
