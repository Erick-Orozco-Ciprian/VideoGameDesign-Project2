using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 10f;
    public GameObject projectilePrefab;
    public float attackRate = 1f; // Attacks per second

    private Animator animator;
    private float lastAttackTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < attackRange && Time.time > lastAttackTime + 1f / attackRate)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        // Trigger attack animation
        animator.SetTrigger("attack");
        // Method to shoot projectile
        ShootProjectile();
    }

    void ShootProjectile()
    {
        // Instantiate and configure the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.direction = (player.position - transform.position).normalized;
    }
}

