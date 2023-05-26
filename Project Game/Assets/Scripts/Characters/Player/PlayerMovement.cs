using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] doubleJumpSounds;
    [SerializeField] private AudioClip[] landingSounds;

    public CoinManager cm; 
    private bool m_CollectingCoin = false;
    
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private bool m_DoubleJumped = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            if (controller.m_Grounded)
            {
                // Player is on the ground and wants to jump
                if(jumpSounds.Length > 0)
                {
                    AudioSource.PlayClipAtPoint(
                        jumpSounds[Random.Range(0, jumpSounds.Length-1)], //Which sound effect - (a random one)
                        transform.position  //2D location from where it's heard
                    );
                }/*
                else
                {
                    SoundManagerScript.PlaySound("Jump");
                }*/
                jump = true;
                animator.SetBool("IsJumping", true);
                m_DoubleJumped = false; // Reset double jump flag on ground jump
                
            }
            else if (!m_DoubleJumped)
            {
                // Player is in the air and wants to double jump
                if (doubleJumpSounds.Length > 0)
                {
                    AudioSource.PlayClipAtPoint(
                        doubleJumpSounds[Random.Range(0, doubleJumpSounds.Length - 1)], //Which sound effect - (a random one)
                        transform.position  //2D location from where it's heard
                    );
                }/*
                else
                {
                    SoundManagerScript.PlaySound("Jump");
                }*/
                jump = true;
                animator.SetBool("IsJumping", true);
                m_DoubleJumped = true; // Set double jump flag
                
            }
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            //animator.SetBool("IsCrouching", true);
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            //animator.SetBool("IsCrouching", false);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        m_DoubleJumped = false; // Reset double jump flag on landing
        if (landingSounds.Length > 0)
        {
            AudioSource.PlayClipAtPoint(
                landingSounds[Random.Range(0, landingSounds.Length - 1)], //Which sound effect - (a random one)
                transform.position  //2D location from where it's heard
            );
        }
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

     void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            m_CollectingCoin = true;
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin") && m_CollectingCoin)
        {
            Destroy(other.gameObject); 
            cm.coinCount++; 
            m_CollectingCoin = false;
            SoundManagerScript.PlaySound("CollectCoin");
        }
    }
    
}

