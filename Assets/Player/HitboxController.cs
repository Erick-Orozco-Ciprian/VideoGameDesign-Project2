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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDeflectTimer = canDeflectTime;
        cooldownTimer = cooldownTime;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCoolingDown)
        {
            canDeflect = true;
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
    }
  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            //Debug.Log("Projectile Detected!");
            // Handle projectile collision if left mouse button clicked and not cooling down
            if (canDeflect)
            {
                Debug.Log("Projectile Deflected!");

                // Calculate reflection direction using Vector3.Reflect
                Rigidbody2D projectileRB = other.GetComponent<Rigidbody2D>();
                Vector2 reflectionDirection = Vector2.Reflect(projectileRB.velocity.normalized, transform.up);

                // Apply reflection force to the projectile
                projectileRB.velocity = reflectionDirection.normalized * projectileRB.velocity.magnitude;
            }
        }
    }
}

