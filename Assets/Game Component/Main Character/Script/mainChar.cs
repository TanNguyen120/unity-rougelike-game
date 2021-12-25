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

    public float playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveListener();
        checkDead();
    }

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
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
        Debug.Log("angle:" + angle);
    }


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

    private void checkDead()
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Dead");
        }
    }

    public void receiveDamage(float damage)
    {
        playerHealth -= damage;
    }
}

