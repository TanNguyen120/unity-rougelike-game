using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class souls : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.gameObject;

        if (player.tag == "Player")
        {
            SFXManager.instance.playSuckingSound();
            GameManeger.instance.absorbSouls(amount);
            Destroy(gameObject);
        }
    }
}
