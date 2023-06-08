using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float moveSpeed = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float deadZone =- 45;
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;

        if(transform.position.y < deadZone) {
            Destroy(gameObject);
        }
    }
}

