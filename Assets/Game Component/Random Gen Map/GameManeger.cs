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

    //****************************** VARIABLE FOR MAP GEN AND GAME STATE TASK *****************************************************************************************************
    // enum is just a data class to store the scene name
    public SceneState sceneState;

    public bool isPaused;

    public int level = 1;

    //save the weapon the the player currently holding

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //save player health
    public float playerHealth = 0;




    //****************************** VARIABLE FOR INVENTORY TASK ***************************************************************************************************

    // the array of ui element for showing itemsData 

    // the list of object store in our inventory
    public List<itemsData> inventory = new List<itemsData>();


    public int souls;


    // we just want to store some data of the prefab not the hold thing
    public itemsData mainWeaponData = new itemsData();

    public bool inventoryFull = false;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
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
        mainWeaponData.itemName = "";
        isPaused = false;
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

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        // determines what to generate base on what scene we are in
        switch (sceneState)
        {

            case SceneState.beginScene:
                {
                    Debug.Log("back to beginning");

                }
                break;
            case SceneState.randomGenScene:
                {
                    if (level % 3 == 0)
                    {
                        SceneManager.LoadScene(0);
                        sceneState = SceneState.beginScene;
                    }
                    if (level == 5)
                    {
                        SceneManager.LoadScene(2);
                        sceneState = SceneState.bossScene;
                    }
                    initializeGame();
                    Debug.Log("on scene load " + level);
                    UIController.instance.displayLevel(level);
                    UIController.instance.displaySouls(souls);
                }
                break;
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // this function will generate map
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
            UIController.instance.showItems();

        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void absorbSouls(int amount)
    {
        souls += amount;
        Debug.Log("souls: " + souls);
        UIController.instance.displaySouls(souls);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void addToInventory(itemsData item)
    {
        if (inventory.Count < 4)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].isMainWeapon = false;
            }
            item.inventorySlot = inventory.Count;
            inventory.Add(item);
            Debug.Log("add to inventory: " + item.itemName + "at slot: " + item.inventorySlot);

            // re toggle the is mainWeapon in inventory
            inventoryFull = false;
        }
        else
        {
            Debug.Log("inventoryFull");
            inventoryFull = true;
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // this is for the main weapon item data so we can show it in a box in UI 
    public void assignMainWeapon(string weaponName, Sprite weaponSprite)
    {
        mainWeaponData.itemName = weaponName;
        mainWeaponData.itemIcon = weaponSprite;
        mainWeaponData.isMainWeapon = true;
        Debug.Log("main weapon is: " + mainWeaponData.itemName);
        // display it to UI
        UIController.instance.displayMainWeapon(mainWeaponData.itemIcon);
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    // change the main weapon in inventory not relate to the function above
    public void changeMainWeapon(int slotNumber)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].isMainWeapon = false;
        }
        inventory[slotNumber].isMainWeapon = true;

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    // drop the weapon in inventory
    public void dropItem(int slotNumber)
    {
        if (!inventory[slotNumber].isMainWeapon)
        {

            // CLONE THE PREFABS
            GameObject dropItem = Instantiate(Resources.Load("Prefabs/items/" + inventory[slotNumber].itemName) as GameObject);
            GameObject mainChar = GameObject.Find("Main Char");
            dropItem.transform.position = mainChar.transform.position + new Vector3(1, -1, 0);
            inventory.RemoveAt(slotNumber);
            inventoryFull = false;
        }


    }



    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void pauseGame()
    {
        Time.timeScale = 0;
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------


}
