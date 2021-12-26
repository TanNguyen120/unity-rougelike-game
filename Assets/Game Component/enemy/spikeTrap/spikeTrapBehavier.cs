using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrapBehavier : MonoBehaviour
{
    public float damage = 2f;

    void Awake()
    {
        Debug.Log("hah");
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("enter trap ");
            other.gameObject.GetComponent<mainChar>().receiveDamage(damage);

        }
    }
}
