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
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    [SerializeField] Text healthDisplay;

    float originalSize;
    [SerializeField] Image healthMask;
    //--------------------------------- exit door -------------------------------------------------
    [SerializeField] GameObject exitDoor;

    //--------------------------------- Particle System -------------------------------------------
    [SerializeField] ParticleSystem deadExpolder;

    void Start()
    {
        exitDoor.SetActive(false);
        attackTimer = 0;
        slimeSpawnTimer = 0;
        souls.GetComponent<souls>().amount = 1000;
        currentHealth = maxHealth;
        healthDisplay.text = currentHealth + "/" + maxHealth;
        originalSize = healthMask.rectTransform.rect.width;
        deadExpolder.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        float healthPercent = (currentHealth / maxHealth);
        setBossHealth(healthPercent);
        if (currentHealth <= 0)
        {
            deadHandle();
        }
        if (healthPercent * 100 > 20)
        {
            spawningSlime();
            basicAttack(1.5f);
            attackTimer += Time.deltaTime;
            slimeSpawnTimer += Time.deltaTime;
        }
        if (healthPercent * 100 < 20)
        {
            spawningSlime();
            basicAttack(2f);
            attackTimer += (1.5f * Time.deltaTime);
            slimeSpawnTimer += (Time.deltaTime * 1.5f);
        }

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

    // spawn some item and open gate to next level when the boss dead
    void deadHandle()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        // play the dramatic effect
        StartCoroutine(playDeadParticles());
        // spawn a chest and souls

    }


    // reciveDamge when hit by player bullet
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("somthing Enter");
        if (other.gameObject.tag == "player bullet")
        {
            Debug.Log("it player bullet");
            if (other.gameObject.GetComponent<BulletAtrribute>())
            {
                currentHealth -= other.gameObject.GetComponent<BulletAtrribute>().damage;
            }
            else
            {
                currentHealth -= 35;
            }
            healthDisplay.text = currentHealth + "/" + maxHealth;
        }
    }

    void setBossHealth(float value)
    {
        healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    IEnumerator playDeadParticles()
    {
        Debug.Log("play particles");
        deadExpolder.Play();
        yield return new WaitForSeconds(2f);
        Instantiate(chest, transform.position + Vector3.down, Quaternion.identity);
        Instantiate(souls, transform.position, Quaternion.identity);
        // open door to next level
        exitDoor.SetActive(true);
        Destroy(gameObject);
    }
}
