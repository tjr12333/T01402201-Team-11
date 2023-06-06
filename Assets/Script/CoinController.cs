using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private GameObject coinEffectPrefab;
    private float rotateSpeed = 100.0f;

    public Logic logic;

    private void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update()
    {
        // 코인 오브젝트 회전
        transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collisoin) {
        // 코인 오브젝트 획득 효과 (coinEffectPrefabs) 생성
        // GameObject clone = Instantiate(coinEffectPrefab);
        // clone.transform.position = transform.position;
        
        // 점수 획득
        if (collisoin.gameObject.layer == 7) {
            logic.addScore(1);
        }

        // 코인 오브젝트 삭제
        Destroy(gameObject);
        
    }
}
