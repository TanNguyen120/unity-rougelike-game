using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] AudioSource buttonPressSound;
    [SerializeField] AudioSource damageSound;

    [SerializeField] AudioSource gunFireSound;


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
}
