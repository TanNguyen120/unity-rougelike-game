using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeControl : MonoBehaviour
{
    public float maxhealth;

    public float health;

    void Awake()
    {
        health = maxhealth;
    }

    private void Update()
    {
        checkLife();
    }

    private void checkLife()
    {
        // destroy game object when it run out of health
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void receiveDamage(float damage)
    {
        Debug.Log("receiveDamage: " + damage);
        health -= damage;
    }
}
