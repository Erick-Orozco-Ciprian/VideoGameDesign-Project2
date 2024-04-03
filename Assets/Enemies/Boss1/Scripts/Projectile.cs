using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public Vector3 direction;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply damage to the player
            // e.g., collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        // Destroy the projectile after hitting something
        Destroy(gameObject);
    }
}


