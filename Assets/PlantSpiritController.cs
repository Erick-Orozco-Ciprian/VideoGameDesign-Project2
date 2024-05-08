using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpiritController : MonoBehaviour
{
    public Transform player;
    public float attackRange = 5f;
    public GameObject projectilePrefab;
    public GameObject soulEnemyPrefab;
    public float attackRate = 1f; // Attacks per second
    private Animator animator;
    private float lastAttackTime;
    private Vector3 startPosition;
    private bool canAttack;

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

        // if (player.position.x < transform.position.x)
        // {
        //     // Player is on the left side
        //     transform.localScale = new Vector3(-1, 1, 1);
        // }
        // else
        // {
        //     // Player is on the right side
        //     transform.localScale = new Vector3(1, 1, 1);
        // }  }
    }

    public void attackAnimationFinished() {
        animator.SetBool("isAttacking", false);
    }

    void AttackPlayer()
    {
        // Instantiate and configure the projectile
        animator.SetBool("isAttacking", true);
        // Instantiate and configure the projectile slightly above the enemy
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.direction = (player.position - transform.position).normalized;
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

