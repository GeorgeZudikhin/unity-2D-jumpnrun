using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Singleton - one instance to rule them all
    static SoundManager soundManager;

    [Header("Player")]
    [SerializeField] private AudioClip[] playerBasicAttack;
    [SerializeField] private AudioClip[] playerSlideGround;
    [SerializeField] private AudioClip[] playerSlideAttack;
    [SerializeField] private AudioClip[] playerDash;
    [SerializeField] private AudioClip[] playerDeath;
    [SerializeField] private AudioClip[] playerJump;
    [SerializeField] private AudioClip[] playerDoubleJump;
    [SerializeField] private AudioClip[] playerHit;
    [SerializeField] private AudioClip[] playerHeal;

    [Header("Skeleton_A")]
    [SerializeField] private AudioClip[] skeletonAttack1;
    [SerializeField] private AudioClip[] skeletonAttack2;
    [SerializeField] private AudioClip[] skeletonHit;
    [SerializeField] private AudioClip[] skeletonDeath;
    [SerializeField] private AudioClip[] skeletonHeal;

    [Header("Background Music")]
    [SerializeField] private AudioClip fullBGM;
    [SerializeField] private AudioClip startBGM;
    [SerializeField] private AudioClip loopBGM;
    [SerializeField] private AudioClip endBGM;
    [SerializeField] private AudioClip gameOverBGM;

    [Header("Audio Mixer Groups")]
    [SerializeField] private AudioMixerGroup playerGroup;
    [SerializeField] private AudioMixerGroup enemyGroup;
    [SerializeField] private AudioMixerGroup sceneGroup;
    [SerializeField] private AudioMixerGroup menuGroup;
    [SerializeField] private AudioMixerGroup musicGroup;

    AudioSource playerSource;
    AudioSource enemySource;
    AudioSource sceneSource;
    AudioSource menuSource;
    AudioSource musicSource;

    void Awake()
    {
        // Singleton SoundManager has to exist accross scenes.
        soundManager = this;
        //DontDestroyOnLoad(gameObject); //makes sure the instance doesn't get removed during runtime

        //Generate Audio Source "channels"
        playerSource = gameObject.AddComponent<AudioSource>();
        enemySource = gameObject.AddComponent<AudioSource>();
        sceneSource = gameObject.AddComponent<AudioSource>();
        menuSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        playerSource.outputAudioMixerGroup = playerGroup;
        enemySource.outputAudioMixerGroup = enemyGroup;
        sceneSource.outputAudioMixerGroup = sceneGroup;
        menuSource.outputAudioMixerGroup = menuGroup;
        musicSource.outputAudioMixerGroup = musicGroup;

        //Begin playing the audio
        RefreshAudioVolume();
        StartLevelBGM();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public static void RefreshAudioVolume()
    {
        if (soundManager == null)
        {
            return;
        }
        soundManager.musicSource.volume = GameManager.GetBgmVolume();
    }

    public static void StartLevelBGM()
    {
        // Play the start part of the background music
        if(soundManager.startBGM != null)
        {
            soundManager.musicSource.PlayOneShot(soundManager.startBGM);
        }
        // Play the loop part of the background music - in loop obviously.
        if(soundManager.loopBGM != null)
        {
            soundManager.musicSource.clip = soundManager.loopBGM;
            soundManager.musicSource.loop = true;
            soundManager.musicSource.Play();
        }
    }
    public static void PlayEndLevelBGM()
    {
        // Stop the BGM loop
        soundManager.musicSource.loop = false;
        // Play the end part of the background music
        if (soundManager.endBGM != null)
        {
            soundManager.musicSource.PlayOneShot(soundManager.endBGM);
        }
    }
    public static void PlayGameOverBGM()
    {
        // Interrupt all running music
        soundManager.musicSource.Stop();
        soundManager.musicSource.loop = false;
        // Play the game over background music
        if (soundManager.gameOverBGM != null)
        {
            soundManager.musicSource.PlayOneShot(soundManager.gameOverBGM);
        }
    }
}
