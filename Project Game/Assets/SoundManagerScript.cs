using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
public static AudioClip AttackSound, CollectCoinSound, JumpSound, EnemyDeathSound;
static AudioSource audioSrc;

    void Start()
    {
        AttackSound = Resources.Load <AudioClip> ("Attack");
        CollectCoinSound = Resources.Load <AudioClip> ("CollectCoin");
        JumpSound = Resources.Load <AudioClip> ("Jump");
        EnemyDeathSound = Resources.Load <AudioClip> ("EnemyDeath");
        
        audioSrc = GetComponent <AudioSource> ();
    }

    public static void PlaySound (string clip)
    {
        switch (clip) {

        case "Attack":
            audioSrc.PlayOneShot (AttackSound);
            break;

        case "CollectCoin":
            audioSrc.PlayOneShot (CollectCoinSound);
            break;

        case "Jump":
            audioSrc.PlayOneShot (JumpSound);
            break;

        case "EnemyDeath":
            audioSrc.PlayOneShot (EnemyDeathSound);
            break;
        }
    }
}
