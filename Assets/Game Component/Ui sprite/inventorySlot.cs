using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventorySlot : MonoBehaviour, IPointerClickHandler
{
    public bool isFull;
    public GameObject pointer;
    public void Awake()
    {
        pointer.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // if the slot contains an item
        if (isFull)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            // reAssgin the pointer to this slot
            UIController.instance.resetAllPointer();
            setPointer();

            // swap the weapon for player to hold
            GameObject selectWeapon = Instantiate(Resources.Load("Prefabs/items/" + gameObject.name) as GameObject);
            GameObject player = GameObject.Find("Main Char");
            player.GetComponent<mainChar>().swapWeapon(selectWeapon);

            // set the main weapon in game manager
            GameManeger.instance.assignMainWeapon(gameObject.name, gameObject.GetComponent<Image>().sprite);
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
