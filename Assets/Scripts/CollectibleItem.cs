using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName; // Name of the item as displayed in the menu
    public bool isRequiredItem = false; // Is this item necessary to complete the level?
    public ShoppingListManager shoppingListManager;
    private bool isCollected = false; // Flag to track if the item has been collected

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            // If the player collides with the item and it hasn't been collected yet
            CollectItem();
        }
    }

    void CollectItem()
    {
        isCollected = true; // Mark the item as collected

        // Cross off the item from the menu
        shoppingListManager.CollectItem(itemName);

        // If this item is required to complete the level, set the win condition
        if (isRequiredItem)
        {
            GameManager.requiredItemCollected = true;
        }

        // Optionally, play a sound or particle effect indicating collection
        // You can also deactivate the object or destroy it if needed
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
    // Method to reset the collected state of the item
    public void ResetItem()
    {
        // Reset the collected state and required item state
        isCollected = false;
        GameManager.requiredItemCollected = false;
    }
}

