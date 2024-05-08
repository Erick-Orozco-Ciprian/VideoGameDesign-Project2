using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpiritController : MonoBehaviour
{
    public Transform player;
    public float attackRange = 5f;
    public GameObject projectilePrefab1; // First projectile prefab
    public GameObject projectilePrefab2; // Second projectile prefab
    public GameObject soulEnemyPrefab;
    public float attackRate = 1f; // Attacks per second
    private Animator animator;
    private float lastAttackTime;
    private Vector3 startPosition;
    private bool canAttack;
    public int health = 5;

    // Flag to toggle between projectile prefabs
    private bool useProjectilePrefab1 = true;

    private void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        canAttack = Time.time > lastAttackTime + 1f / attackRate;
        if (distanceToPlayer < attackRange && canAttack)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        if (health <= 0)
        {
            Disappear();
        }
        
        if (player.position.x < transform.position.x)
        {
            // Player is on the left side
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // Player is on the right side
            transform.localScale = new Vector3(1, 1, 1);
        } 
    }

    public void attackAnimationFinished() {
        animator.SetBool("isAttacking", false);
    }

    void AttackPlayer()
    {
        // Instantiate and configure the projectile based on the boolean flag
        animator.SetBool("isAttacking", true);
        GameObject projectile;
        if (useProjectilePrefab1)
        {
            projectile = Instantiate(projectilePrefab1, transform.position, Quaternion.identity);
            PlantProjectileScript plantProjectileScript = projectile.GetComponent<PlantProjectileScript>();
            plantProjectileScript.direction = (player.position - transform.position).normalized;
        }
        else
        {
            projectile = Instantiate(projectilePrefab2, transform.position, Quaternion.identity);
            PlantProjectilePushScript plantProjectilePushScript = projectile.GetComponent<PlantProjectilePushScript>();
            plantProjectilePushScript.direction = (player.position - transform.position).normalized;
        }


        // Toggle the flag for the next shot
        useProjectilePrefab1 = !useProjectilePrefab1;
    }

    public void Disappear()
    {
        // Instantiate soulEnemyPrefab at the position of the FlameSpirit
        Instantiate(soulEnemyPrefab, transform.position, Quaternion.identity);

        // Deactivate or destroy the FlameSpirit GameObject
        gameObject.SetActive(false); // Deactivate the GameObject
        // Destroy(gameObject); // Destroy the GameObject
    }
}
