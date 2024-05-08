using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameWin = false;
    public ShoppingListManager shoppingListManager;
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


        if (Input.GetKey(KeyCode.R) )
        {
            Debug.Log("Reset");

            SceneManager.LoadScene("Level1");
            Debug.Log("3");

            requiredItemCollected = false;

            //reset soul counter
            soulCounterManager.soulCount = 0;

            //reset lives??

            //reset list ui
            shoppingListManager.ResetShoppingList();
            Debug.Log("1");

            //reset collected items
            foreach (CollectibleItem item in collectibleItems)  //reset collected items
            {
                item.ResetItem(); // Call the ResetItem method for each collectible item
            }
            Debug.Log("2");

            gameWin = false;

        }

        if (!gameWin) {
            CheckWinConditions();
        }
    }

    public void CheckWinConditions()
    {
        // If the required item (bread) has been collected, trigger the game over
        if (requiredItemCollected)
        {
            // Do Win Stuff
            gameWin = true;
            ShowGameOverScreen();
            Debug.Log("required item collected");
        }
    }

    public void ShowGameOverScreen()
    {
        // Load the game over or ending scene
        SceneManager.LoadScene("EndScene");
    }

    private void ResetGame()
    {
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
        foreach (CollectibleItem item in collectibleItems)
        {
            item.ResetItem(); // Call the ResetItem method for each collectible item
        }

        // Reload the initial level scene to start over
        SceneManager.LoadScene("Level1");
    }
}
