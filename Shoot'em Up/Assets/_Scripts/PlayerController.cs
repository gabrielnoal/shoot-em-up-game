using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    GameManager gm;
    Animator animator;

  
    public GameObject bullet;
    public Transform gun;
    public AudioClip shootSFX;
    public Boudary screenBoudary;
    public float shootDelay = 1.0f;

    private float lastShootTimestamp = 0.0f;
    private int life;


    private void Start() {
        gm = GameManager.GetInstance();
        animator = GetComponent<Animator>();
        life = gm.life;
    }

    public void Shoot()
    {
        if (Time.time - lastShootTimestamp < shootDelay) return;

       lastShootTimestamp = Time.time;
       Instantiate(bullet, gun.position, Quaternion.identity);
    }

    public void TakeDamage()
    {
       life--;
       gm.life--;
       if (life <= 0) Die();
    }

    public void Die()
    {
        gm.changeState(GameManager.GameState.ENDGAME);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (gm.gameState == GameManager.GameState.GAME) {
            float yInput = Input.GetAxis("Vertical");
            float xInput = Input.GetAxis("Horizontal");
            Vector2 playerPosition = transform.position;


            if (playerPosition.x > screenBoudary.max.x)
            {
                Thrust(-1, yInput);
            }
            else if (playerPosition.x < screenBoudary.min.x)
            {
                Thrust(1, yInput);
            }
            else if (playerPosition.y > screenBoudary.max.y)
            {
                Thrust(xInput, -1);
            }
            else if (playerPosition.y < screenBoudary.min.y)
            {
                Thrust(xInput, 1);
            }
            else {
                Thrust(xInput, yInput);
            }

            if (yInput != 0 || xInput != 0)
            {
                animator.SetFloat("Velocity", 1.0f);
            } else {
                animator.SetFloat("Velocity", 0f);
            }

            if(Input.GetAxisRaw("Jump") != 0)
            {
            Shoot();
                AudioManager.PlaySFX(shootSFX);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) return;

        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
        }
        else if (collision.CompareTag("Rock") || collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    private void Update() {
        if (gm.gameState == GameManager.GameState.GAME) {
            if (gm.score >= gm.scoreLimit)
            {
                gm.score = gm.scoreLimit;
                Destroy(gameObject);
                gm.changeState(GameManager.GameState.ENDGAME);
            }
        }
    }

    
}
