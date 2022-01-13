using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{

    [SerializeField]
    float damage = 10;

    [SerializeField]
    GameObject impact;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(impact, gameObject.transform.position, Quaternion.identity);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<mainChar>().receiveDamage(damage);
            Destroy(gameObject);
        }
        Debug.Log("meet:" + other.gameObject.name);
        Destroy(gameObject);
    }

    void Start()
    {
        // destroy the bullet after 20 seconds
        Destroy(gameObject, 20f);
    }
}
