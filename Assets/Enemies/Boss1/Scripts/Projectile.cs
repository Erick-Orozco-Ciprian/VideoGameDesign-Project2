using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1; // Set damage to 1 if each projectile should remove one heart
    public Vector3 direction;
    private bool hasCollided = false; // Flag to ensure single collision

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasCollided && collision.CompareTag("Player"))
        {
            hasCollided = true; // Set flag to true on first collision
            Debug.Log("Projectile hit: " + collision.gameObject.name);
            Debug.Log("Projectile damage: " + damage);

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Damage applied to player");
            }

            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }
}
