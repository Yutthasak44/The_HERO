using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource SoundWalk;
    public AudioSource SoundAttack;
    public AudioSource SoundJump;
    public AudioSource SoundHurt;
    public AudioSource SoundItem;
    public bool IsWalk;

    void Update()
    {   
        if(SoundWalk != null)
            soundWalk();
    }

     void soundWalk()
    {
        if (IsWalk)
        {
            if (!SoundWalk.isPlaying)
            {
                PlaySound(SoundWalk);
                print("isplay");
            }
        }
        else
        {
            if (SoundWalk.isPlaying)
            {
                StopSound(SoundWalk);
                print("isstop");
            }
        }
    }

    public void SoundGetItem()
    {
        if (SoundItem != null)
            PlaySound(SoundItem);
    }

    public void SoundSlash()
    {
        if(SoundAttack != null)
            PlaySound(SoundAttack);
    }

    public void PlaySound(AudioSource Sound)
    {
        if(Sound != null)
            Sound.Play();
    }
    public void StopSound(AudioSource Sound)
    {
        if (Sound != null)
            Sound.Stop();
    }

}
