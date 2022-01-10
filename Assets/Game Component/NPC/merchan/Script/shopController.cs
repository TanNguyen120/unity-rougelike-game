using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using random = UnityEngine.Random;
public class shopController : MonoBehaviour
{
    [SerializeField] List<Image> itemImages = new List<Image>();

    [SerializeField] List<GameObject> itemsToSell = new List<GameObject>();


    void Start()
    {
        selling();
    }
    void selling()
    {
        for (int i = 0; i < itemImages.Count; i++)
        {
            GameObject item = itemsToSell[random.Range(0, itemsToSell.Count)];
            // get a random items in itemsToSell and display it to inventory slot
            itemImages[i].sprite = item.GetComponent<SpriteRenderer>().sprite;
            itemImages[i].name = item.name;
            shopSlotScript script = itemImages[i].GetComponent<shopSlotScript>();
            // show the item price in the price tag
            script.priceTag.GetComponent<Text>().text = item.GetComponent<itemAttribute>().price.ToString();
            script.itemPrice = item.GetComponent<itemAttribute>().price;
            script.itemToolTip = item.GetComponent<itemAttribute>().tooltip;
        }
    }
}
