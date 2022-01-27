using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] AudioSource buttonPressSound;
    [SerializeField] AudioSource damageSound;

    [SerializeField] AudioSource gunFireSound;

    [SerializeField] AudioSource soulsSuckSound;

    [SerializeField] AudioSource cancelButtonSound;

    [SerializeField] AudioSource drinkPotionSound;

    [SerializeField] AudioSource pickUpItemSound;

    [SerializeField] AudioSource teleSound;

    [SerializeField] AudioSource openChestSound;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        // use this to not destroy the game manager when new scene is created
        DontDestroyOnLoad(gameObject);
    }

    public void playButtonPress()
    {
        Debug.Log("play button press sound");
        buttonPressSound.PlayOneShot(buttonPressSound.clip);
    }

    public void playHitSound()
    {
        Debug.Log("play damage Sound");
        damageSound.PlayOneShot(damageSound.clip);
    }

    public void playExplosionSound()
    {
        Debug.Log("play explosion Sound");
        gunFireSound.PlayOneShot(gunFireSound.clip);
    }

    public void playCancleBTNSound()
    {
        Debug.Log("play cancelBTN Sound");
        cancelButtonSound.PlayOneShot(cancelButtonSound.clip);
    }

    public void playDrinkPotionSound()
    {
        Debug.Log("play drink potion Sound");
        drinkPotionSound.PlayOneShot(drinkPotionSound.clip);
    }

    public void playSuckingSound()
    {
        Debug.Log("play sucking Sound");
        soulsSuckSound.PlayOneShot(soulsSuckSound.clip);
    }

    public void playTeleSound()
    {
        Debug.Log("play tele Sound");
        teleSound.PlayOneShot(teleSound.clip);
    }

    public void playOpenChestSound()
    {
        Debug.Log("play open Chest Sound");
        openChestSound.PlayOneShot(openChestSound.clip);
    }

    public void playPickUpItemSound()
    {
        Debug.Log("play pickUpItemSound");
        pickUpItemSound.PlayOneShot(pickUpItemSound.clip);
    }
}
