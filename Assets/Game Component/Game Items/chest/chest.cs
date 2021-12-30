using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject item;

    public void openChest()
    {
        // create the item this chest hold not far away from it position 
        Instantiate(item, transform.position + (Vector3.down * 1), Quaternion.identity);
        Destroy(gameObject);
    }
}
