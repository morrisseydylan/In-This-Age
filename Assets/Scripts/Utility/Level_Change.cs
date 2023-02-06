using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level_Change : MonoBehaviour
{
    public int levelIndex = 0;
    public bool useGameSceneManager = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //The scene number to load (in File->Build Settings)
            if (useGameSceneManager)
            {
                GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameSceneManager>().LoadScene(levelIndex);
            }
            else
            {
                SceneManager.LoadScene(levelIndex);
            }
        }
    }
}