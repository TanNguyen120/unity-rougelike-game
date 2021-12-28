using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtrribute : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Awake()
    {
        if (damage == 0)
        {
            damage = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("enemy detected");
            GameObject enemy = other.gameObject;
            enemy.GetComponent<lifeControl>().receiveDamage(damage);
        }
        Destroy(gameObject);
    }
}
