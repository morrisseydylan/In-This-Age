using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager temp = FindObjectOfType<GameManager>();
            if (temp != null)
            {
                temp.SetNewRespawnPlace(collision.gameObject);
            }
            else
            {
                Debug.Log("Checkpoint: ERROR no GameManager found!");
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager temp = FindObjectOfType<GameManager>();
            if (temp != null)
            {
                temp.SetNewRespawnPlace(collision.gameObject);
            }
            else
            {
                Debug.Log("Checkpoint: ERROR no GameManager found!");
            }
            Destroy(gameObject);
        }
    }


}
