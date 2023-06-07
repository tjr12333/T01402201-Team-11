using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �÷��̾�� �浹�ϸ� ���� ���� */
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    /* ���� �������� ���� ���� */
public class CoinGenerator : MonoBehaviour
    {
        public GameObject coinPrefab;
        public float spawnDelay = 2f; /* ���� ���� ���� ���� */
        public float spawnRadius = 5f;

        private float nextSpawnTime;

        private void Start()
        {
            netSpawnTime = nextSpawnTime.time + spawnRate;
        }

        private void Update()
        {
            if(Time.time >= nextSpawnTime)
            {
                SpawnCoin();
                nextSpawnTime = Time.time + spawnRate;
            }
        }

        private void SpawnCoin()
        {
            Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0f);

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}