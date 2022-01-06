using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// this class is for store some data of gameObject so 
[System.Serializable]
public class itemsData
{
    [SerializeField]
    public Sprite itemIcon { get; set; }

    [SerializeField]
    public string itemName { get; set; }

    [SerializeField]
    public bool isMainWeapon { get; set; }

    public int inventorySlot { get; set; }

}

