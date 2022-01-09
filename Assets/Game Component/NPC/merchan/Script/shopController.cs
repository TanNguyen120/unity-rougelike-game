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
            // get a random items in itemsToSell and display it to inventory slot
            itemImages[i].sprite = itemsToSell[random.Range(0, itemsToSell.Count)].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
