using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatlhPotion : MonoBehaviour
{
    public float amount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SFXManager.instance.playDrinkPotionSound();
            other.gameObject.GetComponent<mainChar>().restoreHealth(amount);
            Destroy(gameObject);
        }
    }
}
