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
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(0, 1);
        }
        else
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition + transform.position).normalized;
        animator.SetFloat("xDirection", aimDirection.x);
        animator.SetFloat("yDirection", aimDirection.y);
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
}

