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

    public float floatSpeed = 1.0f; // Speed of floating movement
    public float floatHeight = 0.5f; // Height of the float
    private Vector3 startPosition;
    public int bossHealth = 5;
    public GameObject breadObject; // Assign the bread prefab in the Unity Editor

    private void Start()
    {
        startPosition = transform.position;
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

        Vector3 newPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = newPosition;

        if (bossHealth < 0) {
            Death();
        }
    }

    void AttackPlayer()
    {
        // Trigger attack animation
        animator.SetTrigger("attack");
        // Instantiate and configure the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.direction = (player.position - transform.position).normalized;
    }

    void Death() 
    {
        breadObject.SetActive(true);
        // Destroy the boss object
        Destroy(gameObject);
    }
}

