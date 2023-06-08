using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public PlayerMovement player;
    public float platformMoveSpeed = 1f;
    public float deadZone = -5;

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
            transform.position = transform.position + (Vector3.down * platformMoveSpeed) * Time.deltaTime;
        }

        if(transform.position.y < deadZone) {
            Destroy(gameObject);
        }
    }
}

