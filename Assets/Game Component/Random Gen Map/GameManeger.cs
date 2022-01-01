using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManeger : MonoBehaviour
{
    // make game manager singleton
    // singleton is very useful when come to save state between scene
    public static GameManeger instance = null;
    public BoardManeger boardScript;
    // Start is called before the first frame update

    public int level = 1;


    //save the weapon the the player currently holding
    public string mainWeapon;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

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
        // use this to not destroy the game manager when new scene is created
        DontDestroyOnLoad(gameObject);

        initializeGame();
        boardScript = GetComponent<BoardManeger>();

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        initializeGame();
        UIController.instance.displayLevel(level);

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // this function will generate map when game start
    public void initializeGame()
    {
        boardScript.mapGen(level);
    }
}
