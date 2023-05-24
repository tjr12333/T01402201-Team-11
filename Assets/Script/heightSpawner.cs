using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class heightSpawner : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject textPrefab;
    public GameObject canvas;
    public float spawnRate = 2;
    private float timer = 0;
    public int height_num = 0;
    

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
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }else{
                Debug.Log(height_num);
                SpawnText();
                timer = 0;
            }
        }
        
    }

    public void SpawnText()
    {
        GameObject textInstance = Instantiate(textPrefab, canvas.transform);
        TextMeshProUGUI textComponent = textInstance.GetComponent<TextMeshProUGUI>();
        height_num += 100;
        textComponent.text = "---" + height_num.ToString() + "m---";
    }
}
