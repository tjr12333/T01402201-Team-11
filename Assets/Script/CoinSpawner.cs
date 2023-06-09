using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;
    [SerializeField]public float spawnRate = 3f;
    private float timer = 0f;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spawnCoin();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameIsActive) {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            } else {
                spawnCoin();
                timer = 0f;
            }
        }
    }

    public float heightOffset = 5f;
    public float widthOffset = 8.3f;

    void spawnCoin()
    {
        float leftPoint = transform.position.x - widthOffset;
        float rightPoint = transform.position.x + widthOffset;
        float lowestPoint = transform.position.y - heightOffset/2;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(coin, new Vector3(Random.Range(leftPoint, rightPoint), Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
