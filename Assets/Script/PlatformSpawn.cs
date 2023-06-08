using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platform; // 파이프 게임 오브젝트
    public float spawnRate = 5; // 파이프 생성 속도
    private float timer = 0; // 타이머 변수

    // Start 함수는 첫 번째 프레임 이전에 실행됩니다.
    void Start()
    {
        spawnPlatform(); // 파이프 생성 함수 호출
    }

    // Update 함수는 매 프레임마다 호출됩니다.
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime; // 타이머 증가
        }
        else
        {
            spawnPlatform(); // 파이프 생성 함수 호출
            timer = 0; // 타이머 초기화
        }
    }

    public float offset = 5; // 파이프 좌우 오프셋 변수

    // 파이프 생성 함수
    void spawnPlatform()
    {
        float leftPoint = transform.position.x - offset; // 파이프의 왼쪽 끝점
        float rightPoint = transform.position.x + offset; // 파이프의 오른쪽 끝점

        // 파이프를 생성하는 Instantiate 함수를 호출하여 파이프를 생성합니다.

        // 회전은 현재 오브젝트의 회전과 동일합니다.
        Instantiate(platform, new Vector3(Random.Range(leftPoint, rightPoint),0), transform.rotation);
    }
}


