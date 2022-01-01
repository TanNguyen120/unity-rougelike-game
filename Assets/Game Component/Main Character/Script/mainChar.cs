using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainChar : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;
    private Vector3 moveDir;

    private bool isDashing = false;

    private Animator animator;

    private Vector2 lookDirection;

    public float playerHealth = 100;

    private float currentHealth;

    public Transform weaponHoldPoint;

    // save main weapon gameObject so we can re equip it next scene

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //check if we have store a weapon before
        if (GameManeger.instance.mainWeapon.Length != 0)
        {
            GameObject weaponPref = Resources.Load("Prefabs/items/" + GameManeger.instance.mainWeapon) as GameObject;
            if (weaponPref)
            {
                GameObject updateWeapon = Instantiate(weaponPref, transform.position, Quaternion.identity);
                swapWeapon(updateWeapon);
            }
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = playerHealth;
        weaponHoldPoint = transform.Find("weaponHold");


    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        moveListener();
        checkDead();
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            switchWeapon();
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate()
    {

        SetAnimationState();
        rigidBody.velocity = moveDir * speed * Time.deltaTime;

        if (isDashing)
        {
            rigidBody.MovePosition(transform.position + moveDir * 4);
            isDashing = false;
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void SetAnimationState()
    {
        if (moveDir.x == 0 && moveDir.y == 0)
        {
            animator.SetLayerWeight(0, 1);
            animator.SetLayerWeight(1, 0);
        }
        else
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }

        // 4 line below is some math to calculate the look direction and angle of the gun from 0 degree
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        lookDirection = (Vector2)aimDirection;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // control state base on angle
        if (angle <= -135f || angle >= 135f)
        {
            animator.SetFloat("xdirection", -1f);
            animator.SetFloat("ydirection", 0f);
        }
        if (angle <= 45f && angle > -35f)
        {
            animator.SetFloat("xdirection", 1f);
            animator.SetFloat("ydirection", 0f);
        }
        if (angle < 135f && angle > 45f)
        {
            animator.SetFloat("ydirection", 1f);
            animator.SetFloat("xdirection", 0f);
        }
        if (angle < -35f && angle > -135f)
        {
            animator.SetFloat("ydirection", -1f);
            animator.SetFloat("xdirection", 0f);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void moveListener()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }
        moveDir = new Vector3(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // handle game over event
    private void checkDead()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Dead");
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void receiveDamage(float damage)
    {
        currentHealth -= damage;
        UIController.instance.SetHealth(currentHealth / playerHealth);
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void restoreHealth(float amount)
    {
        currentHealth += amount;
        // if we over restoreHealth the set back player health at max health
        if (currentHealth > playerHealth)
        {
            currentHealth = playerHealth;
            UIController.instance.SetHealth(currentHealth / playerHealth);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void OpenChest()
    {
        // using RaycastHit2D to detect the chest
        RaycastHit2D hit = Physics2D.Raycast(rigidBody.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("chest"));
        if (hit.collider != null)
        {
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            chest chestObject = hit.collider.GetComponent<chest>();
            if (chestObject != null)
            {
                chestObject.openChest();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // change the curret held weapon 
    public void swapWeapon(GameObject ortherGun)
    {
        // first we destroy currently held weapon
        if (transform.Find("gun"))
        {
            GameObject currentGun = transform.Find("gun").gameObject;
            Destroy(currentGun);
        }
        Debug.Log("swap to " + ortherGun);


        // the we assign the new weapon and move it position to player
        ortherGun.transform.parent = transform;

        ortherGun.transform.position = weaponHoldPoint.position;

        ortherGun.name = "gun";

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // get the weapon in scene and swap with current held one
    public void switchWeapon()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidBody.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("gun"));
        if (hit.collider != null)
        {
            string weaponName = hit.collider.gameObject.name.Replace("(Clone)", "");
            GameManeger.instance.mainWeapon = weaponName;
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            swapWeapon(hit.collider.gameObject);

            // assign the weapon to main weapon in gamemanager

        }
    }
}

