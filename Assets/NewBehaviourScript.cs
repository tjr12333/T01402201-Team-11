using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject goldCoin;
    public GameObject coinSet;

    public float productionTime;
    void Start()
    {
        StartCoroutine("CoinPro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CoinPro()
    {
        Coin();
        yield return new WaitForSeconds(productionTime);
        StartCoroutine("CoinPro");
    }
    void Coin()
    {
        GameObject coin = Instantiate(goldCoin) as GameObject;
        coin.transform.parent = coinSet.transform;
        coin.transform.position = new Vector3(transform.position.x, Random.Range(-3.9f, 3.9f), 0);
        //coin.GetComponent<CoinMove>().coinSpeed = Random.Range(40, 100);
    }
}
