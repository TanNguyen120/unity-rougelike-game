using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    int currenLevel;
    GameManeger gameManeger;

    void Start()
    {
        gameManeger = GameManeger.instance;
        currenLevel = gameManeger.level;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if the player is enter this increse level and reloaded scenes
        if (other.gameObject.tag == "Player")
        {
            int nextLevel = currenLevel + 1;
            gameManeger.level = nextLevel;
            Debug.Log("next level: " + nextLevel);
            gameManeger.sceneState = SceneState.randomGenScene;
            SceneManager.LoadScene("randomMap");
        }

    }
}
