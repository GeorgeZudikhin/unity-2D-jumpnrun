using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    // =//=//=//=//=//= Start of: PlayerCombat Class =//=//=//=//=//=
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int basicAttackDamage = 20;
    [SerializeField] private int dashAttackDamage = 40;

    [SerializeField] private float basicAttackRate = 1f;  ///  ( 1 / attackRate )
    private float nextAttackTime = 0f;

    private bool dashAttacking = false;

    public static event Action OnPlayerDeath;

    // =====/////===== Start of: Unity Lifecycle Functions =====/////=====
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime && !dashAttacking)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Debug.Log("Player Pressed Attack Button.");
                
                BasicAttack();
                nextAttackTime = Time.time + 1f / basicAttackRate;

                Debug.Log("Attack function ended.");
            }
        }
        
    }
    // =====/////===== End of: Unity Lifecycle Functions =====/////=====
    // ========== Start of: Action Functions and their Helpers ==========
    void BasicAttack()
    {
        // Play an attack animation
        animator.SetTrigger("BasicAttack");

        //SoundManagerScript.PlaySound("Attack");

        // Detect enemies in range of attacak
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Collider2D was filled with " + hitEnemies.Length + " enemies");

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Health>() != null)
            {
                Debug.Log("We hit " + enemy.name + ".");
                enemy.GetComponent<Health>().TakeDamage(basicAttackDamage);
            }
            
        }
    }

    public void DashAttack()
    {
        Debug.Log("Executed DashAttack.");
        dashAttacking = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Collider2D was filled with " + hitEnemies.Length + " enemies");
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Health>() != null)
            {
                Debug.Log("We hit " + enemy.name + ".");
                enemy.GetComponent<Health>().TakeDamage(dashAttackDamage);
            }

        }
        //nextAttackTime = Time.time + 1f / basicAttackRate;
        animator.SetBool("IsDashAttacking", false);
    }

    public void DashAttackEnd()
    {
        dashAttacking = false;
    }

    void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Death()
    {
        Debug.Log("The Player died!");

        //GameManager.isPlayerAlive = false;

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;

        Invoke(nameof(LoadDeathMenu), 0.5f);
    }

    public void LoadDeathMenu()
    {
        OnPlayerDeath?.Invoke();
        Time.timeScale = 0f;
    }
    // ========== End of: Action Functions and their Helpers ==========
    // =//=//=//=//=//= End of: PlayerCombat Class =//=//=//=//=//=
}
