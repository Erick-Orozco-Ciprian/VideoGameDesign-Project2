using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public float cooldownTime = 0.75f; // Cooldown time in seconds
    private float cooldownTimer = 0f;
    public float canDeflectTime = 0.2f; // Cooldown time in seconds
    private float canDeflectTimer = 0f;
    private bool isCoolingDown = false;
    private bool canDeflect = false;
    private Rigidbody2D rb;
    private GameObject projectileObject; // Variable to store the collided object
    private bool hasDeflected = false; // Flag to track if projectile has been deflected
    private bool reset = false; // Flag to track if projectile has been deflected

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDeflectTimer = canDeflectTime;
        cooldownTimer = cooldownTime;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && !isCoolingDown)
        {
            canDeflect = true;
        }

        if (canDeflect)
        {
            canDeflectTimer -= Time.deltaTime;
            if (canDeflectTimer <= 0f)
            {
                canDeflect = false;
                isCoolingDown = true;
                canDeflectTimer = canDeflectTime;
            }
        }

        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
            }
        }

        
        if (reset)
        {
            hasDeflected = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Projectile") && !hasDeflected)
        {
            reset = false;
            // Handle projectile collision if left mouse button clicked and not cooling down
            if (canDeflect)
            {
                Rigidbody2D projectileRB = other.GetComponent<Rigidbody2D>();
                Vector2 reflectionDirection = -projectileRB.velocity.normalized;
                projectileObject = other.gameObject;
                Projectile projectileScript = projectileObject.GetComponent<Projectile>();
                projectileScript.ChangeTarget(reflectionDirection.normalized);
                projectileScript.canHurtBoss = true;
                hasDeflected = true; // Set the flag to true after deflection
            }
        }

        else if (other.CompareTag("FireEnemy") && !hasDeflected)
        {
            Animator enemyAnimator = other.GetComponent<Animator>();
            if (enemyAnimator != null && canDeflect)
            {
                enemyAnimator.SetBool("isDying", true);
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Projectile") && hasDeflected)
        {
            reset = true;
        }
    }
}

