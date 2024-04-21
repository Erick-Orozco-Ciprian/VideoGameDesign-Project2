using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1; // Set damage to 1 if each projectile should remove one heart
    public Vector3 direction;
    private bool hasCollided = false; // Flag to ensure single collision
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Apply velocity to the Rigidbody
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasCollided && collision.CompareTag("Player"))
        {
            hasCollided = true; // Set flag to true on first collision
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }
}
