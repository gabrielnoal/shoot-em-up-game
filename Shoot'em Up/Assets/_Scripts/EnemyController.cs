using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SteerableBehaviour, IShooter, IDamageable
{
    public GameObject bullet;

    GameManager gm;


    private int life; 
    private int reward;

    void Start()
    {
        gm = GameManager.GetInstance();

        life = Random.Range(1, 3);
        reward = life * 300;
    }

    public void Shoot()
    {
       Instantiate(bullet, transform.position, Quaternion.identity);
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

    private void Update() {
        if (gm.gameState == GameManager.GameState.ENDGAME)
        {
            Destroy(gameObject);
        }
    }
}
