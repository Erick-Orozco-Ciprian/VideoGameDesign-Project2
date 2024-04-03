using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ShoppingListManager shoppingListManager;
    public static bool requiredItemCollected = false;
    public static bool soulCountExceedsThreshold = false;
    public static bool enemyIsDestroyed = false;

    void Update() {
        CheckWinConditions();
    }

    public void CheckWinConditions()
    {
        // If all conditions are true, trigger game over
        //if (requiredItemCollected && soulCountExceedsThreshold && enemyIsDestroyed)
        if (requiredItemCollected)
        {
            // Do Win Stuff
            GameOver();
        }
    }

    // Method to be called when game over condition is met
    public void GameOver()
    {
        // Implement game over logic here
        //Debug.Log("Game Over!");
    }
}
