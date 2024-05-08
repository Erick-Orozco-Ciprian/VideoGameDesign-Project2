using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    // Reference to the GameManager object
    public GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call ShowGameOverScreen function from GameManager
            gameManager.ShowGameOverScreen();
        }
    }
}
