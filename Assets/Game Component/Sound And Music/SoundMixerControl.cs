using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerControl : MonoBehaviour
{
    [SerializeField] AudioMixer audioController;

    public void setBGMSound(float amount)
    {
        audioController.SetFloat("BGM", amount);
    }

    public void setEffectSound(float amount)
    {
        audioController.SetFloat("Effect", amount);
    }



}
