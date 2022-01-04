using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneState { beginScene, randomGenScene, bossScene }
public class GameManeger : MonoBehaviour
{
    // make game manager singleton
    // singleton is very useful when come to save state between scene
    public static GameManeger instance = null;
    public BoardManeger boardScript;
    // enum is just a data class to store the scene name
    public SceneState sceneState;


    public int level = 1;

    //save the weapon the the player currently holding

    //save player health
    public float playerHealth = 0;
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // prefab name of currently held weapon

    public GameObject inventoryUi;
    // the array of items in the inventory
    public GameObject[] slots;

    // the flag determines whether the inventory is full or not
    public bool[] filled;
    private static int souls;

    private bool showInventory = false;

    public itemsData mainWeaponData = new itemsData();

    bool oneTimeUpdate = true;




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
        boardScript = GetComponent<BoardManeger>();


    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (sceneState == SceneState.randomGenScene && level == 1)
        {
            initializeGame();
            Debug.Log("awake gen:" + level);
        }
        UIController.instance.displaySouls(souls);
        inventoryUi.SetActive(false);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        switch (sceneState)
        {

            case SceneState.beginScene:
                {
                    Debug.Log("back to beginning");

                }
                break;
            case SceneState.randomGenScene:
                {
                    initializeGame();
                    Debug.Log("on scene load " + level);
                    UIController.instance.displayLevel(level);
                    UIController.instance.displaySouls(souls);
                }
                break;
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // this function will generate map when game start
    public void initializeGame()
    {
        boardScript.mapGen(level);
    }




    private void Update()
    {
        if (oneTimeUpdate)
        {
            oneTimeUpdate = false;
            UIController.instance.displayMainWeapon(mainWeaponData.itemIcon);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;
            inventoryUi.SetActive(showInventory);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void absorbSouls(int amount)
    {
        souls += amount;
        Debug.Log("souls: " + souls);
        UIController.instance.displaySouls(souls);
    }

    public void addToInventory(GameObject obj)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!filled[i])
            {
                slots[i] = obj;
                return;
            }
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void assignMainWeapon(string weaponName, Sprite weaponSprite)
    {
        mainWeaponData.itemName = weaponName;
        mainWeaponData.itemIcon = weaponSprite;
        // display it to UI
        UIController.instance.displayMainWeapon(mainWeaponData.itemIcon);

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

}
