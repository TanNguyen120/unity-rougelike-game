using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource randomGenSceneBGM;

    [SerializeField] AudioSource beginSceneBGM;

    [SerializeField] AudioSource kingSlimeBGM;
    public static SoundManager instance = null;

    [SerializeField] bool oneTimeUpdate;
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
    [SerializeField] SceneState currentScene;

    public void playRandomSceneBGM()
    {
        if (beginSceneBGM.isPlaying)
        {
            beginSceneBGM.Stop();
        }
        if (kingSlimeBGM.isPlaying)
        {
            kingSlimeBGM.Stop();
        }
        randomGenSceneBGM.Play();
    }

    public void playBeginSceneBGM()
    {
        // turn off all orther music and play the beginSceneBG
        if (randomGenSceneBGM.isPlaying)
        {
            randomGenSceneBGM.Stop();
        }
        if (kingSlimeBGM.isPlaying)
        {
            kingSlimeBGM.Stop();
        }
        beginSceneBGM.Play();
    }

    public void playKingSlimeSceneBGM()
    {
        // turn off all orther music and play the kingSlimeSceneBG
        if (randomGenSceneBGM.isPlaying)
        {
            randomGenSceneBGM.Stop();
        }
        if (beginSceneBGM.isPlaying)
        {
            beginSceneBGM.Stop();
        }
        kingSlimeBGM.Play();
    }
}
