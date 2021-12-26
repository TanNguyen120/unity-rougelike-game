using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class basic_ai : MonoBehaviour
{


    public float moveSpeed;


    public float meter = 1;
    public float travelDistance = 1000;

    bool meterUp = true;


    // player Position
    private Transform target;
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player is move close to the object it will follow them
        if (Vector2.Distance(this.transform.position, target.position) < 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        }
        else // just move right and left
        {
            moveHandle();
        }

    }

    // get a random direction vector

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
        transform.position += new Vector3(1f, 0f, 0f) * moveSpeed * Time.deltaTime;
        meter++;
    }

    void moveLeft()
    {
        transform.position += new Vector3(-1f, 0f, 0f) * moveSpeed * Time.deltaTime;
        meter--;
    }

    // deal damage when Collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = (GameObject)other.gameObject;
            player.GetComponent<mainChar>().receiveDamage(5f);
        }
    }

}

