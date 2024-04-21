using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy has triggered with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Find the SoulCounterManager in the scene and increment the soul count
            FindObjectOfType<SoulCounterManager>().IncreaseSoulCount();

            // Destroy this enemy GameObject
            Destroy(gameObject);
        }
    }
}

