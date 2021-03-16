using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : SteerableBehaviour, IDamageable
{

    GameManager gm;

    public float minScale =  0.5f;
    public float maxScale = 1;
    
    private int life; 
    private int reward;
    
    void Start()
    {
        gm = GameManager.GetInstance();

        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1);
        life = Random.Range(1, 3);
        reward = life * 100;
    }

    public void TakeDamage()
    {
        life--;
        if (life <= 0) Die();
    }

    public void Die()
    {
        gm.score += reward;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Thrust(-1, 0);
        Vector2 posicaoViewport = Camera.main.WorldToViewportPoint(transform.position);
        if (posicaoViewport.x < 0) Destroy(gameObject);
    }
}
