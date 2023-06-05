using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour
{
    public PlayerMovement player;
    public float speed = 1f; // 텍스트 이동 속도 (조정 가능)
    private float deadZone = -10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // 텍스트를 아래로 이동
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
