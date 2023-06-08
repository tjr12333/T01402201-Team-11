using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;
    [SerializeField]public float spawnRate = 5;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        spawnCoin();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        } else {
            spawnCoin();
            timer = 0;
        }
    }

    public float heightOffset = 5;
    public float widthOffset = 9;

    void spawnCoin()
    {
        float leftPoint = transform.position.x - widthOffset;
        float rightPoint = transform.position.x + widthOffset;
        // float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(coin, new Vector3(Random.Range(leftPoint, rightPoint), Random.Range(transform.position.y, highestPoint), 0), transform.rotation);
    }
}
