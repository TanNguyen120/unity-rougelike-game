using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainChar : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;
    private Vector3 moveDir;

    private bool isDashing = false;

    private Animator animator;

    private Vector2 lookDirection;

    public float playerHealth = 100;

    static float currentHealth;

    public Transform weaponHoldPoint;

    // the flag for some thing we just want to update onetime
    public bool oneTimeUpdate;

    [SerializeField] GameObject mainWeapon;
    // save main weapon gameObject so we can re equip it next scene

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        oneTimeUpdate = true;
        //check if we have store a weapon before


        if (GameManeger.instance.sceneState == SceneState.beginScene)
        {
            Debug.Log("deactive gun");
            GameObject gun = transform.Find("gun").gameObject;
            gun.SetActive(false);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (currentHealth == 0)
        {
            currentHealth = playerHealth;

        }

        weaponHoldPoint = transform.Find("weaponHold");
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        if (oneTimeUpdate)
        {
            oneTimeUpdate = false;
            UIController.instance.SetHealth(currentHealth / playerHealth);

            // get the weapon info from gameManeger
            GameObject mainWeapon = GameManeger.instance.findMainWeapon();
            if (mainWeapon != null)
            {
                GameObject updateWeapon = Instantiate(mainWeapon, transform.position, Quaternion.identity);
                swapWeapon(updateWeapon);
            }
            else
            {
                // if not store we assign the default weapon
                // and add it to inventory
                GameObject defaultWeapon = transform.Find("gun").gameObject;
                string weaponName = defaultWeapon.name;
                Sprite weaponSprite = defaultWeapon.GetComponent<SpriteRenderer>().sprite;
                itemsData mainWeaponData = new itemsData { itemName = weaponName, itemIcon = weaponSprite, isMainWeapon = true };
                GameManeger.instance.addToInventory(mainWeaponData);
                UIController.instance.displayMainWeapon(mainWeaponData.itemIcon);
            }
        }
        moveListener();
        checkDead();
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
            talkingToBugMan();
            talkToMerchant();
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
            currentHealth = playerHealth;
            Debug.Log("Dead");
            GameManeger.instance.sceneState = SceneState.beginScene;
            // throw player back at the open scene
            SceneManager.LoadScene(0);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void receiveDamage(float damage)
    {
        currentHealth -= damage;

        UIController.instance.SetHealth(currentHealth / playerHealth);
        GameManeger.instance.playerHealth = currentHealth;
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
        else
        {
            UIController.instance.SetHealth(currentHealth / playerHealth);
            GameManeger.instance.playerHealth = currentHealth;

        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void talkingToBugMan()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidBody.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("npc"));
        if (hit.collider != null)
        {
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            bugManController bugman = hit.collider.gameObject.GetComponent<bugManController>();
            if (bugman)
            {
                Debug.Log("main player talk to bug man");
                bugman.chatting();
            }
        }

    }

    public void talkToMerchant()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidBody.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("npc"));
        if (hit.collider != null)
        {
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            npcInteract merchant = hit.collider.gameObject.GetComponent<npcInteract>();
            if (merchant)
            {
                Debug.Log("main player want to shopping");
                merchant.chatting();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void OpenChest()
    {
        // using RaycastHit2D to detect the chest
        RaycastHit2D hit = Physics2D.Raycast(rigidBody.position + Vector2.up * 0.2f, lookDirection, 3f, LayerMask.GetMask("chest"));
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

    // change the curret held weapon when pick up
    public void swapWeapon(GameObject ortherGun)
    {
        // first we destroy currently held weapon

        Debug.Log("swap to " + ortherGun);


        // the we assign the new weapon and move it position to player
        ortherGun.transform.parent = gameObject.transform;

        ortherGun.transform.position = weaponHoldPoint.position;


        if (gameObject.transform.Find("gun"))
        {
            Debug.Log("finned" + transform.Find("gun").name);
            GameObject currentGun = gameObject.transform.Find("gun").gameObject;
            Destroy(currentGun);
        }
        ortherGun.name = "gun";
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // get the weapon in scene and swap with current held one
    public void switchWeapon()
    {
        if (!GameManeger.instance.inventoryFull)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidBody.position + Vector2.up * 0.2f, lookDirection, 2.5f, LayerMask.GetMask("gun"));
            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);

                //swap the current held weapon to the new onestring weaponName = ortherGun.gameObject.name.Replace("(Clone)", "");
                GameObject pickedWeapon = hit.collider.gameObject;

                // add it to inventory and make it the main weapon
                itemsData pickedWeaponData = new itemsData
                {
                    itemName = pickedWeapon.name.Replace("(Clone)", ""),
                    itemIcon = pickedWeapon.GetComponent<SpriteRenderer>().sprite,
                    isMainWeapon = true
                };
                GameManeger.instance.addToInventory(pickedWeaponData);
                UIController.instance.displayMainWeapon(pickedWeaponData.itemIcon);
                swapWeapon(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.Log("cant pick up");
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    // this func is for swap weapon in inventory so we dont need to add a new item to inventory
    public void changeMainWeapon(GameObject ortherGun)
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

}

