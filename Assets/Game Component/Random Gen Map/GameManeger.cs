using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManeger : MonoBehaviour
{
    // make game manager singleton
    public static GameManeger instance = null;
    public BoardManeger boardScript;
    // Start is called before the first frame update

    public int level = 1;

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

        initializeGame();

        // use this to not destroy the game manager when new scene is created
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManeger>();

    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        initializeGame();

    }

    // this function will generate map when game start
    public void initializeGame()
    {
        boardScript.mapGen(level);
        UIController.instance.displayLevel(level);
    }
}
