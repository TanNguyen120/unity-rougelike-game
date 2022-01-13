using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;
using UnityEngine.UI;

public class slimeKingBehavier : MonoBehaviour
{

    [SerializeField] GameObject stage1Projectile;

    [SerializeField] GameObject stage2Projectile;

    [SerializeField] GameObject childrenSlime;

    //--------------------------------- flag variables --------------------------------------
    [SerializeField] float attackTimer;
    public float reAttackTime;
    [SerializeField] float slimeSpawnTimer;

    public float respawnTime; // time to spawn another slime

    //-------------------------------- loot drop var ------------------------------------------
    [SerializeField] GameObject chest;
    [SerializeField] GameObject souls;

    //---------------------------------- Health bar var ------------------------------------------

    [SerializeField] Image healthMask;
    [SerializeField] Text healthDisplay;

    float originalSize;

    float maxhealth;

    float currentHealth;

    void Start()
    {
        maxhealth = gameObject.GetComponent<lifeControl>().getMaxHealth();
        currentHealth = gameObject.GetComponent<lifeControl>().getCurrentHealth();
        attackTimer = 0;
        slimeSpawnTimer = 0;
        souls.GetComponent<souls>().amount = 1000;
        originalSize = healthMask.rectTransform.rect.width;
    }


    // Update is called once per frame
    void Update()
    {
        currentHealth = gameObject.GetComponent<lifeControl>().getCurrentHealth();
        // calculate health
        int helthPercent = (int)(currentHealth / maxhealth) * 100;

        // the boss have two stage 
        if (helthPercent > 20)
        {
            spawningSlime();
            basicAttack(1.5f);
            attackTimer += Time.deltaTime;
            slimeSpawnTimer += Time.deltaTime;
        }
        if (helthPercent < 20)
        {
            spawningSlime();
            basicAttack(2f);
            attackTimer += Time.deltaTime + 1;
            slimeSpawnTimer += Time.deltaTime + 1;
        }
        if (currentHealth <= 0)
        {
            deadHandle();
        }
        healthDisplay.text = (currentHealth / maxhealth) * 100 + " %";

    }

    void spawningSlime()
    {
        if (slimeSpawnTimer >= respawnTime)
        {
            // spawn slime at random position in front of the boss
            int xpos = rand.Range(-3, 3);
            Instantiate(childrenSlime, gameObject.transform.position + new Vector3((float)xpos, -3, 0), Quaternion.identity);
            slimeSpawnTimer = 0;
        }
    }

    void basicAttack(float attackSpeed)
    {
        float angle;
        Vector3 direction;
        if (attackTimer >= reAttackTime)
        {
            // fire ring of bullets using circle equational
            for (int i = 0; i < 380; i = i + 20)
            {
                angle = (float)i;
                direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                GameObject slimeProjectile = Instantiate(stage1Projectile, gameObject.transform.position, Quaternion.identity);
                // re calculate the angle to degree
                float rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                slimeProjectile.transform.eulerAngles = new Vector3(0, 0, rotateAngle);
                // fire the bullet with the direction
                slimeProjectile.GetComponent<Rigidbody2D>().velocity = direction * attackSpeed;
                //reset timer
                attackTimer = 0;
            }
        }
    }

    void deadHandle()
    {
        // spawn a chest and souls
        Instantiate(chest, transform.position + Vector3.down, Quaternion.identity);
        Instantiate(souls, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetHealth(float value)
    {
        healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);

    }

}
