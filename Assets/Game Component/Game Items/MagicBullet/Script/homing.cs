using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homing : MonoBehaviour
{
    public float radius;

    public float fireVelocity;

    float angle = 0;

    bool isIdle = true;

    CircleCollider2D collider;

    public float damage;

    [SerializeField] GameObject impact;


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Awake()
    {

        if (damage == 0)
        {
            damage = 10;
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void FixedUpdate()
    {
        circleAround();
    }

    // make the projectile go circleAround
    void circleAround()
    {
        if (isIdle)
        {

            // because the player is unique so the array always have one element
            GameObject mainChar = GameObject.FindGameObjectsWithTag("Player")[0];
            Vector3 mainPosition = mainChar.transform.position;
            // he toa do cuc co ban 
            // ham x = rcosA, y= rsinA;
            float xPos = Mathf.Cos(angle);
            float yPos = Mathf.Sin(angle);
            transform.position = mainPosition + new Vector3(xPos * radius, yPos * radius, 0);
            Debug.Log("bullet pos: " + transform.position);
            angle += Time.deltaTime + fireVelocity;
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------


    // Start is called before the first frame update



    void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(impact, gameObject.transform.position, Quaternion.identity);
        if (other.gameObject.tag == "enemy")
        {
            GameObject enemy = other.gameObject;
            enemy.GetComponent<lifeControl>().receiveDamage(damage);
        }
        // PLAY some PARTICLE
        Destroy(gameObject);
    }
}
