using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance = null;

    public Image mask;

    public Image reloadMask;

    public Text levelDisplay;

    public Text soulsAmount;

    public GameObject mainWeaponImage;

    public GameObject inventoryUi;


    public List<Image> itemsImage = new List<Image>();




    bool showInventory;

    float originalSize;
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        inventoryUi.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventoryFunc();
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------


    public void SetHealth(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void SetMana(float value)
    {
        reloadMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void displayLevel(int level)
    {
        levelDisplay.text = "FLOOR: " + level;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void displaySouls(int amount)
    {
        soulsAmount.text = amount.ToString();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void displayMainWeapon(Sprite weaponSprite)
    {
        mainWeaponImage.GetComponent<Image>().sprite = weaponSprite;
    }

    public void showItems()
    {
        List<itemsData> weapons = GameManeger.instance.inventory;

        // before we show the weapon we have to clear it first
        resetInventory();
        // loop through the inventory to show to UI
        for (int i = 0; i < weapons.Count; i++)
        {

            //------------------------------
            itemsImage[i].sprite = weapons[i].itemIcon;
            inventorySlot slot = itemsImage[i].GetComponent<inventorySlot>();
            slot.removePointer();
            slot.isFull = true;
            slot.changeName(weapons[i].itemName);
            slot.inventorySlotNumber = weapons[i].inventorySlot;
            // set color so it visible when show
            itemsImage[i].color = new Color(1, 1, 1, 1);
            Debug.Log("weapon: " + weapons[i].itemName + "main weapon: " + weapons[i].isMainWeapon);
            //set pointer if it the main weapon
            if (weapons[i].isMainWeapon)
            {
                slot.setPointer();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void showInventoryFunc()
    {
        showInventory = !showInventory;
        // pauseGame when showing inventory
        if (showInventory)
        {
            GameManeger.instance.pauseGame();
            GameManeger.instance.isPaused = true;
            showItems();
            SFXManager.instance.playButtonPress();
        }
        else
        {
            GameManeger.instance.resumeGame();
            GameManeger.instance.isPaused = false;
            resetInventory();
            SFXManager.instance.playCancleBTNSound();
        }

        inventoryUi.SetActive(showInventory);

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void resetAllPointer()
    {
        for (int i = 0; i < itemsImage.Count; i++)
        {
            inventorySlot slot = itemsImage[i].GetComponent<inventorySlot>();
            slot.removePointer();
        }
    }

    public void resetInventory()
    {
        resetAllPointer();

        // remove all sprite from inventory slot
        for (int i = 0; i < itemsImage.Count; i++)
        {
            inventorySlot slot = itemsImage[i].GetComponent<inventorySlot>();
            slot.removeSlot();
        }
    }
}
