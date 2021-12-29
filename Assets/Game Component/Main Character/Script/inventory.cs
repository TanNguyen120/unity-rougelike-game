using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    public GameObject inventoryUi;
    // the array of items in the inventory
    public GameObject[] slots;

    // the flag determines whether the inventory is full or not
    public bool[] filled;
    private static int souls;

    private bool showInventory = false;

    private void Start()
    {
        UIController.instance.displaySouls(souls);
        inventoryUi.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;
            inventoryUi.SetActive(showInventory);
        }
    }
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
}
