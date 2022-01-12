using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

public class moveSet : MonoBehaviour
{
    float health;
    float maxHealth;

    Rigidbody2D rigidBody;

    public float moveSpeed;
    // Start is called before the first frame update

    public float meter = 1;
    public float travelDistance = 1000;

    public GameObject enemyBullet;

    //----------------------------------- flag var -------------------------------
    [SerializeField]
    float attackTimer;

    [SerializeField]
    float reAttackTime = 4;

    bool meterUp = true;
    void Start()
    {
        health = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= reAttackTime)
        {
            fireBullet();
            attackTimer = 0;
        }
        attackTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        moveHandle();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = (GameObject)other.gameObject;
            player.GetComponent<mainChar>().receiveDamage(5f);
        }
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

    void fireBullet()
    {
        int count = 1;
        while (count <= 4)
        {
            GameObject bullet = Instantiate(enemyBullet, gameObject.transform.position, Quaternion.identity);
            count++;
            float angle = rand.Range(0, 90f);
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 2;
        }
    }
}
