using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    public PlayerMovement player;
    public float speed = 1f;
    private float deadZone = -20;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.gameIsActive)
        {
            transform.position = transform.position + (Vector3.down * speed) * Time.deltaTime;
        }

        if(transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
