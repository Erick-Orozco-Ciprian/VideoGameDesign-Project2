using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameWin = false;
    public ShoppingListManager shoppingListManager;
    public SoulCounterManager soulCounterManager;
    public static bool requiredItemCollected = false;
    public static bool soulCountExceedsThreshold = false;
    public static bool enemyIsDestroyed = false;
    public CollectibleItem[] collectibleItems;   // Reference to the CollectibleItem script

    public static GameManager Instance { get; private set; }

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
        // If all conditions are true, trigger game over
        //if (requiredItemCollected && soulCountExceedsThreshold && enemyIsDestroyed)
        if (requiredItemCollected)
        {
            // Do Win Stuff
            gameWin = true;
            ShowGameOverScreen();
            Debug.Log("required item collected");
        }
    }

    // Method to be called when game over condition is met
    public void ShowGameOverScreen()
    {
        // Show the game over screen
        SceneManager.LoadScene("Game Over Scene");
    }
}
