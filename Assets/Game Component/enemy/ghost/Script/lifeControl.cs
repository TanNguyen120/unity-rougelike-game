using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeControl : MonoBehaviour
{
    public float maxhealth;

    public float health;

    public Animator animator;

    void Awake()
    {
        health = maxhealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        checkLife();
    }

    private void checkLife()
    {
        // destroy game object when it run out of health
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void receiveDamage(float damage)
    {
        Debug.Log("receiveDamage: " + damage);
        health -= damage;
        StartCoroutine(notDamgeAnimation());

    }
    IEnumerator notDamgeAnimation()
    {
        animator.SetBool("damged", true);
        // trigger the damged animation for 2 seconds
        yield return new WaitForSeconds(2);
        animator.SetBool("damged", false);
    }
}
