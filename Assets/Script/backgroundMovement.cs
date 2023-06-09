using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class backgroundMovement : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;
 
    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
 
        leftPosX = -(yScreenHalfSize * 2);
        rightPosX = yScreenHalfSize * 2 * backgrounds.Length;
    }

    void Update()
    {
       for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(0, -speed, 0) * Time.deltaTime;
 
            if(backgrounds[i].position.y < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x, nextPos.y + rightPosX, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}
