using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopSlotScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject priceTag;

    public float itemPrice;

    public string itemToolTip;


    public void OnPointerClick(PointerEventData eventData)
    {
        SFXManager.instance.playButtonPress();
        Debug.Log("Buy items");
        float playerSouls = GameManeger.instance.souls;

        // check if player can afford to buy the item
        if (playerSouls < itemPrice)
        {
            GameObject warningText = GameObject.Find("warning");
            warningText.GetComponent<Text>().text = "kill some more monster to absort their soul";
        }
        else if (GameManeger.instance.inventoryFull)
        {
            Debug.Log("inventory is full");
            GameObject warningText = GameObject.Find("warning");
            warningText.GetComponent<Text>().text = "your inventory is full drop some items";
        }
        else
        {
            // create an item data and add to inventory
            string buyItemName = gameObject.name;
            Sprite buyItemSprite = gameObject.GetComponent<Image>().sprite;
            itemsData buyItemData = new itemsData { itemName = buyItemName, itemIcon = buyItemSprite };
            GameManeger.instance.addToInventory(buyItemData);

            // spend souls
            GameManeger.instance.souls -= (int)itemPrice;
        }
    }


    // show tooltip when object enter the slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Over" + gameObject.name);
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = Input.mousePosition;

        //find the tooltip Ui and show it where mouse enter
        GameObject tooltip = GameObject.Find("toolTip");
        tooltip.GetComponent<RectTransform>().anchoredPosition = mousePosition;
        // display weapon tooltip
        GameObject tipText = tooltip.transform.Find("tipText").gameObject;
        tipText.GetComponent<Text>().text = itemToolTip;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject tooltip = GameObject.Find("toolTip");
        Debug.Log("Mouse Exit" + gameObject.GetComponent<Image>().sprite);
        // when mouse exit we just move the tooltip out of screen canvas
        tooltip.GetComponent<RectTransform>().localPosition = new Vector2(-750f, -160f);
    }

}
