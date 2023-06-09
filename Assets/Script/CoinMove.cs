using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public PlayerMovement player;
    public float coinMoveSpeed = 1f;
    public float coinDeadZone = -10;

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
            transform.position = transform.position + (Vector3.down * coinMoveSpeed) * Time.deltaTime;
        }

        if(transform.position.y < coinDeadZone) {
            Destroy(gameObject);
        }
    }
}
