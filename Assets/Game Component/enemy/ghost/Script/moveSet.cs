using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSet : MonoBehaviour
{
    float health;
    float maxHealth;

    Rigidbody2D rigidBody;

    public float moveSpeed;
    // Start is called before the first frame update

    public float meter = 1;
    public float travelDistance = 1000;

    bool meterUp = true;
    void Start()
    {
        health = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        moveHandle();
    }

    void moveHandle()
    {
        if (meter <= travelDistance)
        {
            if (meterUp)
            {
                moveRight();
            }
            else
            {
                moveLeft();
            }

        }
        if (meter >= travelDistance)
        {
            meterUp = false;
        }
        if (meter < 0)
        {
            meterUp = true;
        }

    }

    void moveRight()
    {
        rigidBody.velocity = new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        meter++;
    }

    void moveLeft()
    {
        rigidBody.velocity = new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
        meter--;
    }
}
