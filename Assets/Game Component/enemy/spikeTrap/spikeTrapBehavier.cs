using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeTrapBehavier : MonoBehaviour
{
    public float damage = 1f;

    // time between each attack
    public float reAttackTime = 3f;

    public bool attack = false;

    private void Update()
    {
        attackCounter();
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (attack == true)
            {
                return;
            }
            Debug.Log("enter trap ");
            attack = true;
            other.gameObject.GetComponent<mainChar>().receiveDamage(damage);
        }
    }

    void attackCounter()
    {
        // timer count down 3 seconds before attack the player again
        if (attack == true)
        {
            reAttackTime -= Time.deltaTime;
            if (reAttackTime <= 0)
            {
                attack = false;
                reAttackTime = 3f;
            }
        }
    }
}
