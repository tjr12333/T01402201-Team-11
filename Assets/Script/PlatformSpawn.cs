using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject platform; // 파이프 게임 오브젝트
    public GameObject platform1;
    public float spawnRate = 5; // 파이프 생성 속도
    private float timer = 0; // 타이머 변수
    private float allTime = 0.0f;

    float[] values = {4, 9, -4, -8, 0};
    float[] values1 = {-7, -3, 0, 3, 7};
    float randomValue;
    float previousValue;  //   이전에 선택된 값 저장 변수

    // Start 함수는 첫 번째 프레임 이전에 실행됩니다.
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spawnPlatform(); // 파이프 생성 함수 호출
    }

    // Update 함수는 매 프레임마다 호출됩니다.
    void Update()
    {
        if(player.gameIsActive)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime; // 타이머 증가
            }
            else
            {
                if(allTime / 60f < 1.1){
                    spawnPlatform(); // 파이프 생성 함수 호출
                }else{
                    spawnPlatform1();
                }
                
                timer = 0; // 타이머 초기화
            }
            allTime += Time.deltaTime;
        }
    }

    public float offset = 5; // 파이프 좌우 오프셋 변수

    // 파이프 생성 함수
    void spawnPlatform()
    {
        // float leftPoint = this.transform.position.x - offset; // 파이프의 왼쪽 끝점
        // float rightPoint = this.transform.position.x + offset; // 파이프의 오른쪽 끝점
        
        while(true)
        {
            randomValue = values[Random.Range(0, values.Length)];
            if(randomValue != previousValue){
                break;
            }
        }

        previousValue = randomValue;

        // 파이프를 생성하는 Instantiate 함수를 호출하여 파이프를 생성합니다.

        // 회전은 현재 오브젝트의 회전과 동일합니다.
        //Instantiate(platform, new Vector3(Random.Range(leftPoint, rightPoint),transform.position.y,0), transform.rotation);
        Instantiate(platform, new Vector3(transform.position.x + previousValue,transform.position.y,0), transform.rotation);
    }

    void spawnPlatform1()
    {
        while(true)
        {
            randomValue = values1[Random.Range(0, values.Length)];
            if(randomValue != previousValue){
                break;
            }
        }

        previousValue = randomValue;

        Instantiate(platform1, new Vector3(transform.position.x + previousValue,transform.position.y+3,0), transform.rotation);
    }
}


