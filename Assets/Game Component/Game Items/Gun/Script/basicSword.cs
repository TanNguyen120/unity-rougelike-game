using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSword : MonoBehaviour
{
    public float damage;




    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("enterTriger");
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<lifeControl>().receiveDamage(damage);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

}
