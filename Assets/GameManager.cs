using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ShoppingListManager shoppingListManager;
    public PlayerHealth playerHealth;
    public SoulCounterManager soulCounterManager; // Assuming this is needed for other parts of your game
    public static bool requiredItemCollected = false; // Flag to check if bread has been collected
    public static bool soulCountExceedsThreshold = false; // Assuming this is needed for other parts of your game
    public static bool enemyIsDestroyed = false; // Assuming this is needed for other parts of your game
    public CollectibleItem[] collectibleItems;   // Reference to the CollectibleItem script

    public static GameManager Instance { get; private set; } // Singleton pattern

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Optionally load the initial level or perform other start tasks
        //SceneManager.LoadScene("Level1");
    }

    void Update() 
    {
        CheckWinConditions(); // Check if the game should end based on collected items

        if (Input.GetKey(KeyCode.R)) 
        {
            ResetGame(); // Reset the game when R is pressed
        }
    }

    public void CheckWinConditions()
    {
        // If the required item (bread) has been collected, trigger the game over
        if (requiredItemCollected)
        {
            ShowWinScreen();
        }
    }

    public void ShowWinScreen()
    {
        // Load the game over or ending scene
        SceneManager.LoadScene("EndScene"); 
    }

        public void ShowGameOverScreen()
    {
        // Load the game over or ending scene
        SceneManager.LoadScene("Game Over Scene"); 
    }

    private void ResetGame()
    {
        // Reload the initial level scene to start over
        SceneManager.LoadScene("Level1");

        //reset soul counter if applicable
        if (soulCounterManager != null)
        {
            soulCounterManager.soulCount = 0;
        }

        // Reset the shopping list UI
        if (shoppingListManager != null)
        {
            shoppingListManager.ResetShoppingList();
        }

        //reset collected items
        if (collectibleItems != null) {
            foreach (CollectibleItem item in collectibleItems)
            {
                item.ResetItem(); // Call the ResetItem method for each collectible item
            }
        }

        if (playerHealth != null) 
        {
            // reset health
            playerHealth.resetHealth();
        }
    }
}