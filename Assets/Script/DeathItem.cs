using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spiked Ball"))
        {
            Logic logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
            if (logic != null)
            {
                logic.gameOver();
            }
        }
    }
}