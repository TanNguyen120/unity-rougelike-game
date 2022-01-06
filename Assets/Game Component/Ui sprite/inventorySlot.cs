using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventorySlot : MonoBehaviour, IPointerClickHandler
{
    public bool isFull;
    public GameObject pointer;


    public int inventorySlotNumber;
    public void Awake()
    {
        pointer.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // if player left clicked
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // if the slot contains an item
            if (isFull)
            {
                Debug.Log("Select: " + eventData.pointerCurrentRaycast.gameObject.name + " in inventory slot");
                // reAssgin the pointer to this slot
                UIController.instance.resetAllPointer();
                setPointer();

                // swap the weapon for player to hold
                GameObject selectWeapon = Instantiate(Resources.Load("Prefabs/items/" + gameObject.name) as GameObject);
                GameObject player = GameObject.Find("Main Char");
                player.GetComponent<mainChar>().changeMainWeapon(selectWeapon);

                // change the item data inventory
                GameManeger.instance.changeMainWeapon(inventorySlotNumber);

                // set the main weapon in game manager
                GameManeger.instance.assignMainWeapon(gameObject.name, gameObject.GetComponent<Image>().sprite);
            }

        }
        else if (eventData.button == PointerEventData.InputButton.Right)    // if the player right clicked
        {
            if (isFull)
            {
                Debug.Log("drop:" + eventData.pointerCurrentRaycast.gameObject.name + "in inventory");
                if (GameManeger.instance.inventory[inventorySlotNumber].isMainWeapon)
                {
                    Debug.Log("cant drop main weapon");
                }
                else
                {
                    Debug.Log("drop at slot: " + inventorySlotNumber);
                }
            }
        }
    }

    public void setPointer()
    {
        Debug.Log("set pointer to true");
        pointer.SetActive(true);
    }

    public void removePointer()
    {
        pointer.SetActive(false);
    }

    public void changeName(string name)
    {
        gameObject.name = name;
    }
}
