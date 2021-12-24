using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    // make game manager singleton
    public static GameManeger instance = null;
    public BoardManeger boardScript;
    // Start is called before the first frame update

    public int level = 3;

    void Awake()
    {

        // create an instance of the game manager when awake
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        boardScript = GetComponent<BoardManeger>();

        initializeGame();
    }

    // this function will generate map when game start
    void initializeGame()
    {
        boardScript.mapGen(level);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
