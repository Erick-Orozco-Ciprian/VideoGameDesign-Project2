using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantProjectilePushScript : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1; // Set damage to 1 if each projectile should remove one heart
    public Vector3 direction;
    private bool hasCollided = false; // Flag to ensure single collision
    private Rigidbody2D rb;
    private Vector2 moveDirection; // Direction of movement
    public bool canHurt;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set the initial direction of movement towards the original target
        //moveDirection = (originalTargetTransform.position - rb.position).normalized;
        moveDirection = direction.normalized;
    }
    void Update()
    {
        //transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Apply velocity to the Rigidbody
        //rb.velocity = direction.normalized * speed;

        // Apply velocity to the Rigidbody
        rb.velocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("SlowGround")))
        {
            hasCollided = true; // Set flag to true on first collision
            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }

    // Call this method when you want to change the target
    public void ChangeTarget(Vector2 newDirection)
    {
        // Calculate new direction towards the new target
        moveDirection = newDirection;
    }
}
