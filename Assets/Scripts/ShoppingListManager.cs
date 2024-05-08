using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoppingListManager : MonoBehaviour
{
    public TextMeshProUGUI[] itemTexts; // Array of Text elements representing the items in the menu
    public static List<string> collectedItems = new List<string>(); // Static list of collected item names

    void Start()
    {
        // Initialize the shopping list UI at the start
        InitializeShoppingListUI();
    }

    // Initialize the UI to hide or show items based on collected status
    void InitializeShoppingListUI()
    {
        // Loop through each item text element
        foreach (TextMeshProUGUI itemText in itemTexts)
        {
            // Check if the item has been collected and update the UI accordingly
            if (collectedItems.Contains(itemText.text))
            {
                // If collected, show the item as crossed off
                itemText.text = "<s>" + itemText.text + "</s>";
            }
            else
            {
                // If not collected, ensure the text is normal (useful for resetting)
                itemText.text = itemText.text.Replace("<s>", "").Replace("</s>", "");
            }
        }
    }

    // Method to collect an item and update the UI
    public void CollectItem(string itemName)
    {
        // Only add the item if it hasn't already been collected
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName); // Add to the static list
            UpdateShoppingListUI(); // Update the UI to reflect the new collection
            
            // Check if the collected item is Bread
            if (itemName == "Bread")
            {
                GameManager.requiredItemCollected = true; 
            }
        }
    }

    // Reset the shopping list to initial state
    public void ResetShoppingList()
    {
        collectedItems.Clear(); // Clear the static list of collected items
        InitializeShoppingListUI(); // Re-initialize the UI to reflect the reset
    }

    // Update the shopping list UI to reflect the collected items
    void UpdateShoppingListUI()
    {
        foreach (TextMeshProUGUI itemText in itemTexts)
        {
            // Cross off the item if it has been collected
            if (collectedItems.Contains(itemText.text))
            {
                itemText.text = "<s>" + itemText.text + "</s>";
            }
            else
            {
                // If not collected, ensure the text is normal
                itemText.text = itemText.text.Replace("<s>", "").Replace("</s>", "");
            }
        }
    }
}
