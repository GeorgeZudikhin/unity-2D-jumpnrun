using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_A_Combat : MonoBehaviour
{
    //--------------------------------------------------
    // References
    private Animator animator;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip[] attack1Sounds;
    [SerializeField] private AudioClip[] attack2Sounds;

    [Header("Attack Parameters")]
    [SerializeField] private float attack1Cooldown = 1f;
    [SerializeField] private float attack2Cooldown = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage1 = 10;
    [SerializeField] private int damage2 = 15;
    private float attack1CooldownTimer = 0f;
    private float attack2CooldownTimer = 0f;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private BoxCollider2D boxCollider2;


    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    //--------------------------------------------------


    // Start is called before the first frame update
    /*
    void Start()
    {
        // Find the player GameObject and get its tranform component
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //playerTransform = player.transform;
    }
    */

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        attack1CooldownTimer += Time.deltaTime;
        attack2CooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            //Attack
            if (attack1CooldownTimer >= attack1Cooldown)
            {
                attack1CooldownTimer = 0;
                if (attack1Sounds.Length > 0)
                {
                    AudioSource.PlayClipAtPoint(
                        attack1Sounds[Random.Range(0, attack1Sounds.Length - 1)], //Which sound effect - (a random one)
                        transform.position  //2D location from where it's heard
                    );
                }
                animator.SetTrigger("Attack1");
            }
            else if (attack2CooldownTimer >= attack2Cooldown)
            {
                attack2CooldownTimer = 0;
                if (attack2Sounds.Length > 0)
                {
                    AudioSource.PlayClipAtPoint(
                        attack2Sounds[Random.Range(0, attack2Sounds.Length - 1)], //Which sound effect - (a random one)
                        transform.position  //2D location from where it's heard
                    );
                }
                animator.SetTrigger("Attack2");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,  //origin
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),   //size
            0,                          //angle
            Vector2.left,               //direction
            0,                          //distance
            playerLayer                 //what to detect (the player)
        );
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,  //origin
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z)      //size
        );
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,  //origin
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z)      //size
        );
    }

    private void Damage1Player()
    {
        // If player is still within range, damage them
        if (PlayerInSight())
        {
            // Damage player health
            playerHealth.TakeDamage(damage1);
        }
    }
    private void Damage2Player()
    {
        // If player is still within range, damage them
        if (PlayerInSight())
        {
            // Damage player health
            playerHealth.TakeDamage(damage2);
        }
    }

    //Individual Handling of the character's death.
    //IMPORTANT: The uniform handling every character gets is in the Health script!
    public void Death()
    {
        // Disable the enemy and their physics
        GetComponent<Collider2D>().enabled = false;
        enemyPatrol.enabled = false;
        GetComponent<Health>().enabled = false;
        this.enabled = false;
    }
}
