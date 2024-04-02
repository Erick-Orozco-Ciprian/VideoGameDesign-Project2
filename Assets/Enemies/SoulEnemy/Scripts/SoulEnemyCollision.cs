using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy has collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find the SoulCounterManager in the scene and increment the soul count
            FindObjectOfType<SoulCounterManager>().IncreaseSoulCount();

            // Destroy this enemy GameObject
            Destroy(gameObject);
        }
    }
}

