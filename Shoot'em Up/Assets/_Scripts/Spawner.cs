using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameManager gm;
    public GameObject rock;
    public GameObject enemy;
    public GameObject player;

    public Boudary spawnBoudary;

    private int maxRockCount = 10;

    public float spawnDelay = 1.0f;
    private float lastSpawnTimestamp = 0.0f;
    private bool startEnemySpawn;

    void Start()
    {
        gm = GameManager.GetInstance();
        startEnemySpawn = true;
    }

    IEnumerator SpawnWave() {
        if (gm.gameState == GameManager.GameState.GAME) {
            startEnemySpawn = false;
            int score = gm.score;
            for (int i = 0; i < (score <= 100 ? 1 : score/100); i++)
            {
                Vector3 rockPosition = new Vector3(transform.position.x, Random.Range(spawnBoudary.min.y, spawnBoudary.max.y));
                if (Time.time - lastSpawnTimestamp < spawnDelay) yield return new WaitForSeconds(1);
                lastSpawnTimestamp = Time.time;
                if (transform.childCount >= maxRockCount) yield return new WaitUntil(() => transform.childCount < maxRockCount);
                Instantiate(rock, rockPosition, Quaternion.identity, transform);
                if (i % 10 == 0)
                {
                    for (int j = 0; j < score/10; j++) {
                        Vector3 enemyPosition = new Vector3(transform.position.x, Random.Range(spawnBoudary.min.y, spawnBoudary.max.y));
                        Instantiate(enemy, enemyPosition, Quaternion.identity, transform);
                        yield return new WaitForSeconds(1);
                    }
                }
            }
            startEnemySpawn = true;
        } else if (gm.gameState == GameManager.GameState.ENDGAME)
        {   
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    private void FixedUpdate() {
        if (gm.gameState == GameManager.GameState.GAME && gm.spawn == true)
        {
            gm.spawn = false;
            Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity, transform);
        }
        if (startEnemySpawn)
        {
            StartCoroutine(SpawnWave());
        }
    }

}
