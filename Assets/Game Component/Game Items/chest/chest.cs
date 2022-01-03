using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject[] items;

    public void openChest()
    {
        //spawn a random items
        int randomIndex = Random.Range(0, items.Length);

        // create the item this chest hold not far away from it position 
        Instantiate(items[randomIndex], transform.position + (Vector3.down * 1), Quaternion.identity);
        Destroy(gameObject);

    }
}
