using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBehaviour : SteerableBehaviour
{
    Vector3 posPlayer;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        posPlayer = GameObject.FindWithTag("Player").transform.position;
        direction = (posPlayer - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Thrust(direction.x, direction.y);
        Vector2 posicaoViewport = Camera.main.WorldToViewportPoint(transform.position);
        if (posicaoViewport.x > 1 ||
            posicaoViewport.x < 0 || 
            posicaoViewport.y > 1 ||
            posicaoViewport.y < 0
        ) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) return;
        
        IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if (!(damageable is null))
        {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible() {
        gameObject.SetActive(false);
    }
}
